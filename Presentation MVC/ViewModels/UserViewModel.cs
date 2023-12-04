using System.ComponentModel.DataAnnotations;

namespace RunningGroups.ViewModels
{
    public class UserViewModel 
    {
        [Key]
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
    }
}
