using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystemStore.Domain.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public double Price { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public int CategoryRef { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
