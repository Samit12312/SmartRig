using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Ram
    {
        int ramId;
        string ramName;
        string ramSize;
        int ramTypeId;
        string ramSpeed;
        int ramPrice;
        int ramCompanyId;

        public string RamName
        {
            get { return this.ramName; }
            set { this.ramName = value; }
        }

        public string RamSize
        {
            get { return this.ramSize; }
            set { this.ramSize = value; }
        }

        public int RamTypeId
        {
            get { return this.ramTypeId; }
            set { this.ramTypeId = value; }
        }

        public string RamSpeed
        {
            get { return this.ramSpeed; }
            set { this.ramSpeed = value; }
        }

        public int RamPrice
        {
            get { return this.ramPrice; }
            set { this.ramPrice = value; }
        }

        public int RamCompanyId
        {
            get { return this.ramCompanyId; }
            set { this.ramCompanyId = value; }
        }



    }
}
