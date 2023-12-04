using Application.RepositoryInterface;
using Application.ServiceInterface;
using Domain.Models;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Presentation_MVC.ViewModels;

namespace Presentation_MVC.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceService _raceService;
        private readonly IPhotoService _photoService;

        public RaceController(IRaceService raceRepository, IPhotoService photoService)
        {
            _raceService = raceRepository;
            _photoService = photoService;
        }
        public IActionResult Index()
        {
            var races = _raceService.GetRace();
            return View(races);
        }
        public IActionResult Detail(int id)
        {
            var race = _raceService.GetRaceById(id);
            return View(race);
        }

        public async Task<IActionResult> Create(CreateRaceViewModels races)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }
            else
            {
                var result = await _photoService.AddPhotoAsync(races.Image);
                var race = new Race()
                {
                    Title = races.Title,
                    Description = races.Description,
                    Image = result.Url.ToString(),
                    Address = new Address()
                    {
                        Street = races.Address.Street,
                        City = races.Address.City,
                        State = races.Address.State,
                    }
                };
                _raceService.Add(race);
            }


            return View(races);


            //if (ModelState.IsValid)
            //{
            //    return View(clubs);
            //}
            //else
            //{
            //    _clubRepository.Add(clubs);
            //    return RedirectToAction("Index");
            //}

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var race = _raceService.GetRaceById(Id);
            if (race == null) return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                Address = race.Address,
                AddressId = race.AddressId,
                URL = race.Image,
                RaceCategory = race.RaceCategory

            };
            return View(raceVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
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
            var userClub = _raceService.GetRaceByIdNoTracking(id);
            if (userClub == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo uplaad failed");
                return View(raceVM);
            }

            if (!string.IsNullOrEmpty(userClub.Image))
            {
                _ = _photoService.DeletePhotoAsync(userClub.Image);
            }


            var race = new Race
            {
                Id = raceVM.Id,
                Title = raceVM.Title,
                Description = raceVM.Description,
                Image = photoResult.Url.ToString(),
                AddressId = raceVM.AddressId,
                Address = raceVM.Address,
            };
            var updatedOrNot = _raceService.Update(race);
            if (updatedOrNot == true)
                return RedirectToAction("Index");
            else
                return View();
        }
        public IActionResult Delete(int id)
        {
            var result = _raceService.Delete(id);
            if (result) return View();
            return View("Error");
        }
    }
}
