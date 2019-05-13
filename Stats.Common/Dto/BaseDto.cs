using System;

namespace Stats.Common.Dto
{
    public class BaseDto
    {
        public Guid? CreatedBy { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
