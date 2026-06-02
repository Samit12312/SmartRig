using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class UpdateProfileViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "You must enter your name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "First name cannot be longer than 15 characters and less than 2")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "You must enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [RegularExpression(
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$",
            ErrorMessage = "Email must be a real format like name@gmail.com"
        )]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "You must enter your address")]
        public string UserAddress { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose a city")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "You must enter your phone number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string UserPhoneNumber { get; set; }
    }
}