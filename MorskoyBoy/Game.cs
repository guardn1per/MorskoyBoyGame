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
        private Player player1, player2;

        private bool ChangePlayer = true;

        public void Init()
        {
            player1 = new Player(1);
            player2 = new Player(2);


            UIManager.UI_DisplayMessage(UIManager.MessageName.ArenaSetup, 1);
            ArenaSetup(player1, arenaDimensions, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);

            UIManager.UI_DisplayMessage(UIManager.MessageName.ArenaSetup, 2);
            ArenaSetup(player2, arenaDimensions, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);
        }
        private void GameCycle_Attack(Player playerToAttack)
        {
            var arena = playerToAttack.GetArena();

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
                        PerformAttack(coordinatesOfAttack, playerToAttack);
                        UIManager.DisplayArena(arena);
                        return;
                    }

                    UIManager.UI_DisplayError(1);
                }
            }
        }
        private void GameCycle_AddSingleShip(Player player, Ship ship)
        {
            var arena = player.GetArena();

            arena.UpdateArenaToDisplay(ship);
            UIManager.DisplayArena(arena);

            while (true)
            {
                char input = Console.ReadKey().KeyChar;

                var newCoordinates = ShipPlacementMovement.MakeAMove(InputToCommand.GetCommand(input), ship.GetShipCoordinates());
                var newRotation = ShipPlacementMovement.RotateShip(ship, InputToCommand.GetCommand(input));

                if (ShipPlacementMovement.isPossibleToRotate(ship, arena))
                    ship.SetOrientation(newRotation);
                if (ShipPlacementMovement.isPossibleToGo(ship, arena, newCoordinates))
                    ship.SetShipCoordinates(newCoordinates);

                arena.UpdateArenaToDisplay(ship);
                UIManager.DisplayArena(arena);

                if (InputToCommand.GetCommand(input) == Command.ChooseAPoint)
                {
                    if (ShipPlacementMovement.isPossibleToPlace(ship, arena))
                    {
                        arena.AddShip(ship);
                        UIManager.DisplayArena(arena);
                        return;
                    }

                    UIManager.UI_DisplayError(0);
                }
            }
        }
        private void ArenaSetup(Player player, (int x, int y) arenaDimensions, int fourDeckAmount, int threeDeckAmount, int doubleDeckAmount, int singleDeckAmount)
        {
            var arena = new Arena(arenaDimensions.x, arenaDimensions.y, fourDeckAmount, threeDeckAmount, doubleDeckAmount, singleDeckAmount);
            player.AssignArena(arena);
            AddShipsToPlayerArena(player);
        }
        public void AddShipsToPlayerArena(Player player)
        {
            var arena = player.GetArena();
            var shipsArray = arena.GetShipsArray();

            int shipsToPlace = shipsArray.Length;
            while (shipsToPlace > 0)
            {
                GameCycle_AddSingleShip(player, shipsArray[shipsToPlace - 1]);
                shipsToPlace--;
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
            int turns = 1;
            Player currentPlayer = player1;
            Player playerToAttack = player2;

            while (!player1.GetArena().AllShipsDestroyed() && !player2.GetArena().AllShipsDestroyed())
            {


                if (ChangePlayer)
                {
                    if (turns % 2 == 1)
                    {
                        currentPlayer = player1; playerToAttack = player2;
                    }
                    else
                    {
                        currentPlayer = player2; playerToAttack = player1;
                    }
                    turns++;
                }

                Turn(currentPlayer, playerToAttack);
            }
        }
        private void Turn(Player attackingPlayer, Player playerToAttack)
        {
            UIManager.UI_DisplayMessage(UIManager.MessageName.PreMoveMessage, attackingPlayer.GetPlayerNumber());

            GameCycle_Attack(playerToAttack);

            UIManager.UI_DisplayMessage(UIManager.MessageName.InputWait, attackingPlayer.GetPlayerNumber());
        }


        public void EndGame()
        {
            if (player1.GetArena().AllShipsDestroyed())
            {
                UIManager.UI_DisplayMessage(UIManager.MessageName.WinMessage, 1);
                return;
            }

            UIManager.UI_DisplayMessage(UIManager.MessageName.WinMessage, 2);
        }

        public void PreGame()
        {
            UIManager.UI_DisplayMessage(UIManager.MessageName.RuleMessage, 0);
        }

    }
}
