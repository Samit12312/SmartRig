using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CartComputer
    {
        public int computerId;
        public string computerName;
        public int computerPrice;
        public int computerQuantity;
        public string computerPicture;
        public int ComputerId
        {
            get { return this.computerId; }
            set { this.computerId = value; }
        }

        public string ComputerName
        {
            get { return this.computerName; }
            set { this.computerName = value; }
        }

        public int ComputerPrice
        {
            get { return this.computerPrice; }
            set { this.computerPrice = value; }
        }

        public int ComputerQuantity
        {
            get { return this.computerQuantity; }
            set { this.computerQuantity = value; }
        }

        public string ComputerPicture
        {
            get { return this.computerPicture; }
            set { this.computerPicture = value; }
        }


    }
}
