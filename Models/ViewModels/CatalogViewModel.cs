using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CatalogViewModel
    {
        public List<Computer>? Computers { get; set; }
        public List<Type>? Types { get; set; }
        public int? TypeId { get; set; }
        public List<Company>? Companys { get; set; }
        public List<OperatingSystem>? operatingSystems { get; set; }
    }
}
