using Microsoft.VisualBasic;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Monster
{

    public string Name { get; set; }
    public int HP{ get; set; }
    public int Damage{ get; set; }
    public Monster(string name, int hP, int damage)
    {
        this.Name = name;
        this.HP = hP;
        this.Damage = damage;
    }
    public string MonsterAction()
    {
        Random rnd = new Random();
        string[] actions = { "Attack", "Defence" };
        string returnAction = actions[rnd.Next(actions.Length)];
        return returnAction;
    }

}