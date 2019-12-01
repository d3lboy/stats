using System;
using Newtonsoft.Json;

namespace Stats.Common.Dto
{
    public class BaseDto
    {
        public Guid? CreatedBy { get; set; }
        public DateTime? Timestamp { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
