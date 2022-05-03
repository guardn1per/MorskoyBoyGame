using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class Game
    {
        private readonly (int x, int y) arenaDimensions = (20, 20); //Sets up arena dimensions

        // amount of different ship types
        private const int fourDeckAmount = 1;
        private const int threeDeckAmount = 2;
        private const int doubleDeckAmount = 3;
        private const int singleDeckAmount = 4;

        Arena arena1;
        Arena arena2;

        public void InitArenas()
        {
            arena1 = new Arena(arenaDimensions.x, arenaDimensions.y, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);
            arena2 = new Arena(arenaDimensions.x, arenaDimensions.y, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);

            UIManager.UI_DisplayMessage(UIManager.MessageName.ArenaSetup, 1);
            arena1.SetArena();

            UIManager.UI_DisplayMessage(UIManager.MessageName.ArenaSetup, 2);
            arena2.SetArena();
        }

        public void Gameplay()
        {
            int turns = 0;

            while (!arena1.AllShipsDestroyed() && !arena2.AllShipsDestroyed())
            {
                turns++;

                Turn(turns % 2);

            }
        }
        private void Turn(int turnsMod2)
        {
            if (turnsMod2 == 0)
                turnsMod2 = 2;

            UIManager.UI_DisplayMessage(UIManager.MessageName.PreMoveMessage, turnsMod2);

            if (turnsMod2 == 1)
                AttackLogic.ChooseACellToAttack(arena2);
            else
                AttackLogic.ChooseACellToAttack(arena1);

            UIManager.UI_DisplayMessage(UIManager.MessageName.InputWait, turnsMod2);
        }


        public void EndGame()
        {
            if (arena1.AllShipsDestroyed())
            {
                UIManager.UI_DisplayMessage(UIManager.MessageName.WinMessage, 2);
                return;
            }

            UIManager.UI_DisplayMessage(UIManager.MessageName.WinMessage, 1);
        }

        public void PreGame()
        {
            UIManager.UI_DisplayMessage(UIManager.MessageName.RuleMessage, 0);
        }

    }
}
