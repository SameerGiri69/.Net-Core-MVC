using Domain.Enum;
using Domain.Models;

namespace Presentation_MVC.ViewModels
{
    public class CreateClubViewModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public ClubCategory ClubCategory { get; set; }
    }
}
