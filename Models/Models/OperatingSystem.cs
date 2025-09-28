using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class OperatingSystem
    {
        int operatingSystemId;
        string operatingSystemName;
        int operatingSystemPrice;
        int operatingSystemCompanyId;

        public int OperatingSystemId
        {
            get { return this.operatingSystemId; }
            set { this.operatingSystemId = value; }
        }

        public string OperatingSystemName
        {
            get { return this.operatingSystemName; }
            set { this.operatingSystemName = value; }
        }

        public int OperatingSystemPrice
        {
            get { return this.operatingSystemPrice; }
            set { this.operatingSystemPrice = value; }
        }

        public int OperatingSystemCompanyId
        {
            get { return this.operatingSystemCompanyId; }
            set { this.operatingSystemCompanyId = value; }
        }



    }
}
