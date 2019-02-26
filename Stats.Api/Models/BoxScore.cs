using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stats.Api.Models
{
    public class BoxScore : BaseModel
    {
        public Guid Id { get; set; }
        public Player Player { get; set; }
        public Game Game { get; set; }

        public ushort SecondsPlayed { get; set; }

        public ushort Fg1M { get; set; }
        public ushort Fg1A { get; set; }

        public ushort Fg2M { get; set; }
        public ushort Fg2A { get; set; }

        public ushort Fg3M { get; set; }
        public ushort Fg3A { get; set; }

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
