using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class Player
    {
        private int playerNumber;
        public int GetPlayerNumber() => playerNumber;
        private Arena arena;
        public Arena GetArena() => arena;


        public Player(int newPlayerNumber)
        {
            playerNumber = newPlayerNumber;
        }

        public void SetArena((int x, int y) arenaDimensions, int fourDeckAmount, int threeDeckAmount, int doubleDeckAmount, int singleDeckAmount)
        {
            arena = new Arena(arenaDimensions.x, arenaDimensions.y, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);
        }
    }
}
