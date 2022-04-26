using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class Game
    {
        private (int x, int y) arenaDimensions = (20, 20); //Sets up arena dimensions

        // amount of different ship types
        private int fourDeckAmount = 1;
        private int threeDeckAmount = 2;
        private int doubleDeckAmount = 3;
        private int singleDeckAmount = 4;

        Arena arena1;
        Arena arena2;

        public void InitArenas()
        {
            arena1 = new Arena(arenaDimensions.x, arenaDimensions.y, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);
            arena2 = new Arena(arenaDimensions.x, arenaDimensions.y, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);

            Console.WriteLine("Player 1, get ready to set your arena, waiting for your input");
            Console.ReadKey();
            arena1.SetArena();
            Console.Clear();

            Console.WriteLine("Player 2, get ready to set your arena, waiting for your input");
            Console.ReadKey();
            arena2.SetArena();
        }

        public void Gameplay()
        {
            int turns = 0;

            while (!arena1.WinCheck() && !arena2.WinCheck())
            {
                turns++;

                if(turns % 2 == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Player 2, get ready to make a move, waiting for your input");
                    Console.ReadKey();

                    AttackLogic.ChooseACellToAttack(arena1);
                    Console.WriteLine("Waiting for input to continue");
                    Console.ReadKey();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Player 1, get ready to make a move, waiting for your input");
                    Console.ReadKey();

                    AttackLogic.ChooseACellToAttack(arena2);
                    Console.WriteLine("Waiting for input to continue");
                    Console.ReadKey();
                }

            }
        }

        public void EndGame()
       {
            Console.Clear();
            if (arena1.WinCheck())
            {
                Console.WriteLine("Player 2 wins");
                return;
            }

            Console.WriteLine("Player 1 wins");

            Console.ReadLine();
        }

        public void PreGame()
        {
            Console.WriteLine("Rules: First, two players set their fields up.");
            Console.WriteLine("To move ships around use WASD, to rotate ships use R, to secure ship position use C. Then the game starts.");
            Console.WriteLine("Two players take turns, they blindly shoot enemy cells, in case of a hit '!' symbol appears on a screen. In case of a miss '%' symbol appears.");
            Console.WriteLine("In case of a hit, player gets a chance to make another shot immediately. First one to destroy all enemy ships wins.");
            Console.WriteLine("Waiting for your input to start the game");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
