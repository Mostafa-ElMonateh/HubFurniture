using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Specifications.ProductItemSpecifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 10;
        public string? Sort { get; set; }
        private string? search;

        public string? Search
        {
            get => search;
            set => search = value?.ToLower();
        }

        public string? CategoryName { get; set; }
        public string? ProductColor { get; set; }
        public int? MinimumPrice { get; set; }
        public int? MaximumPrice { get; set; }

        public int PageIndex { get; set; } = 1;

        private int pageSize = 5;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = value > 10 ? 10 : value;
        }

    }
}
