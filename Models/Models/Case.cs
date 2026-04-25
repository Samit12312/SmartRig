using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Case : Model
    {
        int caseId;
        string caseName;
        int casePrice;
        int caseCompanyId;

        public int CaseId
        {
            get { return this.caseId; }
            set { this.caseId = value; }
        }

        [Required(ErrorMessage = "You must enter case name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Case name must be between 2 and 40 characters")]
        public string CaseName
        {
            get { return this.caseName; }
            set
            {
                this.caseName = value;
                ValidateProperty(value, "CaseName");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Case price must be bigger than 0")]
        public int CasePrice
        {
            get { return this.casePrice; }
            set
            {
                this.casePrice = value;
                ValidateProperty(value, "CasePrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int CaseCompanyId
        {
            get { return this.caseCompanyId; }
            set
            {
                this.caseCompanyId = value;
                ValidateProperty(value, "CaseCompanyId");
            }
        }
    }
}