using System;

namespace Models
{
    public class Type
    {
        int typeId;
        string typeName;
        int typeCode; // 1 = Work/gaming/laptop, 2 = RAM type (DDR4/DDR3), 3 = storage type (SSD/NVMe2)

        public int TypeId
        {
            get { return this.typeId; }
            set { this.typeId = value; }
        }

        public string TypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
        }

        public int TypeCode
        {
            get { return this.typeCode; }
            set { this.typeCode = value; }
        }
    }
}
