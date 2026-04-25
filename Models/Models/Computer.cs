using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Computer : Model
    {
        int computerId;
        string computerName;
        int computerTypeId;
        int companyId;
        int storageId;
        int ramId;
        int cpuId;
        int gpuId;
        int price;
        int operatingSystemId;
        int caseId;
        int powerSupplyId;
        int cpuFanId;
        int motherBoardId;
        string computerPicture;

        public int ComputerId
        {
            get { return this.computerId; }
            set { this.computerId = value; }
        }

        [Required(ErrorMessage = "You must enter computer name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Computer name must be between 2 and 40 characters")]
        public string ComputerName
        {
            get { return this.computerName; }
            set
            {
                this.computerName = value;
                ValidateProperty(value, "ComputerName");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose computer type")]
        public int ComputerTypeId
        {
            get { return this.computerTypeId; }
            set
            {
                this.computerTypeId = value;
                ValidateProperty(value, "ComputerTypeId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose company")]
        public int CompanyId
        {
            get { return this.companyId; }
            set
            {
                this.companyId = value;
                ValidateProperty(value, "CompanyId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose storage")]
        public int StorageId
        {
            get { return this.storageId; }
            set
            {
                this.storageId = value;
                ValidateProperty(value, "StorageId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose ram")]
        public int RamId
        {
            get { return this.ramId; }
            set
            {
                this.ramId = value;
                ValidateProperty(value, "RamId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose cpu")]
        public int CpuId
        {
            get { return this.cpuId; }
            set
            {
                this.cpuId = value;
                ValidateProperty(value, "CpuId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose gpu")]
        public int GpuId
        {
            get { return this.gpuId; }
            set
            {
                this.gpuId = value;
                ValidateProperty(value, "GpuId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Price must be bigger than 0")]
        public int Price
        {
            get { return this.price; }
            set
            {
                this.price = value;
                ValidateProperty(value, "Price");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose operating system")]
        public int OperatingSystemId
        {
            get { return this.operatingSystemId; }
            set
            {
                this.operatingSystemId = value;
                ValidateProperty(value, "OperatingSystemId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose case")]
        public int CaseId
        {
            get { return this.caseId; }
            set
            {
                this.caseId = value;
                ValidateProperty(value, "CaseId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose power supply")]
        public int PowerSupplyId
        {
            get { return this.powerSupplyId; }
            set
            {
                this.powerSupplyId = value;
                ValidateProperty(value, "PowerSupplyId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose cpu fan")]
        public int CpuFanId
        {
            get { return this.cpuFanId; }
            set
            {
                this.cpuFanId = value;
                ValidateProperty(value, "CpuFanId");
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "You must choose motherboard")]
        public int MotherBoardId
        {
            get { return this.motherBoardId; }
            set
            {
                this.motherBoardId = value;
                ValidateProperty(value, "MotherBoardId");
            }
        }

        [Required(ErrorMessage = "You must choose computer picture")]
        public string ComputerPicture
        {
            get { return this.computerPicture; }
            set
            {
                this.computerPicture = value;
                ValidateProperty(value, "ComputerPicture");
            }
        }
    }
}