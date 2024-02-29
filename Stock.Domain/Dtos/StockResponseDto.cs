using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Dtos
{
    public class StockResponseDto<T>
    {
        public string? Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
    public class StockResponseDtoList<T>
    {
        public string? Message { get; set; } = string.Empty;
        public IEnumerable<T>? Data { get; set; }
    }
}
