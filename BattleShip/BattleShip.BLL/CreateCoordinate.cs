using BattleShip.BLL.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.BLL
{
    public class CreateCoordinate
    {
        public Coordinate GetCoordinate(string xStringToInt, int yCoordinate)
        {
            int toInt;
            xStringToInt = xStringToInt.ToLower();
            if (xStringToInt == "a")
            {
                toInt = 1;
            }
            else if (xStringToInt == "b")
            {
                toInt = 2;
            }
            else if (xStringToInt == "c")
            {
                toInt = 3;
            }
            else if (xStringToInt == "d")
            {
                toInt = 4;
            }
            else if (xStringToInt == "e")
            {
                toInt = 5;
            }
            else if (xStringToInt == "f")
            {
                toInt = 6;
            }
            else if (xStringToInt == "g")
            {
                toInt = 7;
            }
            else if (xStringToInt == "h")
            {
                toInt = 8;
            }
            else if (xStringToInt == "i")
            {
                toInt = 9;
            }
            else if (xStringToInt == "j")
            {
                toInt = 10;
            }
            else
            {
                throw new ArgumentException();
            }
            if (yCoordinate > 10 || yCoordinate < 1)
            {
                throw new ArgumentException();
            }
            return new Coordinate(toInt, yCoordinate);
        }
    }
}
