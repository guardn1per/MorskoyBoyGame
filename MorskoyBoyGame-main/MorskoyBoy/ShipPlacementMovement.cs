using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class ShipPlacementMovement : MovementManager
    {
        public static bool RotateShip(Ship ship, Command com)
        {
            var orientationIsVertical = ship.IsOrientationVerticalGet();

            if (com == Command.Rotate)
            {
                if (orientationIsVertical)
                    return false;

                return true;
            }

            return orientationIsVertical;
        }

        public static bool isPossibleToRotate(Ship ship)
        {
            var arena = ship.ParentArenaGet();
            var arenaDimensions = arena.GetArenaDimensions();
            var arenaArray = arena.GetArenaArray();
            var shipCoordinates = ship.GetShipCoordinates();

            if (ship.IsOrientationVerticalGet())
            {
                if (shipCoordinates.x + ship.DecksAmountGet() > arenaDimensions.x)
                    return false;
                return true;
            }


            if (shipCoordinates.y + ship.DecksAmountGet() > arenaDimensions.y)
                return false;
            return true;
        }

        public static bool isPossibleToGo(Ship ship, (int x, int y) newCoordinates)
        {
            var arena = ship.ParentArenaGet();
            var arenaArray = arena.GetArenaArray();
            var arenaDimensions = arena.GetArenaDimensions();

            if (ship.IsOrientationVerticalGet())
            {
                if (newCoordinates.y + ship.DecksAmountGet() > arenaDimensions.y || newCoordinates.x < 0 || newCoordinates.y < 0 || newCoordinates.x > arenaDimensions.x - 1)
                    return false;
                return true;
            }



            if (newCoordinates.x + ship.DecksAmountGet() > arenaDimensions.x || newCoordinates.x < 0 || newCoordinates.y < 0 || newCoordinates.y > arenaDimensions.y - 1)
                return false;
            return true;
        }

        public static bool isPossibleToPlace(Ship ship)
        {
            var arena = ship.ParentArenaGet();
            var arenaArray = arena.GetArenaArray();
            var arenaDimensions = arena.GetArenaDimensions();

            if (ship.IsOrientationVerticalGet())
            {
                for (int i = 0; i < ship.DecksAmountGet(); i++)
                {
                    if (arenaArray[ship.GetShipCoordinates().y + i, ship.GetShipCoordinates().x] != '.')
                        return false;
                }
                return true;
            }

            for (int i = 0; i < ship.DecksAmountGet(); i++)
            {
                if (arenaArray[ship.GetShipCoordinates().y, ship.GetShipCoordinates().x + i] != '.')
                    return false;
            }
            return true;
        }
    }
}
