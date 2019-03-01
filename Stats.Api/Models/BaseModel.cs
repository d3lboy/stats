using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stats.Api.Models
{
    public class BaseModel
    {
        public DateTime Timestamp { get; set; }

        public BaseModel()
        {
            Timestamp = DateTime.Now;
        }
    }
}
