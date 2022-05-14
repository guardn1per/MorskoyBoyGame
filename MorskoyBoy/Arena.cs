using System;

namespace MorskoyBoy
{
    internal class Arena
    {
        private readonly char shipChar = '#';
        public char GetShipChar() => shipChar;
        private readonly char attackPointerChar = '@';
        public char GetAttackPointerChar() => attackPointerChar;
        private readonly char hitChar = '!';
        public char GetHitChar() => hitChar;
        private readonly char missChar = '%';
        public char GetMissChar() => missChar;
        private const char waterChar = '.';
        public char GetWaterChar() => waterChar;

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
        public Ship[] GetShipsArray() => ships.Clone() as Ship[];
        private int shipDecksCount;

        private void CreateClearArena(int x, int y)
        {
            arena = new char[y, x];

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    arena[i, j] = waterChar;
                }
            }
        }
        private void AddShipsToArray(int fourDeck, int threeDeck, int twoDeck, int oneDeck)
        {
            shipDecksCount = fourDeck*4 + threeDeck*3 + twoDeck*2 + oneDeck;
            ships = new Ship[fourDeck + threeDeck + twoDeck + oneDeck];
            var shipsPlacedAmount = 0;

            for (int i = 0; i < fourDeck; i++)
            {
                ships[i] = new Ship(4);
            }
            shipsPlacedAmount += fourDeck;
            for (int i = 0; i < threeDeck; i++)
            {
                ships[i + shipsPlacedAmount] = new Ship(3);
            }
            shipsPlacedAmount += threeDeck;
            for (int i = 0; i < twoDeck; i++)
            {
                ships[i + shipsPlacedAmount] = new Ship(2);
            }
            shipsPlacedAmount += twoDeck;
            for (int i = 0; i < oneDeck; i++)
            {
                ships[i + shipsPlacedAmount] = new Ship(1);
            }
        }
        public void AddShip(Ship ship)
        {
            var shipCoordinates = ship.GetShipCoordinates();

            if (ship.IsOrientationVerticalGet())
            {
                for (int i = 0; i < ship.DecksAmountGet(); i++)
                {
                    arena[shipCoordinates.y + i, shipCoordinates.x] = shipChar;
                }
                return;
            }

            for (int i = 0; i < ship.DecksAmountGet(); i++)
            {
                arena[shipCoordinates.y, shipCoordinates.x + i] = shipChar;
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
                    arenaToDisplay[shipCoordinates.y + i, shipCoordinates.x] = shipChar;
                }
            }
            else
            {
                for (int i = 0; i < ship.DecksAmountGet(); i++)
                {
                    arenaToDisplay[shipCoordinates.y, shipCoordinates.x + i] = shipChar;
                }
            }
        }
        public void UpdateArenaToDisplay((int x, int y) coordinates)
        {
            arenaToDisplay = arenaToDisplayForEnemy.Clone() as char[,];
            arenaToDisplay[coordinates.y, coordinates.x] = attackPointerChar;
        }
        public void UpdateArenaToDisplay()
        {
            arenaToDisplay = arenaToDisplayForEnemy.Clone() as char[,];
        }

        public bool HitCheck((int x, int y) attackPoint) => arena[attackPoint.y, attackPoint.x] == shipChar;
        public void UpdateInformationOnAttack(bool isHit, (int x, int y) attackPoint)
        {
            if (isHit)
            {
                arenaToDisplayForEnemy[attackPoint.y, attackPoint.x] = hitChar;
                arena[attackPoint.y, attackPoint.x] = hitChar;
                shipDecksCount--;
                return;
            }

            arena[attackPoint.y, attackPoint.x] = missChar;
            arenaToDisplayForEnemy[attackPoint.y, attackPoint.x] = missChar;
        }
        public bool AllShipsDestroyed()
        {
            if (shipDecksCount > 0)
                return false;
            return true;
        }
        public (int x, int y) ChooseRandomFreePoint()
        {
            for (int i = 0; i < arenaDimensions.y; i++)
            {
                for (int j = 0; j < arenaDimensions.x; j++)
                {
                    if (arenaToDisplayForEnemy[i, j] == waterChar)
                        return (j, i);
                }
            }

            return (0, 0);
        }
    }
}

