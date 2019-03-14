using System;

namespace Stats.Common.Enums
{
    [Flags]
    public enum PlayerPosition
    {
        PointGuard = 1 << 0,
        ShootingGuard = 1 << 1,
        SmallForward = 1 << 2,
        PowerForward = 1 << 3,
        Center = 1 << 4
    }
}
