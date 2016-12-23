using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using AuthLib.Token;
using InColUn.Data.Repositories;


namespace InColUn.Controllers
{
    public class ApiController 
    {
        public static async Task ApiBoards(IApplicationBuilder app, HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            if (!context.Request.Cookies.ContainsKey("access_token"))
            {
                await ApiController.FailureResponse(context, "Access Token is missing");
                return;
            }

            var token = context.Request.Cookies["access_token"];
            var tokenProvider = app.ApplicationServices.GetService<TokenProvider>();
            var tokenId = tokenProvider.ValidateToken(token);
            if (tokenId == null)
            {
                await ApiController.FailureResponse(context, "Invalid access token");
                return;
            }

            if (context.Request.Method == "POST")
            {
                await ApiController.CreateBoard(app, context, tokenId.Value);
                return;
            }

            await ApiController.GetBoards(app, context, tokenId.Value);
        }

        public static async Task GetBoards(IApplicationBuilder app, HttpContext context, long tokenId)
        {
            var userBoardsRepository = app.ApplicationServices.GetService<IUserBoardRepository>();
            var boards = userBoardsRepository.GetUserBoards(tokenId, UserBoardRelations.Owner)
                .Select(board => new
                {
                    id = board.boardid,
                    title = board.Title,
                    created = board.created,
                    updated = board.updated
                }).ToList();

            var content = JsonConvert.SerializeObject(new { success = true, boards = boards });
            await context.Response.WriteAsync(content);
        }

        public static async Task CreateBoard(IApplicationBuilder app, HttpContext context, long tokenId)
        {
            const string titleField = "title";

            if (!context.Request.Form.ContainsKey(titleField))
            {
                await ApiController.FailureResponse(context, "Board title is missing");
                return;
            }

            var title = context.Request.Form[titleField].ToString();

            if (string.IsNullOrWhiteSpace(title) )
            {
                await ApiController.FailureResponse(context, "Can't create board, title is null or whitespace.");
                return;
            }

            var flakeIdGenerator = app.ApplicationServices.GetService<FlakeGen.Id64Generator>();
            var boardId = flakeIdGenerator.GenerateId();

            var boardsRepository = app.ApplicationServices.GetService<IBoardsRepository>();
            if(!boardsRepository.CreateBoard(boardId, title))
            {
                await ApiController.FailureResponse(context, "Failed to create board.");
                return;
            }

            var userBoardRepository = app.ApplicationServices.GetService<IUserBoardRepository>();
            if (!userBoardRepository.CreateUserBoard(tokenId, boardId, UserBoardRelations.Owner))
            {
                boardsRepository.DeleteBoardById(boardId);
                await ApiController.FailureResponse(context, "Failed to create user-board link.");
                return;
            }

            string json = JsonConvert.SerializeObject(new { success = true, title = title});
            await context.Response.WriteAsync(json);
        }

        private static async Task FailureResponse(HttpContext context, string message)
        {
            string json = JsonConvert.SerializeObject(new { success = false, message = message });
            await context.Response.WriteAsync(json);
        }
    }
}
