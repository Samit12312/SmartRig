using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CatalogViewModel
    {
        public List<Computer>? AllComputers { get; set; }
        public List<Type>? types { get; set; }
        public List<Company>? Companys { get; set; }
        public List<OperatingSystem>? operatingSystems { get; set; }
    }
}
