using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Security.Claims;
using InColUn.Auth;
using InColUn.Auth.FacebookOAuth;
using InColUn.Auth.GoogleOAuth;
using Newtonsoft.Json;

namespace testAuth
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
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            var authService = new OAuthService(new OAuthServiceOptions());

            this.AddFacebookOAuthStrategy(authService);
            this.AddGoogleOAuthStrategy(authService);

            services.AddSingleton(authService);
        }

        private void AddFacebookOAuthStrategy(OAuthService authService)
        {
            var fbOptions = FacebookStrategyOptions.CreateDefault();
            fbOptions.ClientId = Configuration["facebook:appid"];
            fbOptions.ClientSecret = Configuration["facebook:appsecret"];
            fbOptions.CallbackPath = "auth/facebook/callback";

            fbOptions.OnOAuthSuccess = OnExternalLoginSuccess;
            fbOptions.OnOAuthFailure = OnExternalLoginFailure;

            var fbStrategy = new FacebookOAuthStrategy(fbOptions);

            authService.AddStrategy(fbStrategy);
        }


        private void AddGoogleOAuthStrategy(OAuthService authService)
        {
            var gOptions = GoogleStrategyOptions.CreateDefault();
            gOptions.ClientId = Configuration["google:clientid"];
            gOptions.ClientSecret = Configuration["google:clientsecret"];
            gOptions.CallbackPath = "auth/google/callback";

            gOptions.OnOAuthSuccess = OnExternalLoginSuccess;
            gOptions.OnOAuthFailure = OnExternalLoginFailure;

            var gStrategy = new GoogleOAuthStrategy(gOptions);

            authService.AddStrategy(gStrategy);

        }

        private void FacebookLogin(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<OAuthService>();
                await authService.StartAuthentificationAsync(context, FacebookDefaults.AuthenticationScheme);
            });
        }

        private void GoogleLogin(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<OAuthService>();
                await authService.StartAuthentificationAsync(context, GoogleDefaults.AuthenticationScheme);
            });
        }

        private void FacebookLoginCallback(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<OAuthService>();
                var fbStrategy = authService[FacebookDefaults.AuthenticationScheme];
                await fbStrategy.ProcessCallbackAsync(context);
            });
        }

        private void GoogleLoginCallback(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var authService = app.ApplicationServices.GetService<OAuthService>();
                var gStrategy = authService[GoogleDefaults.AuthenticationScheme];
                await gStrategy.ProcessCallbackAsync(context);
            });
        }

        private async Task OnExternalLoginSuccess(HttpContext context, ClaimsIdentity identity, string authSchema)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync("<html><body>");
            await context.Response.WriteAsync("external login successfull<br/>");
            await context.Response.WriteAsync(authSchema);
            await context.Response.WriteAsync("<br/>");


            foreach (var claim in identity.Claims)
            {
                await context.Response.WriteAsync(claim.ToString());
                await context.Response.WriteAsync("<br/>");
            }
            await context.Response.WriteAsync("</body></html>");
        }

        private async Task OnExternalLoginFailure(HttpContext context, string error, string authSchema)
        {
            await context.Response.WriteAsync(string.Format("External Login for {0} Failed.<br/>Error: ", authSchema));
            await context.Response.WriteAsync(error);

            var authResponse = new AuthResponse
            {
                Success = false,
                Message = "Login failed"
            };

            context.Response.Clear();
            context.Response.ContentType = "application/json; charset=utf-8";
            string json = JsonConvert.SerializeObject(authResponse);
            await context.Response.WriteAsync(json);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Map("/auth/Facebook/Login", FacebookLogin);
            app.Map("/auth/Google/Login", GoogleLogin);

            app.Map("/auth/Facebook/callback", FacebookLoginCallback);
            app.Map("/auth/google/callback", GoogleLoginCallback);

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync("<html><body>");
                await context.Response.WriteAsync("<a href=\"/auth/Facebook/Login\">FaceBookLogin</a><br>");
                await context.Response.WriteAsync("<a href=\"/auth/Google/Login\">GoogleLogin</a>");
                await context.Response.WriteAsync("</body></html>");
            });
        }
    }
}
