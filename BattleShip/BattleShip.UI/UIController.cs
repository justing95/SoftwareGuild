using BattleShip.BLL;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    class UIController
    {
        public void TakeShot(Workflow game)
        {
            PrintBoard(game);
            Console.WriteLine("It is " + game.GetPlayerName() + "'s shot");
            Coordinate coordinate = GetCoordinate();
            FireShotResponse shot = game.GetBoard().FireShot(coordinate);

            while (shot.ShotStatus != ShotStatus.Victory)
            {
                while (shot.ShotStatus == ShotStatus.Invalid || shot.ShotStatus == ShotStatus.Duplicate)
                {
                    Console.WriteLine("That was an invalid shot, please choose different coordinates");
                    coordinate = GetCoordinate();
                    shot = game.GetBoard().FireShot(coordinate);
                }
                PrintBoard(game);
                if (shot.ShotStatus == ShotStatus.Hit)
                {
                    Console.WriteLine("You hit you opponents ship! \nPress any key to continue to next turn");
                    Console.ReadKey();
                }
                if (shot.ShotStatus == ShotStatus.Miss)
                {
                    Console.WriteLine("You didn't hit anything! \nPress any key to continue");
                    Console.ReadKey();
                }
                game.SwitchPlayer();
                if (shot.ShotStatus == ShotStatus.HitAndSunk)
                {
                    Console.WriteLine("You sunk your opponents " + shot.ShipImpacted + "\nPress any key to continue to next turn.");
                    Console.ReadKey();
                }
                PrintBoard(game);
                Console.WriteLine("It is " + game.GetPlayerName() + "'s shot");
                coordinate = GetCoordinate();
                shot = game.GetBoard().FireShot(coordinate);
            }

        }

        public string StartMenu()
        {
            Console.WriteLine("BATTLESHIP");
            Console.WriteLine("1. Start Game \n2. Exit");
            Console.WriteLine("Choose 1 to start or 2 to exit");
            string start = Console.ReadLine();
            while (start != "1" && start != "2")
            {
                Console.WriteLine("Choose a 1 or 2");
                start = Console.ReadLine();
            }
            return start;
        }

        public void PrintBoard(Workflow game)
        {
            Console.Clear();
            int row = 1;
            Board board = game.GetBoard();
            Console.WriteLine("   A B C D E F G H I J");
            for (int y = 0; y < 10; y++)
            {
                Console.Write(row.ToString("00") + " ");
                row++;

                for (int x = 0; x < 10; x++)
                {
                    Coordinate createBoard = new Coordinate(x + 1, y + 1);
                    if (!board.ShotHistory.ContainsKey(createBoard))
                    {
                        Console.Write("x ");
                    }
                    else if (board.ShotHistory[createBoard] == ShotHistory.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("H ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else if (board.ShotHistory[createBoard] == ShotHistory.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("M ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public Workflow SetPlayerNames()
        {
            Console.WriteLine("Input player 1's name");
            string player1 = Console.ReadLine();
            while (player1 == "")
            {
                Console.WriteLine("You didn't input a name");
                player1 = Console.ReadLine();
            }
            Console.WriteLine("Input player 2's name");
            string player2 = Console.ReadLine();
            while (player2 == "")
            {
                Console.WriteLine("You didn't input a name");
                player2 = Console.ReadLine();
            }
            Workflow game = new Workflow(player1, player2);
            return game;
        }

        public string EndMenu(Workflow game)
        {
            Console.WriteLine("Congratulations " + game.GetPlayerName() + ". You won!");
            Console.WriteLine("Would you like to play again?\n1. Yes\n2. No");
            string again = Console.ReadLine();
            while (again != "1" && again != "2")
            {
                Console.WriteLine("Input a 1 or 2");
                again = Console.ReadLine();
            }
            return again;
        }

        public void PlaceShip(ShipType type, Workflow game)
        {
            Console.WriteLine(game.GetPlayerName() + " place your " + type);
            Coordinate startPosition = GetCoordinate();
            ShipDirection direction = GetDirection();
            PlaceShipRequest placeShipRequest = new PlaceShipRequest()
            {
                Coordinate = startPosition,
                ShipType = type,
                Direction = direction
            };
            ShipPlacement placeShip = game.GetBoard().PlaceShip(placeShipRequest);
            while (placeShip == ShipPlacement.NotEnoughSpace)
            {
                Console.WriteLine("There's not enough space for your ship! Choose another position");
                placeShipRequest.Coordinate = GetCoordinate();
                placeShipRequest.Direction = GetDirection();
                placeShip = game.GetBoard().PlaceShip(placeShipRequest);
            }
            while (placeShip == ShipPlacement.Overlap)
            {
                Console.WriteLine("Two or more ships are overlapping! Choose another position");
                placeShipRequest.Coordinate = GetCoordinate();
                placeShipRequest.Direction = GetDirection();
                placeShip = game.GetBoard().PlaceShip(placeShipRequest);
            }
            PrintSetUp(game);
        }

        public Coordinate GetCoordinate()
        {
            Console.WriteLine("Choose an X coordinate A-J");
            CreateCoordinate makeCoordinate = new CreateCoordinate();
            string userX = Console.ReadLine().ToLower();
            while (userX != "a" && userX != "b" && userX != "c" && userX != "d" && userX != "e" &&
                userX != "f" && userX != "g" && userX != "h" && userX != "i" && userX != "j")
            {
                Console.WriteLine("You need to input a letter from A-J");
                userX = Console.ReadLine().ToLower();
            }

            Console.WriteLine("Choose a Y coordinate 1-10");
            string userY = Console.ReadLine();
            int yCoordinate;
            while (!int.TryParse(userY, out yCoordinate) || yCoordinate > 10 || yCoordinate < 1)
            {
                Console.WriteLine("You need to input a number from 1-10");
                userY = Console.ReadLine();
            }
            Coordinate coordinate = makeCoordinate.GetCoordinate(userX, yCoordinate);
            return coordinate;
        }

        public ShipDirection GetDirection()
        {
            Console.WriteLine("Choose either up, down, left, or right");
            string direction = Console.ReadLine().ToLower();
            while (direction != "up" && direction != "down" && direction != "left" && direction != "right")
            {
                Console.WriteLine("Please input either up, down, left, or right");
                direction = Console.ReadLine();
            }
            CreateShipDirection shipDirection = new CreateShipDirection();
            return shipDirection.GetDirection(direction);
        }

        public void MakeShip(Workflow game)
        {
            PlaceShip(ShipType.Destroyer, game);
            PlaceShip(ShipType.Cruiser, game);
            PlaceShip(ShipType.Submarine, game);
            PlaceShip(ShipType.Battleship, game);
            PlaceShip(ShipType.Carrier, game);
            game.SwitchPlayer();

        }

        public void PrintSetUp(Workflow game)
        {
            Console.Clear();
            int row = 1;
            Ship[] setUp = game.GetBoard().GetShips();
            Console.WriteLine("   A B C D E F G H I J");
            string print = "x ";
            for (int y = 0; y < 10; y++)
            {
                Console.Write(row.ToString("00") + " ");
                row++;
                for (int x = 0; x < 10; x++)
                {
                    print = "x ";
                    foreach (Ship ship in setUp)
                    {
                        if (ship != null)
                        {
                            foreach (Coordinate coordinate in ship.BoardPositions)
                            {
                                if (x + 1 == coordinate.XCoordinate && y + 1 == coordinate.YCoordinate)
                                {
                                    print = "o ";
                                }
                            }
                        }
                    }
                    Console.Write(print);
                }
                Console.WriteLine();
            }
        }
    }
}
