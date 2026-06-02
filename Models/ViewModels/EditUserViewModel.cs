using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class EditUserViewModel : Model
    {
        private int userId;
        private string userName;
        private string userEmail;
        private string userPassword;
        private string userAddress;
        private string userPhoneNumber;
        private int cityId;
        private bool manager;

        [Range(1, int.MaxValue, ErrorMessage = "User id is not valid")]
        public int UserId
        {
            get { return userId; }
            set
            {
                userId = value;
                ValidateProperty(value, "UserId");
            }
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
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Email must be between 6 and 50 characters")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.[A-Za-z]{2,}$", ErrorMessage = "Email must be a valid email address")]
        public string UserEmail
        {
            get { return userEmail; }
            set
            {
                userEmail = value;
                ValidateProperty(value, "UserEmail");
            }
        }
        public string UserPassword
        {
            get { return userPassword; }
            set
            {
                userPassword = value;
                ValidateProperty(value, "UserPassword");
            }
        }

        [Required(ErrorMessage = "You must enter address")]
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

        [Range(1, int.MaxValue, ErrorMessage = "You must choose city")]
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