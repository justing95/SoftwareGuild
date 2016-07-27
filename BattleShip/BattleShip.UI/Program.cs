using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;

namespace BattleShip.UI
{
    class Program
    {
        
        static void Main(string[] args)
        {
            UIController uiController = new UIController();
            string start = uiController.StartMenu();
            while (start == "1")
            {
                Workflow game = uiController.SetPlayerNames();
                uiController.PrintSetUp(game);
                uiController.MakeShip(game);
                uiController.PrintSetUp(game);
                uiController.MakeShip(game);
                uiController.TakeShot(game);
                start = uiController.EndMenu(game);
            }
            Console.WriteLine("Thanks for Playing");
            Console.ReadKey();
        }

        

    }
}
