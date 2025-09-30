using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User:Model
    {
        int userId;
        string userName;
        string userEmail;
        string userPassword;
        string userAddress;
        int cityId;
        string userPhoneNumber;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [Required(ErrorMessage = "You must enter your name")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "First name cannot be longer than 15 characters and less than 2")]
        [FirstLetterCapital(ErrorMessage = "First letter must be capital")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; 
            ValidateProperty(value,"UserName");
            }
        }
        [Required(ErrorMessage = "You must enter your email address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string UserEmail
        {
            get { return userEmail; }
            set { userEmail = value;
                ValidateProperty(value, "UserEmail");
            }
        }
        [Required(ErrorMessage = "You must enter your password")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Password must be between 4 and 25 characters.")]
        [RegularExpression(@"^(?=.*\d)", ErrorMessage = "Password must contain at least one number.")]
        public string UserPassword
        {
            get { return userPassword; }
            set { userPassword = value;
                ValidateProperty(value, "UserPassword");
            }
        }
        [Required(ErrorMessage = "You must enter your address")]
        public string UserAddress
        {
            get { return userAddress; }
            set { userAddress = value;
                ValidateProperty(value, "UserAddress");
            }
        }
        [Required(ErrorMessage = "You must enter your phone number")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string UserPhoneNumber
        {
            get { return userPhoneNumber; }
            set { userPhoneNumber = value; }
        }
        [Required(ErrorMessage = "You must enter your city")]
        public int CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }
    }
}
