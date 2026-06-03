using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : Model
    {
        int userId;
        string userName;
        string userEmail;
        string userPassword;
        string userAddress;
        int cityId;
        string userPhoneNumber;
        bool manager;

        public string UserSalt { get; set; } = "";
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
            set
            {
                userName = value;
                ValidateProperty(value, "UserName");
            }
        }

        [Required(ErrorMessage = "You must enter your email")]
        [StringLength(40, MinimumLength = 16, ErrorMessage = "Email must be a valid Gmail address")]
        [RegularExpression(@"^(?!.*\.\.)[A-Za-z0-9](?:[A-Za-z0-9.]{4,28}[A-Za-z0-9])@gmail\.com$", ErrorMessage = "Email must be a valid Gmail address and end with @gmail.com")]
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

        [Required(ErrorMessage = "You must enter your address")]
        [StringLength(80, MinimumLength = 2, ErrorMessage = "Address must be between 2 and 80 characters")]
        public string UserAddress
        {
            get { return userAddress; }
            set
            {
                userAddress = value;
                ValidateProperty(value, "UserAddress");
            }
        }

        [Required(ErrorMessage = "You must enter your phone number")]
        [RegularExpression(@"^(05\d{8}|07\d{8}|0[23489]\d{7})$", ErrorMessage = "Phone number must be a valid Israeli number")]
        public string UserPhoneNumber
        {
            get { return userPhoneNumber; }
            set
            {
                userPhoneNumber = value;
                ValidateProperty(value, "UserPhoneNumber");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose a city")]
        public int CityId
        {
            get { return cityId; }
            set
            {
                cityId = value;
                ValidateProperty(value, "CityId");
            }
        }

        public bool Manager
        {
            get { return manager; }
            set
            {
                manager = value;
                ValidateProperty(value, "Manager");
            }
        }
    }
}
