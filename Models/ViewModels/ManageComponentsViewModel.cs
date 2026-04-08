using System.Collections.Generic;

namespace Models.ViewModels
{
    public class CpuManageViewModel
    {
        public Cpu cpu { get; set; }
        public Company company { get; set; }
    }

    public class GpuManageViewModel
    {
        public Gpu gpu { get; set; }
        public Company company { get; set; }
    }

    public class RamManageViewModel
    {
        public Ram ram { get; set; }
        public Company company { get; set; }
        public Models.Type type { get; set; }
    }

    public class StorageManageViewModel
    {
        public Storage storage { get; set; }
        public Company company { get; set; }
        public Models.Type type { get; set; }
    }

    public class MotherBoardManageViewModel
    {
        public MotherBoard motherBoard { get; set; }
        public Company company { get; set; }
    }

    public class CpuFanManageViewModel
    {
        public CpuFan cpuFan { get; set; }
        public Company company { get; set; }
    }

    public class PowerSupplyManageViewModel
    {
        public PowerSupply powerSupply { get; set; }
        public Company company { get; set; }
    }

    public class CaseManageViewModel
    {
        public Case computerCase { get; set; }
        public Company company { get; set; }
    }

    public class OperatingSystemManageViewModel
    {
        public Models.OperatingSystem operatingSystem { get; set; }
        public Company company { get; set; }
    }

    public class ManageComponentsViewModel
    {
        public List<CpuManageViewModel> cpus { get; set; }
        public List<GpuManageViewModel> gpus { get; set; }
        public List<RamManageViewModel> rams { get; set; }
        public List<StorageManageViewModel> storages { get; set; }
        public List<MotherBoardManageViewModel> motherBoards { get; set; }
        public List<CpuFanManageViewModel> cpuFans { get; set; }
        public List<PowerSupplyManageViewModel> powerSupplies { get; set; }
        public List<CaseManageViewModel> cases { get; set; }
        public List<OperatingSystemManageViewModel> operatingSystems { get; set; }

        public ManageComponentsViewModel()
        {
            cpus = new List<CpuManageViewModel>();
            gpus = new List<GpuManageViewModel>();
            rams = new List<RamManageViewModel>();
            storages = new List<StorageManageViewModel>();
            motherBoards = new List<MotherBoardManageViewModel>();
            cpuFans = new List<CpuFanManageViewModel>();
            powerSupplies = new List<PowerSupplyManageViewModel>();
            cases = new List<CaseManageViewModel>();
            operatingSystems = new List<OperatingSystemManageViewModel>();
        }
    }
}