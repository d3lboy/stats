using System;

namespace Stats.Common.Dto
{
    public class BaseDto
    {
        public string Source { get; set; }
        public Guid? CreatedBy { get; set; }
    }
}
