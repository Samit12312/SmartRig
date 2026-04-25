using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class MotherBoard : Model
    {
        int motherBoardId;
        string motherBoardName;
        int motherBoardPrice;
        int motherBoardCompanyId;

        public int MotherBoardId
        {
            get { return this.motherBoardId; }
            set { this.motherBoardId = value; }
        }

        [Required(ErrorMessage = "You must enter motherboard name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Motherboard name must be between 2 and 40 characters")]
        public string MotherBoardName
        {
            get { return this.motherBoardName; }
            set
            {
                this.motherBoardName = value;
                ValidateProperty(value, "MotherBoardName");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Motherboard price must be bigger than 0")]
        public int MotherBoardPrice
        {
            get { return this.motherBoardPrice; }
            set
            {
                this.motherBoardPrice = value;
                ValidateProperty(value, "MotherBoardPrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int MotherBoardCompanyId
        {
            get { return this.motherBoardCompanyId; }
            set
            {
                this.motherBoardCompanyId = value;
                ValidateProperty(value, "MotherBoardCompanyId");
            }
        }
    }
}