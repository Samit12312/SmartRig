using System.Collections.Generic;

namespace Models
{
    public class CatalogViewModel
    {
        public List<ComputerCatalogViewModel>? Computers { get; set; }

        public List<Type>? Types { get; set; }
        public int? TypeId { get; set; }

        public List<Company>? Companys { get; set; }
        public int? CompanyId { get; set; }   

        public List<OperatingSystem>? operatingSystems { get; set; }
        public int? OperatingSystemId { get; set; }

        // Price filters
        public int? MinPrice { get; set; }   
        public int? MaxPrice { get; set; }       

        // Price sorting
        public int? PriceSort { get; set; }    
    }
}
