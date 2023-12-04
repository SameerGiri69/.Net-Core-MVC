using Application.ServiceInterface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.ViewModels;

namespace Presentation_MVC.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;
        private readonly IPhotoService _photoService;

        public ClubController(IClubService clubService, IPhotoService photoService)
        {
            _clubService = clubService;
            _photoService = photoService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var clubs = _clubService.GetClubs();
            return View(clubs);
        }
        public IActionResult Detail(int id)
        {
            var result = _clubService.GetClubById(id);
            if (result == null)
                return View("Error");
            return View(result);
        }
        public IActionResult Delete(int id)
        {
            var result = _clubService.Delete(id);
            if (result) return View();
            return View("Error");
        }

        public async Task<IActionResult> Create(CreateClubViewModels clubs)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {
                var result = await _photoService.AddPhotoAsync(clubs.Image);
                var club = new Club()
                {
                    Title = clubs.Title,
                    Description = clubs.Description,
                    Image = result.Url.ToString(),
                    Address = new Address()
                    {
                        Street = clubs.Address.Street,
                        City = clubs.Address.City,
                        State = clubs.Address.State,
                    }
                };
                _clubService.Add(club);
            }


            return View(clubs);

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var clubs = _clubService.GetClubById(Id);
            if (clubs == null) return View("Error");
            var clubVM = new EditClubViewModels
            {
                Title = clubs.Title,
                Description = clubs.Description,
                Address = clubs.Address,
                AddressId = clubs.AddressId,
                URL = clubs.Image,
                ClubCategory = clubs.ClubCategory

            };
            return View(clubVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModels clubVM)
        {

            //if(!ModelState.IsValid)
            //{
            //    var clubs = _clubRepository.GetClubById(id);
            //    if (clubs == null) return View("Error");
            //    var ViewModel = new EditClubViewModel
            //    {
            //        Title = clubs.Title,
            //        Description = clubs.Description,
            //        Address = clubs.Address,
            //        AddressId = clubs.AddressId,
            //        URL = clubs.Image,
            //        ClubCategory = clubs.ClubCategory

            //    };
            //    return View(ViewModel);
            //}
            var userClub = _clubService.GetClubByIdNoTracking(id);
            if (userClub == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo uplaad failed");
                return View(clubVM);
            }

            if (!string.IsNullOrEmpty(userClub.Image))
            {
                _ = _photoService.DeletePhotoAsync(userClub.Image);
            }


            var club = new Club
            {
                Id = clubVM.Id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Address = clubVM.Address,
            };
            var updatedOrNot = _clubService.Update(club);
            if (updatedOrNot == true)
                return RedirectToAction("Index");
            else
                return View();
        }

    }
}

