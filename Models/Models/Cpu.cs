using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Cpu : Model
    {
        int cpuId;
        string cpuName;
        int cpuPrice;
        int cpuCompanyId;

        public int CpuId
        {
            get { return this.cpuId; }
            set { this.cpuId = value; }
        }

        [Required(ErrorMessage = "You must enter cpu name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Cpu name must be between 2 and 40 characters")]
        public string CpuName
        {
            get { return this.cpuName; }
            set
            {
                this.cpuName = value;
                ValidateProperty(value, "CpuName");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Cpu price must be bigger than 0")]
        public int CpuPrice
        {
            get { return this.cpuPrice; }
            set
            {
                this.cpuPrice = value;
                ValidateProperty(value, "CpuPrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int CpuCompanyId
        {
            get { return this.cpuCompanyId; }
            set
            {
                this.cpuCompanyId = value;
                ValidateProperty(value, "CpuCompanyId");
            }
        }
    }
}