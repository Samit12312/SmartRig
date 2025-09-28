using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PowerSupply
    {
        int powerSupplyId;
        string powerSupplyName;
        int powerSupplyPrice;
        int powerSupplyWatt;
        int powerSupplyCompanyId;
        public string PowerSupplyName
        {
            get { return this.powerSupplyName; }
            set { this.powerSupplyName = value; }
        }

        public int PowerSupplyPrice
        {
            get { return this.powerSupplyPrice; }
            set { this.powerSupplyPrice = value; }
        }

        public int PowerSupplyWatt
        {
            get { return this.powerSupplyWatt; }
            set { this.powerSupplyWatt = value; }
        }

        public int PowerSupplyCompanyId
        {
            get { return this.powerSupplyCompanyId; }
            set { this.powerSupplyCompanyId = value; }
        }



    }
}
