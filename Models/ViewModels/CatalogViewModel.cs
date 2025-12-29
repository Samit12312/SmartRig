using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CatalogViewModel
    {
        public List<ComputerCatalogViewModel> Computers { get; set; }

        // Filters
        public List<Type> Types { get; set; }
        public List<Company> Companies { get; set; }
        public List<OperatingSystem> OperatingSystems { get; set; }
    }
}

