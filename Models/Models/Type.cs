using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Type
    {
        int typeId;
        string typeName;
        int TypeCode; // 1 = Work/gaming/laptop 2 = ram type ddr4/ddr3 3 = storagetype ssd/nvme2

        public string TypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
        }

        public int TypeCode
        {
            get { return this.TypeCode; }
            set { this.TypeCode = value; }
        }



    }
}
