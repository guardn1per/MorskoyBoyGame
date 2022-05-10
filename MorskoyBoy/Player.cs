using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class Player
    {
        private bool isAI = false;
        public void SetIsAI() => isAI = true;
        public bool GetIsAI() => isAI;

        private int playerNumber;
        public int GetPlayerNumber() => playerNumber;
        private Arena arena;
        public Arena GetArena() => arena;
        public void AssignArena(Arena Arena)
        {
            arena = Arena;
        }

        public Player(int newPlayerNumber)
        {
            playerNumber = newPlayerNumber;
        }
    }
}
