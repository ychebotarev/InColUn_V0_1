using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using miniAuth.Token;

namespace InColUn.Controllers
{
    public class ApiController : Controller
    {
        private TokenProvider tokenProvider;
        private Db.MSSqlDbContext dbContext;

        public ApiController(TokenProvider tokenProvider, Db.MSSqlDbContext dbContext)
        {
            this.tokenProvider = tokenProvider;
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            //return list ofavailable api
            if (!HttpContext.Request.Cookies.ContainsKey("access_token"))
            {
                return RedirectToAction("Index", "Home");
            }

            var token = HttpContext.Request.Cookies["access_token"];

            //TODO refresh token
            return View();
        }

        public IActionResult Boards()
        {
            if (!HttpContext.Request.Cookies.ContainsKey("access_token"))
            {
                return FailureResponse("Access Token is missing");
            }

            var token = HttpContext.Request.Cookies["access_token"];

            var result = this.tokenProvider.ValidateToken(token);
            if(result == null)
            {
                return FailureResponse("Invalid access token");
            }

            var boardsTable = this.dbContext.GetTableService<Db.BoardsTableService>();
            var boards = boardsTable.GetBoards(result.Value).ToList();
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { success = true, boards = boards }),
                ContentType = "application/json; charset=utf-8",
                StatusCode = 401
            };
        }

        private ContentResult FailureResponse(string message)
        {
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { success = false, message = message }),
                ContentType = "application/json; charset=utf-8",
                StatusCode = 401
            };
        }
    }
}
