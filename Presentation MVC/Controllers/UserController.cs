using Application.RepositoryInterface;
using Microsoft.AspNetCore.Mvc;
using RunningGroups.Interface;
using RunningGroups.ViewModels;

namespace Presentation_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsers();
            List<UserInformationViewModel> allUsers = new List<UserInformationViewModel>();
            foreach (var user in users)
            {
                var viewModel = new UserInformationViewModel()
                {
                    Id = user.Id,
                    Pace = user.Pace,
                    Mileage = user.Mileage,
                    Username = user.UserName,
                    ProfileImageUrl = user.ProfilieImageUrl

                };
                allUsers.Add(viewModel);
            }
            return View(allUsers);
        }
        [Route("users/detail/{id}")]
        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userService.GetById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                Pace = user.Pace,
                Mileage = user.Mileage,
                Username = user.UserName,
                ProfileImageUrl = user.ProfilieImageUrl
            };
            return View(userDetailViewModel);
        }
    }
}
