using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ComputerDetailsViewModel
    {
        public Computer computer { get; set; }
        public Ram ram { get; set; }
        public Company company { get; set; }
        public Gpu gpu { get; set; }
        public OperatingSystem operatingSystem { get; set; }
        public CpuFan cpuFan { get; set; }
        public Cpu cpu { get; set; }
        public PowerSupply powerSupply { get; set; }
        public MotherBoard motherBoard { get; set; }
        public Case computerCase { get; set; }
        public Type type { get; set; }
    }
}
