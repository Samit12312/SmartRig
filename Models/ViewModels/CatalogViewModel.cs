using System;
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

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        public int? PriceSort { get; set; }

        public Dictionary<string, string> Currencies { get; set; } = new Dictionary<string, string>();
        public string CurrencyCode { get; set; } = "ILS";
        public string CurrencySymbol { get; set; } = "₪";

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 6;
        public int TotalItems { get; set; }

        public int TotalPages
        {
            get
            {
                if (PageSize <= 0)
                {
                    return 1;
                }

                int pages = (int)Math.Ceiling((double)TotalItems / PageSize);

                if (pages < 1)
                {
                    return 1;
                }

                return pages;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return Page > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return Page < TotalPages;
            }
        }
    }
}