using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class MovementManager
    {
        public static (int x, int y) MakeAMove(Command com, (int x, int y) currentCoordinates)
        {
            if (com == Command.Right)
                return (currentCoordinates.x + 1, currentCoordinates.y);
            if (com == Command.Left)
                return (currentCoordinates.x - 1, currentCoordinates.y);
            if (com == Command.Up)
                return (currentCoordinates.x, currentCoordinates.y - 1);
            if (com == Command.Down)
                return (currentCoordinates.x, currentCoordinates.y + 1);
            return (currentCoordinates);
        }

        public static bool isPossibleToMove((int x, int y) newCoordinates, Arena arena)
        {
            var arenaDimensions = arena.GetArenaDimensions();

            if (newCoordinates.x < 0 || newCoordinates.y < 0 || newCoordinates.x >= arenaDimensions.x || newCoordinates.y >= arenaDimensions.y)
                return false;
            return true;
        }

        public static bool isPossibleToChosseAPoint((int x, int y) newCoordinates, Arena arena)
        {
            var arenaArray = arena.GetArenaArray();
            if (arenaArray[newCoordinates.y, newCoordinates.x] == arena.GetHitChar() || arenaArray[newCoordinates.y, newCoordinates.x] == arena.GetMissChar())
                return false;
            return true;
        }
    }

}
