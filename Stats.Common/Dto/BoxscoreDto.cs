using System;

namespace Stats.Common.Dto
{
    public class BoxscoreDto
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerReference { get; set; }

        public Guid GameId { get; set; }

        public ushort SecondsPlayed { get; set; }

        public Shoot One { get; set; }
        public Shoot Two { get; set; }
        public Shoot Three { get; set; }

        public ushort RebsO { get; set; }
        public ushort RebsD { get; set; }

        public ushort Ass { get; set; }
        public ushort St { get; set; }
        public ushort To { get; set; }

        public ushort BlckFv { get; set; }
        public ushort BlckAg { get; set; }

        public ushort FoulCm { get; set; }
        public ushort FoulRv { get; set; }

        public ushort Val { get; set; }
    }
}
