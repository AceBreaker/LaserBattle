using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaserBattle
{
    static class Tags
    {
        public static readonly ushort CreatePlayerTag = 0;
        public static readonly ushort MovePieceTag = 1;
        public static readonly ushort DespawnPlayerTag = 2;
        public static readonly ushort EndTurnTag = 3;
    }
}
