using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Company
    {
        int companyId;
        string companyName;
        public int CompanyId
        {
            get { return this.companyId; }
            set { this.companyId = value; }
        }

        public string CompanyName
        {
            get { return this.companyName; }
            set { this.companyName = value; }
        }


    }
}
