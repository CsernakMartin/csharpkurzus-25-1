using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt
{
    class Auth
    {
        public Player register()
        {
            Console.Clear();
            Console.WriteLine("Registration: \n");
            Player returnPlayer = new Player();
            Console.Write("Enter a name: ");
            returnPlayer.Name = Console.ReadLine();
            returnPlayer.Points = 0;
            returnPlayer.Damage = 50;
            returnPlayer.HP = 100;
            return returnPlayer;
        }
    }
}
