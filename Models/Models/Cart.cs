using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cart
    {
        int cartId;
        int userId;
        string date;
        bool isPayed;

        public int CartId
        {
            get { return this.cartId; }
            set { this.cartId = value; }
        }

        public int UserId
        {
            get { return this.userId; }
            set { this.userId = value; }
        }

        public string Date
        {
            get { return this.date; }
            set { this.date = value; }
        }

        public bool IsPayed
        {
            get { return this.isPayed; }
            set { this.isPayed = value; }
        }




    }
}
