using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Gpu
    {
        int gpuId;
        string gpuName;
        string gpuSize;
        string gpuSpeed;
        int gpuPrice;
        int gpuCompanyId;

        public string GpuName
        {
            get { return this.gpuName; }
            set { this.gpuName = value; }
        }

        public string GpuSize
        {
            get { return this.gpuSize; }
            set { this.gpuSize = value; }
        }

        public string GpuSpeed
        {
            get { return this.gpuSpeed; }
            set { this.gpuSpeed = value; }
        }

        public int GpuPrice
        {
            get { return this.gpuPrice; }
            set { this.gpuPrice = value; }
        }

        public int GpuCompanyId
        {
            get { return this.gpuCompanyId; }
            set { this.gpuCompanyId = value; }
        }



    }
}
