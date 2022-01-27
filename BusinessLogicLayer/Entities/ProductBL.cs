using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities
{
    public class ProductBL
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public bool OnSale { get; set; }
        public int Count { get; set; }
    }
}
