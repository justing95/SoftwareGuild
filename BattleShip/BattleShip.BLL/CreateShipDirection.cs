using BattleShip.BLL.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.BLL
{
    public class CreateShipDirection
    {
        public ShipDirection GetDirection(string direction)
        {
            direction = direction.ToLower();
            if (direction == "up")
            {
                return ShipDirection.Up;
            }
            else if (direction == "down")
            {
                return ShipDirection.Down;
            }
            else if (direction == "left")
            {
                return ShipDirection.Left;
            }
            else if (direction == "right")
            {
                return ShipDirection.Right;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
