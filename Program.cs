using System;
namespace Animate;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose your name");
        string playerName = Console.ReadLine();
        var player = new animate(playerName, 10, 10);
        //används i strid
        string enemyName = "";
        int enemyMaxHealth = 0;
        int enemyCurrantHealth = 0;

        Random rnd = new Random();
        string textInput;
        int x = 0;
        int y = 0;
        Console.WriteLine($"x: " + x + " y: " + y);
        Console.WriteLine($"w = up, a = left, s = down, d = right");
        while (true)
        {
            textInput = Console.ReadLine();
            if (textInput == "w")
            {
                y++;
                Console.WriteLine($"x: " + x + " y: " + y);
            }
            else if (textInput == "a")
            {
                x--;
                Console.WriteLine($"x: " + x + " y: " + y);
            }
            else if (textInput == "s")
            {
                y--;
                Console.WriteLine($"x: " + x + " y: " + y);
            }
            else if (textInput == "d")
            {
                x++;
                Console.WriteLine($"x: " + x + " y: " + y);
            }
            if (rnd.Next(0, 6) == 1)
            {
                switch (rnd.Next(1, 3))
                {
                    case 1:
                        enemyName = "skeleton";
                        enemyMaxHealth = 10;
                        enemyCurrantHealth = 10;
                        break;
                    case 2:
                        enemyName = "imp";
                        enemyMaxHealth = 10;
                        enemyCurrantHealth = 10;
                        break;
                }
                var enemy = new animate(enemyName, enemyMaxHealth, enemyCurrantHealth);
                Console.WriteLine($"Battle with " + enemyName);
                while (true)
                {
                    textInput = Console.ReadLine();
                    if (textInput == "Attack")
                    {
                        enemyCurrantHealth = enemyCurrantHealth - rnd.Next(1, 5);
                    }
                    int enmAttack = rnd.Next(1, 4)
                    if (enmAttack = 1)
                    {
                        player.currantHealth = player.currantHealth - rnd.Next(1, 5);
                    }
                    if (enemyCurrantHealth <= 0)
                    {
                        break;
                    }
                    if (player.currantHealth <= 0)
                    {
                        break;
                    }
                }
            }
        }
    }
}