using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CpuFan : Model
    {
        int cpuFanId;
        string cpuFanName;
        int cpuFanPrice;
        int cpuFanCompanyId;

        public int CpuFanId
        {
            get { return this.cpuFanId; }
            set { this.cpuFanId = value; }
        }

        [Required(ErrorMessage = "You must enter cpu fan name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Cpu fan name must be between 2 and 40 characters")]
        public string CpuFanName
        {
            get { return this.cpuFanName; }
            set
            {
                this.cpuFanName = value;
                ValidateProperty(value, "CpuFanName");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Cpu fan price must be bigger than 0")]
        public int CpuFanPrice
        {
            get { return this.cpuFanPrice; }
            set
            {
                this.cpuFanPrice = value;
                ValidateProperty(value, "CpuFanPrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int CpuFanCompanyId
        {
            get { return this.cpuFanCompanyId; }
            set
            {
                this.cpuFanCompanyId = value;
                ValidateProperty(value, "CpuFanCompanyId");
            }
        }
    }
}