using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.BLL
{
    public class Workflow
    {
        private string _player1Name;
        private string _player2Name;
        private Board board1 = new Board();
        private Board board2 = new Board();
        public int player { get; set; }


        public Workflow(string player1, string player2)
        {
            _player1Name = player1;
            _player2Name = player2;
            player = 1;
        }
        public Board GetBoard()
        {
            if (player == 1)
            {
                return board1;
            }
            else
            {
                return board2;
            }
        }

        public string GetPlayerName()
        {
            if (player == 1)
            {
                return _player1Name;
            }
            else
            {
                return _player2Name;
            }
        }

        public void SwitchPlayer()
        {
            if (player == 1)
            {
                player = 2;
            }
            else if (player == 2)
            {
                player = 1;
            }
        }
    }
}
