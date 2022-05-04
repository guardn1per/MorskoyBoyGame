using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class Ship
    {

        public Ship(int decksAmount)
        {
            orientationIsVertical = true;
            DecksAmount = decksAmount;
            shipCoordinates = (0, 0);
        }


        private bool orientationIsVertical;
        public void SetOrientation(bool isOrientationVertical)
        {
            orientationIsVertical=isOrientationVertical;
        }
        public bool IsOrientationVerticalGet() => orientationIsVertical;


        private int DecksAmount;
        public int DecksAmountGet() => DecksAmount;

        private (int x, int y) shipCoordinates; //shipsCoordinates are anchored to its top-left cell
        public (int x, int y) GetShipCoordinates()
        {
            (int x, int y) res = (shipCoordinates.x, shipCoordinates.y);
            return res;
        }
        public void SetShipCoordinates((int x, int y) coordinates)
        {
            shipCoordinates.x = coordinates.x; shipCoordinates.y = coordinates.y;
        }
    }
}
