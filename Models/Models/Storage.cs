using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Storage : Model
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

        [Required(ErrorMessage = "You must enter storage name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Storage name must be between 2 and 40 characters")]
        public string StorageName
        {
            get { return this.storageName; }
            set
            {
                this.storageName = value;
                ValidateProperty(value, "StorageName");
            }
        }

        [Required(ErrorMessage = "You must enter storage size")]
        public string StorageSize
        {
            get { return this.storageSize; }
            set
            {
                this.storageSize = value;
                ValidateProperty(value, "StorageSize");
            }
        }

        [Required(ErrorMessage = "You must enter storage speed")]
        public string StorageSpeed
        {
            get { return this.storageSpeed; }
            set
            {
                this.storageSpeed = value;
                ValidateProperty(value, "StorageSpeed");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose storage type")]
        public int StorageType
        {
            get { return this.storageType; }
            set
            {
                this.storageType = value;
                ValidateProperty(value, "StorageType");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Storage price must be bigger than 0")]
        public int StoragePrice
        {
            get { return this.storagePrice; }
            set
            {
                this.storagePrice = value;
                ValidateProperty(value, "StoragePrice");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int StorageCompanyId
        {
            get { return this.storageCompanyId; }
            set
            {
                this.storageCompanyId = value;
                ValidateProperty(value, "StorageCompanyId");
            }
        }
    }
}