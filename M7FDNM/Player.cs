using projekt;
using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

class Player : IPlayerActions
{
    public Player()
    {
    }

    public string Name { get; set; }
    public int Points { get; set; }
    public int Damage { get; set; }
    public int HP { get; set; }

    public Player(string name, int point, int damage, int hp)
    {
        Name = name;
        Points = point;
        Damage = damage;
        HP = hp;
    }

    public void Lose(string monsterName, bool isTutorial = false)
    {
        if (isTutorial)
        {
            Console.Write(monsterName + " has defeated you!\n" +
                                            "The tutorial is over!\n" +
                                            "You've lost!\n"+
                                            "Press any key to continue...");
            return;
        }
        Console.Write(monsterName + " has defeated you!\n" +
                                        "The game is over!\n" +
                                        "You've lost!\n" +
                                        "Press any key to continue...");
    }
    public void Win(string monsterName, bool isTutorial = false)
    {
        if (isTutorial)
        {
            Console.Write("you've defeated " + monsterName + "!\n" +
                              "The tutorial is over!\n" +
                              "You've won!\n" +
                              "Press any key to continue...");
            return;
        }
        Console.Write("you've defeated " + monsterName + "!\n" +
                          "The round is over!\n" +
                          "You've won!\n" +
                          "Press any key to continue...");
    }

    public string RoundAction()
    {
        Menu menu = new Menu();
        string[] choosableActions = { "Attack", "Defence", "Heal" , "Exit game"};
        return menu.menuTemplate(choosableActions, "Choose your next move!:");
    }

    public override string? ToString()
    {
        return "Name: " + Name + "\nPoints: " + Points + "\nDamage: " + Damage + "\nHP: " + HP;
    } 
};