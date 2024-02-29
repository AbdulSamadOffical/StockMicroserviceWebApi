using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade.Domain.Interfaces
{
    public interface ICurrentContext
    {
        public string LoggedInUser { get; set; }
    }
}
