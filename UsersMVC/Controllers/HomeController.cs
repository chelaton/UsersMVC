using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UsersMVC.DTOs;
using UsersMVC.Helpers;
using UsersMVC.Models;
using UsersMVC.Services;

namespace UsersMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Users(int? pageNumber, string sortOrder, string currentFilter)
        {
            var users = await _userService.GetUsersFromApiAsync();

            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["EmailSortParm"] = sortOrder == "email" ? "email_desc" : "email";
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.name).ToList();
                    break;
                case "email":
                    users = users.OrderBy(s => s.email).ToList();
                    break;
                case "email_desc":
                    users = users.OrderByDescending(s => s.email).ToList();
                    break;
                default:
                    users = users.OrderBy(s => s.name).ToList();
                    break;
            }

            int pageSize = 4;
            return View(await PaginatedList<User>.CreateAsync(users, pageNumber ?? 1, pageSize));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}