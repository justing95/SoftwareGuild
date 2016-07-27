using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            string[] spots = new string[]
               {
                "1","2","3","4","5","6","7","8","9"
               };
            int player = 1;
            Console.WriteLine("What is player 1's name?");
            string player1 = Console.ReadLine();
            Console.WriteLine("What is player 2's name?");
            string player2 = Console.ReadLine();
            MakeBoard(spots, player);
            Console.ReadKey();

        }
        static void MakeBoard(string[] spots, int player)
        {
            int make = 0;
            int position = 0;
            int hor = 0;
            int i = 0;
            string vertical = "    |      |";
            string horizontal = "----------------";
            while (make < 4)
            {
                position++;
                if (make == 2)
                {
                    Console.WriteLine("   " + spots[i++] + "|  " + spots[i++] +
                        "   |" + spots[i++]);
                }
                if (make == 3 && hor < 2)
                {
                    Console.WriteLine(horizontal);
                    make = 0;
                    hor++;
                }
                Console.WriteLine(vertical);
                make++;
                
            }
            if (IsWinner(spots, player))
            {
                if (player == 1)
                {
                    player = 2;
                }
                else if (player == 2)
                {
                    player = 1;
                }
                Console.WriteLine("player " + player + " is the winner");
            }
            else if (count == 9)
            {
                Console.WriteLine("TIE GAME");
            }
            else
            {
                count++;
                DoTurn(spots, player);

            }

        }
        static void DoTurn(string[] spots, int player)
        {
            Console.WriteLine("Choose an open spot!");
            string userChoice = Console.ReadLine();
            int choice;
            if (player == 1)
            {
                while (!int.TryParse(userChoice, out choice) || (spots[choice - 1] == "x" || spots[choice - 1] == "o")
                    || choice > 9 || choice < 1)
                {
                    Console.WriteLine("choose a valid number");
                    userChoice = Console.ReadLine();
                }
                spots[choice - 1] = "x";
                player = 2;

            }
            else if (player == 2)
            {
                while (!int.TryParse(userChoice, out choice) || (spots[choice - 1] == "x" || spots[choice - 1] == "o")
                    || choice > 9 || choice < 1)
                {
                    Console.WriteLine("choose a valid number");
                    userChoice = Console.ReadLine();
                }
                spots[choice - 1] = "o";
                player = 1;

            }
                MakeBoard(spots, player);
            Console.ReadKey();
        }
        static bool IsWinner(string[] spots, int player)
        {
            string marker;
            if (player == 1)
            {
                marker = "o";
            }
            else
            {
                marker = "x";
            }
            if (spots[0] == marker && spots[1] == marker && spots[2] == marker)
            {
                return true;
            }
            else if (spots[3] == marker && spots[4] == marker && spots[5] == marker)
            {
                return true;
            }
            else if (spots[6] == marker && spots[7] == marker && spots[8] == marker)
            {
                return true;
            }
            else if (spots[0] == marker && spots[3] == marker && spots[6] == marker)
            {
                return true;
            }
            else if (spots[1] == marker && spots[4] == marker && spots[7] == marker)
            {
                return true;
            }
            else if (spots[2] == marker && spots[5] == marker && spots[8] == marker)
            {
                return true;
            }
            else if (spots[0] == marker && spots[4] == marker && spots[8] == marker)
            {
                return true;
            }
            else if (spots[2] == marker && spots[4] == marker && spots[6] == marker)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
