using System;

namespace Stats.Fetcher.Library.Dto
{
    public class Player
    {
        public Guid Guid { get; set; }
        public Guid League { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Position { get; set; }
        public ushort Height { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
