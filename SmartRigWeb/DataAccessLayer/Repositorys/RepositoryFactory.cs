using Models;

namespace SmartRigWeb
{
    public class RepositoryFactory
    {
        UserRepository userRepository;
        TypeRepository typeRepository;
        StorageRepository storageRepository;
        RamRepository ramRepository;
        PowerSupplyRepository powerSupplyRepository;
        OperatingSystemRepository operatingSystemRepository;
        MotherBoardRepository motherBoardRepository;
        GpuRepository gpuRepository;
        CpuFanRepository cpuFanRepository;
        CpuRepository cpuRepository;
        ComputerRepository computerRepository;
        CompanyRepository companyRepository;
        CitiesRepository citiesRepository;
        CaseRepository caseRepository;
        CartRepository cartRepository;

        OleDbConext dbContext;
        ModelsFactory modelsFactory;

        public RepositoryFactory(OleDbConext dbContext, ModelsFactory modelsFactory)
        {
            this.dbContext = dbContext;
            this.modelsFactory = modelsFactory;
        }

        public UserRepository UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new UserRepository(dbContext, modelsFactory);
                return this.userRepository;
            }
        }

        public TypeRepository TypeRepository
        {
            get
            {
                if (this.typeRepository == null)
                    this.typeRepository = new TypeRepository(dbContext, modelsFactory);
                return this.typeRepository;
            }
        }

        public StorageRepository StorageRepository
        {
            get
            {
                if (this.storageRepository == null)
                    this.storageRepository = new StorageRepository(dbContext, modelsFactory);
                return this.storageRepository;
            }
        }

        public RamRepository RamRepository
        {
            get
            {
                if (this.ramRepository == null)
                    this.ramRepository = new RamRepository(dbContext, modelsFactory);
                return this.ramRepository;
            }
        }

        public PowerSupplyRepository PowerSupplyRepository
        {
            get
            {
                if (this.powerSupplyRepository == null)
                    this.powerSupplyRepository = new PowerSupplyRepository(dbContext, modelsFactory);
                return this.powerSupplyRepository;
            }
        }

        public OperatingSystemRepository OperatingSystemRepository
        {
            get
            {
                if (this.operatingSystemRepository == null)
                    this.operatingSystemRepository = new OperatingSystemRepository(dbContext, modelsFactory);
                return this.operatingSystemRepository;
            }
        }

        public MotherBoardRepository MotherBoardRepository
        {
            get
            {
                if (this.motherBoardRepository == null)
                    this.motherBoardRepository = new MotherBoardRepository(dbContext, modelsFactory);
                return this.motherBoardRepository;
            }
        }

        public GpuRepository GpuRepository
        {
            get
            {
                if (this.gpuRepository == null)
                    this.gpuRepository = new GpuRepository(dbContext, modelsFactory);
                return this.gpuRepository;
            }
        }

        public CpuFanRepository CpuFanRepository
        {
            get
            {
                if (this.cpuFanRepository == null)
                    this.cpuFanRepository = new CpuFanRepository(dbContext, modelsFactory);
                return this.cpuFanRepository;
            }
        }

        public CpuRepository CpuRepository
        {
            get
            {
                if (this.cpuRepository == null)
                    this.cpuRepository = new CpuRepository(dbContext, modelsFactory);
                return this.cpuRepository;
            }
        }

        public ComputerRepository ComputerRepository
        {
            get
            {
                if (this.computerRepository == null)
                    this.computerRepository = new ComputerRepository(dbContext, modelsFactory);
                return this.computerRepository;
            }
        }

        public CompanyRepository CompanyRepository
        {
            get
            {
                if (this.companyRepository == null)
                    this.companyRepository = new CompanyRepository(dbContext, modelsFactory);
                return this.companyRepository;
            }
        }

        public CitiesRepository CitiesRepository
        {
            get
            {
                if (this.citiesRepository == null)
                    this.citiesRepository = new CitiesRepository(dbContext, modelsFactory);
                return this.citiesRepository;
            }
        }

        public CaseRepository CaseRepository
        {
            get
            {
                if (this.caseRepository == null)
                    this.caseRepository = new CaseRepository(dbContext, modelsFactory);
                return this.caseRepository;
            }
        }

        public CartRepository CartRepository
        {
            get
            {
                if (this.cartRepository == null)
                    this.cartRepository = new CartRepository(dbContext, modelsFactory);
                return this.cartRepository;
            }
        }
    }
}
