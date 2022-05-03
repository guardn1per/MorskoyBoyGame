using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorskoyBoy
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Game game = new Game();

            game.PreGame();
            game.Init();
            game.Gameplay();
            game.EndGame();
        }

    }
}
