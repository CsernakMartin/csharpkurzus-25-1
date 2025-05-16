using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace projekt
{
    class Game : GameLogic
    {
        public Monster[] MonsterList =
        {
            new Monster("Gubóka", 20, 1),
            new Monster("Vorrgoth", 100, 5),
            new Monster("Drazuul", 200, 10),
            new Monster("Kryzthar", 300, 15),
            new Monster("Zornak", 350, 20),
            new Monster("Morgulath", 400, 30)
        };


        public void tutorial(Player p)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Texts/TutorialText.txt");
            string text = File.ReadAllText(path, Encoding.UTF8);
            Console.Clear();
            Console.WriteLine(text);
            Console.ReadKey();


            Monster monster = MonsterList[0];
            Player player = p;
            GameMechanic(player, monster,true);
            player.HP = 100;
            Console.ReadKey();
        }
        public void gameStart(Player p)
        {
            int indexOfStartingMonster;
            if (p.Points.Equals(0))
            {
                string path = Path.Combine(AppContext.BaseDirectory, "Texts/GameText.txt");
                string text = File.ReadAllText(path, Encoding.UTF8);
                Console.Clear();
                Console.WriteLine(text);
                Console.ReadKey();
                indexOfStartingMonster = 1;
            }
            else 
            {
                indexOfStartingMonster = (p.Points / 100) + 1;
            }
            for (int i = indexOfStartingMonster; i < MonsterList.Length; i++)
            {
                Monster monster = MonsterList[i];
                Player player = p;
                GameMechanic(player, monster);

                Console.ReadKey();
            }
            gameEnd(p);
        }
        public void gameEnd(Player p)
        {
            Menu menu = new Menu();
            string path = Path.Combine(AppContext.BaseDirectory, "Texts/EndCredit.txt");
            string credits = File.ReadAllText(path, Encoding.UTF8);
            Console.Clear();
            Console.WriteLine(credits);
            Console.ReadKey();
            Save save = new Save(p);
            menu.mainMenu();
        }
    }
}
