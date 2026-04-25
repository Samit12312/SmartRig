using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Company : Model
    {
        int companyId;
        string companyName;

        public int CompanyId
        {
            get { return this.companyId; }
            set { this.companyId = value; }
        }

        [Required(ErrorMessage = "You must enter company name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Company name must be between 2 and 30 characters")]
        public string CompanyName
        {
            get { return this.companyName; }
            set
            {
                this.companyName = value;
                ValidateProperty(value, "CompanyName");
            }
        }
    }
}