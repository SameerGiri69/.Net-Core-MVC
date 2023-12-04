using Application.RepositoryInterface;
using Application.ServiceInterface;
using CloudinaryDotNet.Actions;
using Domain.Models;
using Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using RunningGroups.Interface;
using RunningGroups.ViewModels;

namespace Presentation_MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(IDashboardService dashboardService, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _dashboardService = dashboardService;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var id = _httpContextAccessor.HttpContext.User.GetUserId();
            var userName = await _dashboardService.GetUserByIdNoTracking(id);
            var userRaces = await _dashboardService.GetAllUserRaces();
            var userClubs = await _dashboardService.GetAllUserClubs();
            var dashboardViewModel = new DashboardViewModel()
            {
                UserName = userName.UserName,
                Races = userRaces,
                Clubs = userClubs
            };
            return View(dashboardViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardService.GetUserById(curUserId);
            if (user == null)
                View("Error");
            var dashboardEditUserViewModel = new EditUserDashboardViewModel()
            {
                Id = curUserId,
                Pace = user.Pace,
                City = user.City,
                State = user.State,
                Mileage = user.Mileage,
                ProfileImageUrl = user.ProfilieImageUrl
            };
            return View(dashboardEditUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel dashboardViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", dashboardViewModel);
            }
            var user = await _dashboardService.GetUserByIdNoTracking(dashboardViewModel.Id);

            if (user.ProfilieImageUrl == "" || user.ProfilieImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(dashboardViewModel.Image);

                MapUserProfile(dashboardViewModel, photoResult, user);
                _dashboardService.Update(user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                try
                {
                    var result = await _photoService.DeletePhotoAsync(user.ProfilieImageUrl);

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "could not upload the photo");
                    return View(dashboardViewModel);
                }
                var photoResult = await _photoService.AddPhotoAsync(dashboardViewModel.Image);

                MapUserProfile(dashboardViewModel, photoResult, user);
                _dashboardService.Update(user);
                return RedirectToAction("Index");
            }


        }
        private void MapUserProfile(EditUserDashboardViewModel editUserDashboard, ImageUploadResult uploadResult, AppUser _appUser)
        {
            _appUser.Id = editUserDashboard.Id;
            _appUser.Pace = editUserDashboard.Pace;
            _appUser.Mileage = editUserDashboard.Mileage;
            _appUser.ProfilieImageUrl = uploadResult.Url.ToString();
            _appUser.City = editUserDashboard.City;
            _appUser.State = editUserDashboard.State;
        }
    }
}

