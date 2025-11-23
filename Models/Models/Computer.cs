using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Computer
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

        public string ComputerName
        {
            get { return this.computerName; }
            set { this.computerName = value; }
        }

        public int ComputerTypeId
        {
            get { return this.computerTypeId; }
            set { this.computerTypeId = value; }
        }

        public int CompanyId
        {
            get { return this.companyId; }
            set { this.companyId = value; }
        }

        public int StorageId
        {
            get { return this.storageId; }
            set { this.storageId = value; }
        }

        public int RamId
        {
            get { return this.ramId; }
            set { this.ramId = value; }
        }

        public int CpuId
        {
            get { return this.cpuId; }
            set { this.cpuId = value; }
        }

        public int GpuId
        {
            get { return this.gpuId; }
            set { this.gpuId = value; }
        }

        public int Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public int OperatingSystemId
        {
            get { return this.operatingSystemId; }
            set { this.operatingSystemId = value; }
        }

        public int CaseId
        {
            get { return this.caseId; }
            set { this.caseId = value; }
        }

        public int PowerSupplyId
        {
            get { return this.powerSupplyId; }
            set { this.powerSupplyId = value; }
        }

        public int CpuFanId
        {
            get { return this.cpuFanId; }
            set { this.cpuFanId = value; }
        }

        public int MotherBoardId
        {
            get { return this.motherBoardId; }
            set { this.motherBoardId = value; }
        }

        public string ComputerPicture
        {
            get { return this.computerPicture; }
            set { this.computerPicture = value; }
        }




    }
}
