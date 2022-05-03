using System;

namespace MorskoyBoy
{
    internal class Arena
    {

        public Arena(int x, int y, int fourDeck, int threeDeck, int twoDeck, int oneDeck)
        {
            arenaDimensions.x = x;
            arenaDimensions.y = y;
            CreateClearArena(x, y);
            AddShipsToArray(fourDeck, threeDeck, twoDeck, oneDeck);
            arenaToDisplayForEnemy = arena.Clone() as char[,];
        }


        private char[,] arena;
        public char[,] GetArenaArray()
        {
            char[,] res = arena.Clone() as char[,];

            return res;
        }
        private char[,] arenaToDisplay;
        public char[,] GetArenaToDisplay() =>  arenaToDisplay.Clone() as char[,];
        private char[,] arenaToDisplayForEnemy;

        private (int x, int y) arenaDimensions;

        public (int x, int y) GetArenaDimensions()
        {
            (int x, int y) res = (arenaDimensions.x, arenaDimensions.y);
            return res;
        }


        private Ship[] ships;
        private int shipDecksCount;

        public void SetArena()
        {
            PlaceShips();
        }

        private void CreateClearArena(int x, int y)
        {
            arena = new char[y, x];

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    arena[i, j] = '.';
                }
            }
        }
        private void AddShipsToArray(int fourDeck, int threeDeck, int twoDeck, int oneDeck)
        {
            shipDecksCount = fourDeck*4 + threeDeck*3 + twoDeck*2 + oneDeck;
            ships = new Ship[fourDeck + threeDeck + twoDeck + oneDeck];

            for (int i = 0; i < fourDeck; i++)
            {
                ships[i] = new Ship(4, this);
            }
            for (int i = fourDeck; i < fourDeck + threeDeck; i++)
            {
                ships[i] = new Ship(3, this);
            }
            for (int i = fourDeck + threeDeck; i < fourDeck + threeDeck + twoDeck; i++)
            {
                ships[i] = new Ship(2, this);
            }
            for (int i = fourDeck + threeDeck + twoDeck; i < fourDeck + threeDeck + twoDeck + oneDeck; i++)
            {
                ships[i] = new Ship(1, this);
            }
        }

        private void PlaceShips()
        {
            int shipsToPlace = ships.Length;
            while(shipsToPlace > 0)
            {
                ships[shipsToPlace - 1].Place();
                shipsToPlace--;
            }
        }

        public void AddShip(Ship ship)
        {
            var shipCoordinates = ship.GetShipCoordinates();

            if (ship.IsOrientationVerticalGet())
            {
                for (int i = 0; i < ship.DecksAmountGet(); i++)
                {
                    arena[shipCoordinates.y + i, shipCoordinates.x] = '#';
                }
                return;
            }

            for (int i = 0; i < ship.DecksAmountGet(); i++)
            {
                arena[shipCoordinates.y, shipCoordinates.x + i] = '#';
            }
        }

        public void UpdateArenaToDisplay(Ship ship)
        {
            arenaToDisplay = arena.Clone() as char[,];

            var shipCoordinates = ship.GetShipCoordinates();

            if (ship.IsOrientationVerticalGet())
            {
                for (int i = 0; i < ship.DecksAmountGet(); i++)
                {
                    arenaToDisplay[shipCoordinates.y + i, shipCoordinates.x] = '#';
                }

                UIManager.DisplayArena(arenaToDisplay);

                return;
            }

            for (int i = 0; i < ship.DecksAmountGet(); i++)
            {
                arenaToDisplay[shipCoordinates.y, shipCoordinates.x + i] = '#';
            }

            UIManager.DisplayArena(arenaToDisplay);
        }
        public void UpdateArenaToDisplay((int x, int y) coordinates)
        {
            arenaToDisplay = arenaToDisplayForEnemy.Clone() as char[,];
            arenaToDisplay[coordinates.y, coordinates.x] = '@';
        }
        public void UpdateArenaToDisplay()
        {
            arenaToDisplay = arenaToDisplayForEnemy.Clone() as char[,];
        }

        public bool HitCheck((int x, int y) attackPoint) => arena[attackPoint.y, attackPoint.x] == '#';
        public void UpdateInformationOnAttack(bool isHit, (int x, int y) attackPoint)
        {
            if (isHit)
            {
                arenaToDisplayForEnemy[attackPoint.y, attackPoint.x] = '!';
                arena[attackPoint.y, attackPoint.x] = '!';
                shipDecksCount--;
                return;
            }

            arena[attackPoint.y, attackPoint.x] = '%';
            arenaToDisplayForEnemy[attackPoint.y, attackPoint.x] = '%';
        }
        public bool AllShipsDestroyed()
        {
            if (shipDecksCount > 0)
                return false;
            return true;
        }
    }
}

