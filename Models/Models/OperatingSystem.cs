using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class OperatingSystem : Model
    {
        int operatingSystemId;
        string operatingSystemName;
        int operatingSystemPrice;
        int operatingSystemCompanyId;

        public int OperatingSystemId
        {
            get { return this.operatingSystemId; }
            set { this.operatingSystemId = value; }
        }

        [Required(ErrorMessage = "You must enter operating system name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Operating system name must be between 2 and 40 characters")]
        public string OperatingSystemName
        {
            get { return this.operatingSystemName; }
            set
            {
                this.operatingSystemName = value;
                ValidateProperty(value, "OperatingSystemName");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Operating system price must be bigger than 0")]
        public int OperatingSystemPrice
        {
            get { return this.operatingSystemPrice; }
            set
            {
                this.operatingSystemPrice = value;
                ValidateProperty(value, "OperatingSystemPrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int OperatingSystemCompanyId
        {
            get { return this.operatingSystemCompanyId; }
            set
            {
                this.operatingSystemCompanyId = value;
                ValidateProperty(value, "OperatingSystemCompanyId");
            }
        }
    }
}