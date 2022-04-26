using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class AttackLogic
    {
        public static void ChooseACellToAttack(Arena arena)
        {
            if (arena.WinCheck())
                return;


            (int x, int y) coordinatesOfAttack = (0, 0);
            arena.UpdateArenaToDisplay(coordinatesOfAttack);

            while (true)
            {
                var input = Console.ReadKey().KeyChar;
                var newCoordinates = MovementManager.MakeAMove(InputToCommand.GetCommand(input), coordinatesOfAttack);

                if (MovementManager.isPossibleToMove(newCoordinates, arena))
                    coordinatesOfAttack = newCoordinates;

                arena.UpdateArenaToDisplay(coordinatesOfAttack);
                if (InputToCommand.GetCommand(input) == Command.ChooseAPoint)
                {
                    if (MovementManager.isPossibleToChosseAPoint(coordinatesOfAttack, arena))
                    {
                        arena.PerformAttack(coordinatesOfAttack);

                        return;
                    }

                    Console.WriteLine("Impossible To Perform Attack Here");
                }
            }
        }
    }
}
