﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using miniAuth.FacebookOAuth;
using miniAuth.GoogleOAuth;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using miniAuth.Token;
using System.Security.Cryptography;

namespace InColUn
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["connection_string:mssql"];

            var authService = new miniAuth.OAuthService(new miniAuth.OAuthServiceOptions());
            //TODO add logger
            var msDBContext = new Db.MSSqlDbContext(connectionString, null, null);
            msDBContext.AddTableService(new Db.UserTableService(msDBContext));
            msDBContext.AddTableService(new Db.BoardsTableService(msDBContext));
            msDBContext.AddTableService(new Db.UserBoardTableService(msDBContext));

            var flakeIdGenerator = new FlakeGen.Id64Generator();

            this.AddFacebookOAuthStrategy(authService);
            this.AddGoogleOAuthStrategy(authService);

            services.AddSingleton(authService);
            services.AddSingleton(msDBContext);
            services.AddSingleton(flakeIdGenerator);

            this.ConfigureToken(services);

            services.AddMvc();
        }

        private void ConfigureToken(IServiceCollection services)
        {
            byte[] keyForHmacSha256 = new byte[64];

            var randomGen = RandomNumberGenerator.Create();
            randomGen.GetBytes(keyForHmacSha256);

            var securityKey = new SymmetricSecurityKey(keyForHmacSha256);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var options = new TokenProviderOptions
            {
                Issuer = "InColUn",
                Audience = "All",
                SigningCredentials = signingCredentials,
                Expiration = TimeSpan.FromMilliseconds(1000)
            };

            //TODO add logger
            var tokenProvider = new TokenProvider(options, null);

            services.AddSingleton(tokenProvider);
        }

        private void AddFacebookOAuthStrategy(miniAuth.OAuthService authService)
        {
            var fbOptions = FacebookStrategyOptions.CreateDefault();
            fbOptions.ClientId = Configuration["facebook:appid"];
            fbOptions.ClientSecret = Configuration["facebook:appsecret"];
            fbOptions.CallbackPath = "auth/facebook/callback";

            var fbStrategy = new FacebookOAuthStrategy(fbOptions);

            authService.AddStrategy(fbStrategy);
        }

        private void AddGoogleOAuthStrategy(miniAuth.OAuthService authService)
        {
            var gOptions = GoogleStrategyOptions.CreateDefault();
            gOptions.ClientId = Configuration["google:clientid"];
            gOptions.ClientSecret = Configuration["google:clientsecret"];
            gOptions.CallbackPath = "auth/google/callback";

            var gStrategy = new GoogleOAuthStrategy(gOptions);

            authService.AddStrategy(gStrategy);
        }

        private void FacebookLogin(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<miniAuth.OAuthService>();
                
                await authService.StartAuthentificationAsync(context, FacebookDefaults.AuthenticationScheme);
            });
        }

        private void GoogleLogin(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<miniAuth.OAuthService>();
                await authService.StartAuthentificationAsync(context, GoogleDefaults.AuthenticationScheme);
            });
        }

        private void FacebookLoginCallback(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<miniAuth.OAuthService>();
                var fbStrategy = authService[FacebookDefaults.AuthenticationScheme];

                var options = fbStrategy.GetOptions();

                options.OnOAuthFailure = async (string error, string authSchema) =>
                {
                    await OnExternalLoginFailure(app, context, error, authSchema);
                };

                options.OnOAuthSuccess = async (ClaimsIdentity identity, string authSchema) =>
                {
                    await OnExternalLoginSuccess(app, context, identity, authSchema);
                };

                await fbStrategy.ProcessCallbackAsync(context);
            });
        }

        private void GoogleLoginCallback(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<miniAuth.OAuthService>();
                var gStrategy = authService[GoogleDefaults.AuthenticationScheme];
                
                var options = gStrategy.GetOptions();

                options.OnOAuthFailure = async (string error, string authSchema) =>
                {
                    await OnExternalLoginFailure(app, context, error, authSchema);
                };

                options.OnOAuthSuccess = async (ClaimsIdentity identity, string authSchema) =>
                {
                    await OnExternalLoginSuccess(app, context, identity, authSchema);
                };

                await gStrategy.ProcessCallbackAsync(context);
            });
        }

        private async Task OnExternalLoginSuccess(
            IApplicationBuilder app,
            HttpContext context,
            ClaimsIdentity identity, 
            string authSchema)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            string name = string.Empty;
            string email = string.Empty;
            string profileId = string.Empty;

            foreach (var claim in identity.Claims)
            {
                if (claim.Type == ClaimTypes.Email) email = claim.Value;
                else if (claim.Type == ClaimTypes.NameIdentifier) profileId = claim.Value;
                else if (claim.Type == ClaimTypes.Name) name = claim.Value;
            }

            if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(profileId) || string.IsNullOrEmpty(email))
            {
                await this.ProcessAuthFailure(context
                    , string.Format("Information from external provider{0} is missing", authSchema));
                return;
            }

            var dbContext = app.ApplicationServices.GetService<Db.MSSqlDbContext>();
            var userTable = dbContext.GetTableService<Db.UserTableService>();

            var user = userTable.FindUserByLoginString(email);

            var tokenProvider = app.ApplicationServices.GetService<TokenProvider>();
            if (user != null)
            {
                var tokenResponse = tokenProvider.GenerateUserToken(user.Id, name);
                await this.ProcessAuthSuccess(context, tokenResponse.access_token);
                return;
            }

            var flakeIdGenerator = app.ApplicationServices.GetService<FlakeGen.Id64Generator>();
            var userId = flakeIdGenerator.GenerateId();
            bool result = userTable.CreateExternalUser(userId, profileId, name, email, authSchema.Substring(0,1));

            if(!result)
            {
                await this.ProcessAuthFailure(context, "Erroe authentification external user");
                return;
            }

            {
                var tokenResponse = tokenProvider.GenerateUserToken(userId, name);
                await this.ProcessAuthSuccess(context, tokenResponse.access_token);
            }
        }

        private async Task OnExternalLoginFailure(
            IApplicationBuilder app,
            HttpContext context, 
            string error, 
            string authSchema)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            await this.ProcessAuthFailure(context
                , string.Format("External Login for {0} Failed.<br/>Error: {1}", authSchema, error));
        }

        private async Task ProcessAuthSuccess(HttpContext context, string token_auth)
        {
            string json = JsonConvert.SerializeObject(new { success = true, token = token_auth });
            await context.Response.WriteAsync(json);
        }

        private async Task ProcessAuthFailure(HttpContext context, string message)
        {
            string json = JsonConvert.SerializeObject(new { success = false, message = message });
            await context.Response.WriteAsync(json);
        }

        private void SignUp(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "application/json; charset=utf-8";

                if (   !context.Request.Form.ContainsKey("name") 
                    || !context.Request.Form.ContainsKey("email")
                    || !context.Request.Form.ContainsKey("password"))
                {
                    await this.ProcessAuthFailure(context, "Can't create user, some fields are missing");
                    return;
                }

                var name = context.Request.Form["name"].ToString();
                var email = context.Request.Form["email"].ToString();
                var password = context.Request.Form["password"].ToString();

                if(string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    await this.ProcessAuthFailure(context, "Can't create user, some fields are empty");
                    return;
                }

                var dbContext = app.ApplicationServices.GetService<Db.MSSqlDbContext>();
                var userTable = dbContext.GetTableService<Db.UserTableService>();

                var ph = Helpers.Crypto.GeneratePasswordHashSalt(password);

                var user = userTable.FindUserByLoginString(email);
                if(user != null)
                {
                    await this.ProcessAuthFailure(context, "User already exist.");
                    return;
                }

                var flakeIdGenerator = app.ApplicationServices.GetService<FlakeGen.Id64Generator>();
                var userId = flakeIdGenerator.GenerateId();

                if (!userTable.CreateLocalUser(userId, name, ph.Item1, ph.Item2, email))
                {
                    await this.ProcessAuthFailure(context, "Failed to create a user");
                    return;
                }

                var tokenProvider = app.ApplicationServices.GetService<TokenProvider>();
                var tokenResponse = tokenProvider.GenerateUserToken(userId, name);

                await this.ProcessAuthSuccess(context, tokenResponse.access_token);
            });
        }

        private void SignIn(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "application/json; charset=utf-8";

                if (!context.Request.Form.ContainsKey("email")
                    || !context.Request.Form.ContainsKey("password"))
                {
                    await this.ProcessAuthFailure(context, "Can't login, some fields are missing");
                    return;
                }

                var name = context.Request.Form["email"].ToString();
                var password = context.Request.Form["password"].ToString();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
                {
                    await this.ProcessAuthFailure(context, "Can't login, some fields are empty.");
                    return;
                }

                var dbContext = app.ApplicationServices.GetService<Db.MSSqlDbContext>();
                var userTable = dbContext.GetTableService<Db.UserTableService>();

                var user = userTable.FindUserByLoginString(name);
                if (user == null)
                {
                    await this.ProcessAuthFailure(context, "Can't find user with this name");
                    return;
                }

                var password_hash = Helpers.Crypto.GeneratePasswordHash(password, user.salt ?? 0);
                if(password_hash != user.password_hash)
                {
                    await this.ProcessAuthFailure(context, "Wrong password.");
                    return;
                }

                var tokenProvider = app.ApplicationServices.GetService<TokenProvider>();
                var tokenResponse = tokenProvider.GenerateUserToken(user.Id, user.display_name);

                await this.ProcessAuthSuccess(context, tokenResponse.access_token);
            });
        }

        public void Boards(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("Hello boards");
                await context.Response.WriteAsync("</body></html>");
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.Map("/auth/signup", SignUp);
            app.Map("/auth/login", SignIn);

            app.Map("/auth/facebook/login", FacebookLogin);
            app.Map("/auth/google/login", GoogleLogin);

            app.Map("/auth/Facebook/callback", FacebookLoginCallback);
            app.Map("/auth/google/callback", GoogleLoginCallback);

            //app.Map("/boards", Boards);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "Boards",
                    template: "{controller=Boards}/{action=Index}");
            });
        }
    }
}
