using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PowerSupply : Model
    {
        int powerSupplyId;
        string powerSupplyName;
        int powerSupplyPrice;
        int powerSupplyWatt;
        int powerSupplyCompanyId;

        public int PowerSupplyId
        {
            get { return this.powerSupplyId; }
            set { this.powerSupplyId = value; }
        }

        [Required(ErrorMessage = "You must enter power supply name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Power supply name must be between 2 and 40 characters")]
        public string PowerSupplyName
        {
            get { return this.powerSupplyName; }
            set
            {
                this.powerSupplyName = value;
                ValidateProperty(value, "PowerSupplyName");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Power supply price must be bigger than 0")]
        public int PowerSupplyPrice
        {
            get { return this.powerSupplyPrice; }
            set
            {
                this.powerSupplyPrice = value;
                ValidateProperty(value, "PowerSupplyPrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Power supply watt must be bigger than 0")]
        public int PowerSupplyWatt
        {
            get { return this.powerSupplyWatt; }
            set
            {
                this.powerSupplyWatt = value;
                ValidateProperty(value, "PowerSupplyWatt");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int PowerSupplyCompanyId
        {
            get { return this.powerSupplyCompanyId; }
            set
            {
                this.powerSupplyCompanyId = value;
                ValidateProperty(value, "PowerSupplyCompanyId");
            }
        }
    }
}