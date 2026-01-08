using System.Collections.Generic;

namespace Models
{
    public class CatalogViewModel
    {
        public List<ComputerCatalogViewModel>? Computers { get; set; }

        public List<Type>? Types { get; set; }
        public int? TypeId { get; set; }
        public List<Company>? Companys { get; set; }
        public List<OperatingSystem>? operatingSystems { get; set; }
    }
}
