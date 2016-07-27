using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using NUnit.Framework;
using BattleShip.BLL;

namespace Battleship.Tests
{
    [TestFixture]
    public class ShipPlacementTests
    {
        [Test]
        public void CanNotPlaceShipOffBoard()
        {
            Board board = new Board();
            PlaceShipRequest request = new PlaceShipRequest()
            {
                Coordinate = new Coordinate(15, 10),
                Direction = ShipDirection.Up,
                ShipType = ShipType.Destroyer
            };

            var response = board.PlaceShip(request);

            Assert.AreEqual(ShipPlacement.NotEnoughSpace, response);
        }

        [Test]
        public void CanNotPlaceShipPartiallyOnBoard()
        {
            Board board = new Board();
            PlaceShipRequest request = new PlaceShipRequest()
            {
                Coordinate = new Coordinate(10, 10),
                Direction = ShipDirection.Right,
                ShipType = ShipType.Carrier
            };

            var response = board.PlaceShip(request);

            Assert.AreEqual(ShipPlacement.NotEnoughSpace, response);
        }

        [Test]
        public void CanNotOverlapShips()
        {
            Board board = new Board();

            // let's put a carrier at (10,10), (9,10), (8,10), (7,10), (6,10)
            var carrierRequest = new PlaceShipRequest()
            {
                Coordinate = new Coordinate(10, 10),
                Direction = ShipDirection.Left,
                ShipType = ShipType.Carrier
            };

            var carrierResponse = board.PlaceShip(carrierRequest);

            Assert.AreEqual(ShipPlacement.Ok, carrierResponse);

            // now let's put a destroyer overlapping the y coordinate
            var destroyerRequest = new PlaceShipRequest()
            {
                Coordinate = new Coordinate(9, 9),
                Direction = ShipDirection.Down,
                ShipType = ShipType.Destroyer
            };

            var destroyerResponse = board.PlaceShip(destroyerRequest);

            Assert.AreEqual(ShipPlacement.Overlap, destroyerResponse);
        }

        [TestCase("a", 14)]
        [TestCase("X", 8)]
        [TestCase("a", 11)]
        [TestCase("z", 99)]
        [TestCase("aa", 11)]
        public void InvalidInputCauseException(string x, int y)
        {
            CreateCoordinate create = new CreateCoordinate();
            Assert.Throws<ArgumentException>(() => create.GetCoordinate(x, y));
        }

        [Test]
        public void ValidInputReturnCoordinate()
        {
            CreateCoordinate create = new CreateCoordinate();
            Coordinate coordinate = new Coordinate(5, 5);
            Assert.AreEqual(coordinate, create.GetCoordinate("e", 5));
        }

        [TestCase("abg")]
        [TestCase("north")]
        [TestCase("leeft")]
        public void InvalidDirectionCauseException(string direction)
        {
            CreateShipDirection create = new CreateShipDirection();
            Assert.Throws<ArgumentException>(() => create.GetDirection(direction));
        }

        [TestCase("right", ShipDirection.Right)]
        [TestCase("LeFt", ShipDirection.Left)]
        [TestCase("DOWN", ShipDirection.Down)]
        [TestCase("up", ShipDirection.Up)]
        public void ValidDirectionReturnShipDirection(string direction, ShipDirection expected)
        {
            CreateShipDirection create = new CreateShipDirection();
            Assert.AreEqual(expected, create.GetDirection(direction));
        }

        [Test]
        public void WorkFlowInititializesCorrectly()
        {
            Workflow game = new Workflow("Justin", "John");
            Assert.AreEqual("Justin", game.GetPlayerName());
            game.SwitchPlayer();
            Assert.AreEqual("John", game.GetPlayerName());
        }
        
        [Test]
        public void CheckBoardsAreDifferent()
        {
            Workflow game = new Workflow("J", "L");
            Board board1 = game.GetBoard();
            game.SwitchPlayer();
            Board board2 = game.GetBoard();
            Assert.AreNotEqual(board1, board2);
        }

        [TestCase(1, 1, ShipDirection.Right, ShipPlacement.Ok)]
        [TestCase(5, 5, ShipDirection.Down, ShipPlacement.Ok)]
        [TestCase(10, 10, ShipDirection.Up, ShipPlacement.Ok)]
        [TestCase(10, 1, ShipDirection.Left, ShipPlacement.Ok)]
        [TestCase(10, 1, ShipDirection.Right, ShipPlacement.NotEnoughSpace)]
        public void CheckShipPlacement(int x, int y, ShipDirection shipDirection, ShipPlacement expected)
        {
            Workflow game = new Workflow("J", "L");
            Coordinate coordinate = new Coordinate(x, y);
            ShipDirection direction = shipDirection;
            ShipType type = ShipType.Destroyer;
            PlaceShipRequest request = new PlaceShipRequest
            {
                Direction = direction,
                Coordinate = coordinate,
                ShipType = type
            };
            Board board = game.GetBoard();
            ShipPlacement ship = board.PlaceShip(request);
            Assert.AreEqual(expected, ship);
        }
        
    }
}
