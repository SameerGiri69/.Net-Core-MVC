using Domain.Models;

namespace RunningGroups.ViewModels
{
    public class DashboardViewModel
    {
        public string UserName { get; set; }
        public List<Race> Races { get; set; }
        public List<Club> Clubs { get; set; }   
    }
}
