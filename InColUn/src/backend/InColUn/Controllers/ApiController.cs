using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using miniAuth.Token;


namespace InColUn.Controllers
{
    public class ApiController 
    {
        public static async Task GetBoards(IApplicationBuilder app, HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            if (!context.Request.Cookies.ContainsKey("access_token"))
            {
                await ApiController.FailureResponse(context, "Access Token is missing");
                return;
            }

            var token = context.Request.Cookies["access_token"];
            var tokenProvider = app.ApplicationServices.GetService<TokenProvider>();
            var result = tokenProvider.ValidateToken(token);
            if(result == null)
            {
                await ApiController.FailureResponse(context, "Invalid access token");
                return;
            }

            var dbContext = app.ApplicationServices.GetService<Db.MSSqlDbContext>();

            var boardsTable = dbContext.GetTableService<Db.BoardsTableService>();
            var boards = boardsTable.GetBoards(result.Value).ToList();

            var content = JsonConvert.SerializeObject(new { success = true, boards = boards });
            await context.Response.WriteAsync(content);
        }

        public static async Task ApiBoard(IApplicationBuilder app, HttpContext context)
        {
            if(context.Request.Method == "POST")
            {
                await ApiController.CreateBoard(app, context);
                return;
            }

            context.Response.ContentType = "application/json; charset=utf-8";

            if (!context.Request.Cookies.ContainsKey("access_token"))
            {
                await ApiController.FailureResponse(context, "Access Token is missing");
                return;
            }

            var token = context.Request.Cookies["access_token"];
            var tokenProvider = app.ApplicationServices.GetService<TokenProvider>();
            var result = tokenProvider.ValidateToken(token);
            if (result == null)
            {
                await ApiController.FailureResponse(context, "Invalid access token");
                return;
            }

            var dbContext = app.ApplicationServices.GetService<Db.MSSqlDbContext>();

            var boardsTable = dbContext.GetTableService<Db.BoardsTableService>();
            var boards = boardsTable.GetBoards(result.Value).ToList();

            var content = JsonConvert.SerializeObject(new { success = true, boards = boards });
            await context.Response.WriteAsync(content);
        }

        public static async Task CreateBoard(IApplicationBuilder app, HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            if (!context.Request.Cookies.ContainsKey("access_token"))
            {
                await ApiController.FailureResponse(context, "Access Token is missing");
                return;
            }

            var token = context.Request.Cookies["access_token"];
            var tokenProvider = app.ApplicationServices.GetService<TokenProvider>();
            var result = tokenProvider.ValidateToken(token);
            if (result == null)
            {
                await ApiController.FailureResponse(context, "Invalid access token");
                return;
            }

            var dbContext = app.ApplicationServices.GetService<Db.MSSqlDbContext>();

            var boardsTable = dbContext.GetTableService<Db.BoardsTableService>();
            var boards = boardsTable.GetBoards(result.Value).ToList();

            var content = JsonConvert.SerializeObject(new { success = true, boards = boards });
            await context.Response.WriteAsync(content);
        }


        [HttpPost]
        IActionResult board([FromBody] string title)
        {
            return null;
        }

        IActionResult board()
        {
            return null;
        }

        private static async Task FailureResponse(HttpContext context, string message)
        {
            string json = JsonConvert.SerializeObject(new { success = false, message = message });
            await context.Response.WriteAsync(json);
        }
    }
}
