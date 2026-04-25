using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Gpu : Model
    {
        int gpuId;
        string gpuName;
        string gpuSize;
        string gpuSpeed;
        int gpuPrice;
        int gpuCompanyId;

        public int GpuId
        {
            get { return this.gpuId; }
            set { this.gpuId = value; }
        }

        [Required(ErrorMessage = "You must enter gpu name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Gpu name must be between 2 and 40 characters")]
        public string GpuName
        {
            get { return this.gpuName; }
            set
            {
                this.gpuName = value;
                ValidateProperty(value, "GpuName");
            }
        }

        [Required(ErrorMessage = "You must enter gpu size")]
        public string GpuSize
        {
            get { return this.gpuSize; }
            set
            {
                this.gpuSize = value;
                ValidateProperty(value, "GpuSize");
            }
        }

        [Required(ErrorMessage = "You must enter gpu speed")]
        public string GpuSpeed
        {
            get { return this.gpuSpeed; }
            set
            {
                this.gpuSpeed = value;
                ValidateProperty(value, "GpuSpeed");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Gpu price must be bigger than 0")]
        public int GpuPrice
        {
            get { return this.gpuPrice; }
            set
            {
                this.gpuPrice = value;
                ValidateProperty(value, "GpuPrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int GpuCompanyId
        {
            get { return this.gpuCompanyId; }
            set
            {
                this.gpuCompanyId = value;
                ValidateProperty(value, "GpuCompanyId");
            }
        }
    }
}