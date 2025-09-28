using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CpuFan
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

        public string CpuFanName
        {
            get { return this.cpuFanName; }
            set { this.cpuFanName = value; }
        }

        public int CpuFanPrice
        {
            get { return this.cpuFanPrice; }
            set { this.cpuFanPrice = value; }
        }

        public int CpuFanCompanyId
        {
            get { return this.cpuFanCompanyId; }
            set { this.cpuFanCompanyId = value; }
        }




    }
}
