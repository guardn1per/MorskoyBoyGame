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

                DisplayArenaToDisplay();

                return;
            }

            for (int i = 0; i < ship.DecksAmountGet(); i++)
            {
                arenaToDisplay[shipCoordinates.y, shipCoordinates.x + i] = '#';
            }

            DisplayArenaToDisplay();
        }
        public void UpdateArenaToDisplay((int x, int y) coordinates)
        {
            arenaToDisplay = arenaToDisplayForEnemy.Clone() as char[,];
            arenaToDisplay[coordinates.y, coordinates.x] = '@';

            DisplayArenaToDisplay();
        }
        private void UpdateArenaToDisplay()
        {
            arenaToDisplay = arenaToDisplayForEnemy.Clone() as char[,];
            DisplayArenaToDisplay();
        }

        private void DisplayArenaToDisplay()
        {
            Console.Clear();
            for (int i = 0; i < arenaDimensions.y; i++)
            {
                for (int j = 0; j < arenaDimensions.x; j++)
                {
                    Console.Write(arenaToDisplay[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        public void PerformAttack((int x, int y) coordinates)
        {
            if (arena[coordinates.y, coordinates.x] == '#')
            {
                arenaToDisplayForEnemy[coordinates.y, coordinates.x] = '!';
                arena[coordinates.y, coordinates.x] = '!';
                shipDecksCount--;
                AttackLogic.ChooseACellToAttack(this); //Gives one more shot in case of a hit
                return;
            }
            
            arena[coordinates.y, coordinates.x] = '%';
            arenaToDisplayForEnemy[coordinates.y, coordinates.x] = '%';

            UpdateArenaToDisplay();

        }

        public bool WinCheck()
        {
            if (shipDecksCount > 0)
                return false;
            return true;
        }
    }
}

