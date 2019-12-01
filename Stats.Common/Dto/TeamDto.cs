using System;
using Stats.Common.Enums;

namespace Stats.Common.Dto
{
    public class TeamDto : BaseDto
    {
        public Guid Id { get; set; }
        public string ReferenceId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Address { get; set; }
        public Competition Competition { get; set; }
        public Country? Country { get; set; }
    }
}
