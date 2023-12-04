﻿using Domain.Enum;
using Domain.Models;

namespace Presentation_MVC.ViewModels
{
    public class CreateRaceViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
