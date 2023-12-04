﻿using System.ComponentModel.DataAnnotations;

namespace RunningGroups.ViewModels
{
    public class RegisterViewModel
    {
        [Key]
        [Display(Name = "Email address")]
        [Required(ErrorMessage ="Email address is required")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
