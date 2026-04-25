using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Cities : Model
    {
        int cityId;
        string cityName;

        public int CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }

        [Required(ErrorMessage = "You must enter city name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City name must be between 2 and 30 characters")]
        public string CityName
        {
            get { return cityName; }
            set
            {
                cityName = value;
                ValidateProperty(value, "CityName");
            }
        }
    }
}