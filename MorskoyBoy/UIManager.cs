using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class UIManager
    {
        public enum MessageName
        {
            ArenaSetup,
            PreMoveMessage,
            InputWait,
            RuleMessage,
            WinMessage,
            Default
        }

        public static void UI_DisplayError(int errorIndex)
        {
            switch (errorIndex)
            {
                case 0:
                    Console.WriteLine("Cannot place ship here");
                    break;
                case 1:
                    Console.WriteLine("Impossible To Perform Attack Here");
                    break;
            }
        }

        public static void UI_DisplayMessage(MessageName messageName, int playerNumber)
        {
            switch (messageName)
            {
                case MessageName.ArenaSetup:
                    DisplayArenaSetupMessage(playerNumber);
                    break;
                case MessageName.PreMoveMessage:
                    DisplayPreMoveMessage(playerNumber);
                    break;
                case MessageName.InputWait:
                    DisplayInputWaitMessage();
                    break;
                case MessageName.RuleMessage:
                    DisplayRuleMessage();
                    break;
                case MessageName.WinMessage:
                    DisplayWinMessage(playerNumber);
                    break;
                default:
                    Console.WriteLine("Default message");
                    break;
            }
        }
        private static void DisplayArenaSetupMessage(int playerNumber)
        {
            Console.Clear();
            Console.WriteLine($"Plater {playerNumber}, get ready to set your arena, waiting for your input");
            Console.ReadKey();
        }
        private static void DisplayPreMoveMessage(int playerNumber)
        {
            Console.Clear();
            Console.WriteLine($"Player {playerNumber}, get ready to make a move, waiting for your input");
            Console.ReadKey();
        }
        private static void DisplayInputWaitMessage()
        {
            Console.WriteLine("Waiting for input to continue");
            Console.ReadKey();
        }
        private static void DisplayRuleMessage()
        {
            Console.WriteLine("Rules: First, two players set their fields up.");
            Console.WriteLine("To move ships around use WASD, to rotate ships use R, to secure ship position use C. Then the game starts.");
            Console.WriteLine("Two players take turns, they blindly shoot enemy cells, in case of a hit '!' symbol appears on a screen. In case of a miss '%' symbol appears.");
            Console.WriteLine("In case of a hit, player gets a chance to make another shot immediately. First one to destroy all enemy ships wins.");
            Console.WriteLine("Waiting for your input to start the game");
            Console.ReadKey();
            Console.Clear();
        }
        private static void DisplayWinMessage(int playerNumber)
        {
            Console.Clear();
            Console.WriteLine($"Player {playerNumber} wins");
            Console.ReadLine();
        }

        public static void DisplayArena(Arena arena)
        {
            var charArena = arena.GetArenaToDisplay();

            Console.Clear();
            for (int i = 0; i < charArena.GetLength(0); i++)
            {
                for (int j = 0; j < charArena.GetLength(1); j++)
                {
                    Console.Write(charArena[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        public static void DisplayArena(char[,] charArena)
        {
            Console.Clear();
            for (int i = 0; i < charArena.GetLength(0); i++)
            {
                for (int j = 0; j < charArena.GetLength(1); j++)
                {
                    Console.Write(charArena[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
