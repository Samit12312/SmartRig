using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Storage
    {
        int storageId;
        string storageName;
        string storageSize;
        string storageSpeed;
        int storageType;
        int storagePrice;
        int storageCompanyId;

        public int StorageId
        {
            get { return this.storageId; }
            set { this.storageId = value; }
        }

        public string StorageName
        {
            get { return this.storageName; }
            set { this.storageName = value; }
        }

        public string StorageSize
        {
            get { return this.storageSize; }
            set { this.storageSize = value; }
        }

        public string StorageSpeed
        {
            get { return this.storageSpeed; }
            set { this.storageSpeed = value; }
        }

        public int StorageType
        {
            get { return this.storageType; }
            set { this.storageType = value; }
        }

        public int StoragePrice
        {
            get { return this.storagePrice; }
            set { this.storagePrice = value; }
        }

        public int StorageCompanyId
        {
            get { return this.storageCompanyId; }
            set { this.storageCompanyId = value; }
        }



    }
}
