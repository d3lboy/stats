using System;
using Stats.Common.Enums;

namespace Stats.Common.Dto
{
    public class ReferenceDto : BaseDto
    {
        public Guid Id { get; set; }
        public Competition Competition { get; set; }
        public ReferenceType Type { get; set; }
        public string LocalValue { get; set; }
        public string ReferenceValue { get; set; }
    }
}
