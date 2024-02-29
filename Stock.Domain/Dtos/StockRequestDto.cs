using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Dtos
{
    public class StockRequestDto
    {
        public string Symbol { get; set; }
        public string CompanyName { get; set; }
        public decimal Price { get; set; }
        
    }

}
