using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class InputToCommand
    {
        public static Command GetCommand(char Input)
        {
            if (Input == 'w' || Input == 'W')
                return Command.Up;
            if (Input == 's' || Input == 'S')
                return Command.Down;
            if (Input == 'a' || Input == 'A')
                return Command.Left;
            if (Input == 'd' || Input == 'D')
                return Command.Right;
            if (Input == 'R' || Input == 'r')
                return Command.Rotate;
            if (Input == 'c' || Input == 'C')
                return Command.ChooseAPoint;
            return Command.Default;
        }
    }


    public enum Command
    {
        Up,
        Down,
        Left,
        Right,
        Rotate,
        ChooseAPoint,
        Default
    }

}
