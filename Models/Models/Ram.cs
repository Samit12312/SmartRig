using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Ram : Model
    {
        int ramId;
        string ramName;
        string ramSize;
        int ramTypeId;
        string ramSpeed;
        int ramPrice;
        int ramCompanyId;

        public int RamId
        {
            get { return this.ramId; }
            set { this.ramId = value; }
        }

        [Required(ErrorMessage = "You must enter ram name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Ram name must be between 2 and 40 characters")]
        public string RamName
        {
            get { return this.ramName; }
            set
            {
                this.ramName = value;
                ValidateProperty(value, "RamName");
            }
        }

        [Required(ErrorMessage = "You must enter ram size")]
        public string RamSize
        {
            get { return this.ramSize; }
            set
            {
                this.ramSize = value;
                ValidateProperty(value, "RamSize");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose ram type")]
        public int RamTypeId
        {
            get { return this.ramTypeId; }
            set
            {
                this.ramTypeId = value;
                ValidateProperty(value, "RamTypeId");
            }
        }

        [Required(ErrorMessage = "You must enter ram speed")]
        public string RamSpeed
        {
            get { return this.ramSpeed; }
            set
            {
                this.ramSpeed = value;
                ValidateProperty(value, "RamSpeed");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Ram price must be bigger than 0")]
        public int RamPrice
        {
            get { return this.ramPrice; }
            set
            {
                this.ramPrice = value;
                ValidateProperty(value, "RamPrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int RamCompanyId
        {
            get { return this.ramCompanyId; }
            set
            {
                this.ramCompanyId = value;
                ValidateProperty(value, "RamCompanyId");
            }
        }
    }
}