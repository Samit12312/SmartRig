using Models;


namespace SmartRigWeb
{
    public class ModelsFactory
    {
        UserCreator userCreator;
        TypeCreator typeCreator;
        StorageCreator storageCreator;
        RamCreator ramCreator;
        PowerSupplyCreator powerSupplyCreator;
        OperatingSystemCreator operatingSystemCreator;
        MotherBoardCreator motherBoardCreator;
        GpuCreator gpuCreator;
        CpuFanCreator cpuFanCreator;
        CpuCreator cpuCreator;
        ComputerCreator computerCreator;
        CompanyCreator companyCreator;
        CitiesCreator citiesCreator;
        CaseCreator caseCreator;
        CartCreator cartCreator;
        CartComputerCreator cartComputer;
        public CartComputerCreator CartComputerCreator
        {
            get
            {
                if (this.cartComputer== null) 
                    this.cartComputer = new CartComputerCreator();
                return this.cartComputer;
            }
        }

        public UserCreator UserCreator
        {
            get
            {
                if (this.userCreator == null)
                    this.userCreator = new UserCreator();
                return this.userCreator;
            }
        }
        public TypeCreator TypeCreator
        {
            get
            {
                if (this.typeCreator == null)
                    this.typeCreator = new TypeCreator();
                return this.typeCreator;
            }
        }
        public StorageCreator StorageCreator
        {
            get
            {
                if (this.storageCreator == null)
                    this.storageCreator = new StorageCreator(); 
                return this.storageCreator;
            }
        }
        public RamCreator RamCreator
        {
            get
            {
                if(this.ramCreator == null)
                    this.ramCreator = new RamCreator();
                return this.ramCreator;
            }
        }
        public PowerSupplyCreator PowerSupplyCreator
        {
            get
            {
                if (this.powerSupplyCreator == null) 
                    this.powerSupplyCreator = new PowerSupplyCreator();
                return this.powerSupplyCreator;
            }
        }
        public OperatingSystemCreator OperatingSystemCreator
        {
            get
            {
                if (this.operatingSystemCreator == null)
                    this.operatingSystemCreator = new OperatingSystemCreator();
                return this.operatingSystemCreator;
            }
        }
        public MotherBoardCreator MotherBoardCreator
        {
            get
            {
                if (this.motherBoardCreator == null)
                    this.motherBoardCreator = new MotherBoardCreator();
                return this.motherBoardCreator;
            }
        }
        public GpuCreator GpuCreator
        {
            get
            {
                if(this.gpuCreator == null)
                    this.gpuCreator = new GpuCreator();
                return this.gpuCreator;
            }
        }
        public CpuFanCreator CpuFanCreator
        {
            get
            {
                if( this.cpuFanCreator == null)
                    this.cpuFanCreator = new CpuFanCreator();
                return this.cpuFanCreator;
            }
        }
        public CpuCreator CpuCreator
        {
            get
            {
                if(this.cpuCreator == null)
                    this.cpuCreator = new CpuCreator();
                return this.cpuCreator;
            }
        }
        public ComputerCreator ComputerCreator
        {
            get
            {
                if(computerCreator == null)
                    computerCreator = new ComputerCreator();
                return computerCreator;
            }
        }
        public CompanyCreator CompanyCreator
        {
            get
            {
                if(this.companyCreator== null)
                    this.companyCreator = new CompanyCreator();
                return this.companyCreator;
            }
        }
        public CitiesCreator CitiesCreator
        {
            get
            {
                if(this.citiesCreator == null)
                    this.citiesCreator = new CitiesCreator();
                return this.citiesCreator;
            }
        }
        public CaseCreator CaseCreator
        {
            get
            {
                if(this.caseCreator == null)
                    this.caseCreator = new CaseCreator();
                return this.caseCreator;
            }
        }
        public CartCreator CartCreator
        {
            get
            {
                if(this.cartCreator == null)
                    this.cartCreator = new CartCreator();
                return this.cartCreator;
            }
        }
    }
}
