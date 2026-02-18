namespace Models
{
    public class NewComputerViewModel
    {
        public Computer Computer { get; set; }
        public List<Cpu> Cpus { get; set; }
        public List<Gpu> Gpus { get; set; }
        public List<Ram> Rams { get; set; }
        public List<Storage> Storages { get; set; }
        public List<MotherBoard> Motherboards { get; set; }
        public List<PowerSupply> PowerSupplies { get; set; }
        public List<CpuFan> Fans { get; set; }
        public List<Case> Cases { get; set; }
        public List<Models.Type> Types { get; set; }
        public List<OperatingSystem> OS { get; set; }
        public List<Company> Companies { get; set; }
    }
}