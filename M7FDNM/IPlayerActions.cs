using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt
{
    interface IPlayerActions
    {
        public string RoundAction();
        public void Lose(string monsterName, bool isTutorial);
        public void Win(string monsterName, bool isTutorial);
    }
}
