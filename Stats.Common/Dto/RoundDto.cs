using System;
using Stats.Common.Enums;

namespace Stats.Common.Dto
{
    public class RoundDto : BaseDto
    {
        public Guid Id { get; set; }
        public int RoundNumber { get; set; }
        public Guid Season { get; set; }
        public RoundType RoundType { get; set; }
    }
}
