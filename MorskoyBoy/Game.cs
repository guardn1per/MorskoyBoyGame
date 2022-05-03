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
        private const int threeDeckAmount = 0;
        private const int doubleDeckAmount = 0;
        private const int singleDeckAmount = 0;

        private Player[] players;

        private bool ChangePlayer = true;

        public void Init()
        {
            players = new Player[2];

            players[0] = new Player(1);
            players[0].SetArena(arenaDimensions, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);

            players[1] = new Player(2);
            players[1].SetArena(arenaDimensions, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);


            UIManager.UI_DisplayMessage(UIManager.MessageName.ArenaSetup, 1);
            players[0].GetArena().SetArena();

            UIManager.UI_DisplayMessage(UIManager.MessageName.ArenaSetup, 2);
            players[1].GetArena().SetArena();
        }
        public void GameCycle_Attack(Player player)
        {
            var arena = player.GetArena();

            if (arena.AllShipsDestroyed())
                return;


            (int x, int y) coordinatesOfAttack = (0, 0);
            arena.UpdateArenaToDisplay(coordinatesOfAttack);
            UIManager.DisplayArena(arena);

            while (true)
            {
                var input = Console.ReadKey().KeyChar;
                var newCoordinates = MovementManager.MakeAMove(InputToCommand.GetCommand(input), coordinatesOfAttack);

                if (MovementManager.isPossibleToMove(newCoordinates, arena))
                    coordinatesOfAttack = newCoordinates;

                arena.UpdateArenaToDisplay(coordinatesOfAttack);
                UIManager.DisplayArena(arena);
                if (InputToCommand.GetCommand(input) == Command.ChooseAPoint)
                {
                    if (MovementManager.isPossibleToChosseAPoint(coordinatesOfAttack, arena))
                    {
                        PerformAttack(coordinatesOfAttack, player);
                        UIManager.DisplayArena(arena);
                        return;
                    }

                    UIManager.UI_DisplayError(1);
                }
            }
        }

        private void PerformAttack((int x, int y) coordinates, Player player)
        {
            var arena = player.GetArena();

            var isHit = arena.HitCheck(coordinates);
            arena.UpdateInformationOnAttack(isHit, coordinates);
            if (isHit)
                ChangePlayer = false;
            else
                ChangePlayer = true;
            arena.UpdateArenaToDisplay();
        }

        public void Gameplay()
        {
            int turns = 0;
            Player currentPlayer = players[0];

            while (!players[0].GetArena().AllShipsDestroyed() && !players[1].GetArena().AllShipsDestroyed())
            {
                if (turns >= players.Length)
                    turns -= players.Length;

                if(ChangePlayer)
                    currentPlayer = players[turns];

                Turn(currentPlayer);

                turns++;
            }
        }
        private void Turn(Player player)
        {
            UIManager.UI_DisplayMessage(UIManager.MessageName.PreMoveMessage, player.GetPlayerNumber());

            GameCycle_Attack(player);

            UIManager.UI_DisplayMessage(UIManager.MessageName.InputWait, player.GetPlayerNumber());
        }


        public void EndGame()
        {
            if (players[0].GetArena().AllShipsDestroyed())
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
