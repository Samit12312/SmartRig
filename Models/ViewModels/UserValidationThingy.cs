using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserValidationThingy : Model
    {
        string userEmail;
        string userPassword;
        [Required(ErrorMessage = "You must enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
                ValidateProperty(value, "UserEmail");
            }
        }
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 25 characters.")]
        [RegularExpression(@"^(?=.*\d).+$", ErrorMessage = "Password must contain at least one number.")]
        [Required(ErrorMessage = "You must enter your password")]
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
                ValidateProperty(value, "UserPassword");
            }
        }
    }
}
