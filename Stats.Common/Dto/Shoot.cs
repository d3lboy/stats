using System;
using System.Collections.Generic;
using System.Text;

namespace Stats.Common.Dto
{
    public class Shoot
    {
        public ushort Made { get; }
        public ushort Attempted { get; }

        public Shoot(ushort made, ushort attempted)
        {
            Made = made;
            Attempted = attempted;
        }
    }
}
