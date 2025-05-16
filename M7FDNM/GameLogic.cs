namespace projekt
{
    internal class GameLogic
    {
        Menu menu = new Menu();
        public void GameMechanic(Player player, Monster monster, bool isTutorial = false)
        {
            int i = 0;
            do
            {
                if(player.HP <= 0)
                {
                    Console.Clear();
                    player.Lose(monster.Name);
                    Console.ReadKey();
                    menu.mainMenu();
                }
                string monsterAction = monster.MonsterAction();
                string playerAction = player.RoundAction();
                Console.WriteLine(monster.Name);
                Console.WriteLine("_________________________");
                Console.WriteLine(i + 1 + ". Round");
                if (playerAction.Equals("Exit game"))
                {
                    Menu menu = new Menu();
                    Save save = new Save(player);
                    menu.mainMenu();
                }
                if (monsterAction.Equals("Attack") && playerAction.Equals("Defence"))
                {
                    monster.HP -= 5;
                    Console.WriteLine("You successfully blocked the enemy's attack! " + monster.Name + ": -5HP");
                }
                if (playerAction.Equals("Attack") && monsterAction.Equals("Defence"))
                {
                    player.HP -= 5;
                    Console.WriteLine("The enemy successfully blocked the your attack! " + player.Name + ": -5HP");
                }
                if (monsterAction.Equals("Attack") && playerAction.Equals("Attack"))
                {
                    monster.HP -= player.Damage;
                    player.HP -= monster.Damage;
                    Console.WriteLine("The enemy has landed a hit on you! " + player.Name + ": -" + monster.Damage + "HP");
                    Console.WriteLine("You landed a hit on the enemy! " + monster.Name + ": -" + player.Damage + "HP");
                }
                if (monsterAction.Equals("Defence") && playerAction.Equals("Defence"))
                {
                    Console.WriteLine("You both blocked each other's hit.");
                }
                if(playerAction.Equals("Heal") && monsterAction.Equals("Attack"))
                {
                    player.HP += 50;
                    if (player.HP > 100)
                    {
                        player.HP = 100;
                    }
                    Console.WriteLine("You've healed yourself! " + player.Name + ": + 50HP");
                    player.HP -= monster.Damage;
                    Console.WriteLine("The enemy has landed a hit on you! " + player.Name + ": -" + monster.Damage + "HP");
                }
                if (playerAction.Equals("Heal") && monsterAction.Equals("Defence"))
                {
                    player.HP += 50;
                    if (player.HP > 100)
                    {
                        player.HP = 100;
                    }
                    Console.WriteLine("You've healed yourself! " + player.Name + ": + 50HP");
                    Console.WriteLine("The enemy stayed behind his shield!");
                }
                Console.WriteLine(monster.Name + ": " + monster.HP + " HP" + " , " + player.Name + ": " + player.HP + " HP");

                if (isTutorial)
                {
                    if (monster.HP <= 0)
                    {
                        player.Win(monster.Name, true);
                        break;
                    }
                    if (player.HP <= 0)
                    {
                        player.Lose (monster.Name, true);
                        break;
                    }
                }
                else
                {
                    if (monster.HP <= 0)
                    {
                        player.Win(monster.Name);
                        player.Damage += 10;
                        player.Points += 100;
                        player.HP = 100;
                        break;
                    }
                    if (player.HP <= 0)
                    {
                        player.Lose(monster.Name);
                        Console.ReadKey();
                        Save save = new Save(player);
                        menu.mainMenu();
                        break;
                    }
                }
                i++;
                Console.Write("\n\nPress enter to continue...");
                Console.ReadKey();
            }
            while (player.HP > 0 && monster.HP > 0);
        }
    }
}