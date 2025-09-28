using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cpu
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

        public string CpuName
        {
            get { return this.cpuName; }
            set { this.cpuName = value; }
        }

        public int CpuPrice
        {
            get { return this.cpuPrice; }
            set { this.cpuPrice = value; }
        }

        public int CpuCompanyId
        {
            get { return this.cpuCompanyId; }
            set { this.cpuCompanyId = value; }
        }



    }
}
