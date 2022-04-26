using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class Ship
    {

        public Ship(int decksAmount, Arena arena)
        {
            orientationIsVertical = true;
            DecksAmount = decksAmount;
            ParentArena = arena;
            shipCoordinates = (0, 0);
        }


        private bool orientationIsVertical;
        public void ChangeOrientation()
        {
            if (orientationIsVertical)
            {
                orientationIsVertical = false;
                return;
            }

            orientationIsVertical = true;
        }
        public bool IsOrientationVerticalGet() => orientationIsVertical;


        private int DecksAmount;
        public int DecksAmountGet() => DecksAmount;

        private Arena ParentArena; 
        public Arena ParentArenaGet() => ParentArena;

        private (int x, int y) shipCoordinates; //shipsCoordinates are anchored to its top-left cell
        public (int x, int y) GetShipCoordinates()
        {
            (int x, int y) res = (shipCoordinates.x, shipCoordinates.y);
            return res;
        }

        public void Place()
        {
            ParentArena.UpdateArenaToDisplay(this);

            while (true)
            {
                char input = Console.ReadKey().KeyChar;

                var newCoordinates = ShipPlacementMovement.MakeAMove(InputToCommand.GetCommand(input), shipCoordinates);
                var newRotation = ShipPlacementMovement.RotateShip(this, InputToCommand.GetCommand(input));

                if (ShipPlacementMovement.isPossibleToRotate(this))
                    orientationIsVertical = newRotation;
                if(ShipPlacementMovement.isPossibleToGo(this, newCoordinates))
                    shipCoordinates = newCoordinates;

                ParentArena.UpdateArenaToDisplay(this);

                if (InputToCommand.GetCommand(input) == Command.ChooseAPoint)
                {
                    if (ShipPlacementMovement.isPossibleToPlace(this)) {
                        ParentArena.AddShip(this);
                        return;
                    }

                    Console.WriteLine("Cannot place ship here");
                }
            }
        }
    }
}
