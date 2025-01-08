using System;
using System.ComponentModel;
public class Program
{
    public static animate player = new animate("", 10, 10, 5, 5, 0, 1, 0, 0, 0, 0);
    static void Main(string[] args)
    {
        char[,] map = new char[7, 7];
        //misc (isntansiering av spelaren och andra orelaterade saker)
        int endGoalX;
        int endGoalY;
        Console.WriteLine("Choose your name");
        player.name = Console.ReadLine();
        player.maxHealth = 10;
        player.maxEnergy = 5;
        player.currantHealth = 10;
        player.currantEnergy = 5;
        player.level = 1;
        Random rnd = new Random();
        endGoalX = rnd.Next(-20, 21);
        endGoalY = rnd.Next(-20, 21);
        Console.WriteLine($"x: " + player.x + " y: " + player.y);
        Console.WriteLine($"w = up, a = left, s = down, d = right");
        Console.WriteLine($"gå till x: " + endGoalX + " y: " + endGoalY);

        //används i strid
        while (true)
        {
            //kollar om du har level * experiance så du kan levla up
            if (player.experiance >= (player.level * 10))
            {
                player.levelUp();
                Console.WriteLine("Level up!");
            }
            //om du har mindre health än noll stänger denna spelet
            if (player.currantHealth < 1)
            {
                break;
            }
            // movment switch
            switch (Console.ReadLine())
            {
                case "w":
                    player.y++;
                    Console.WriteLine($"x: " + player.x + " y: " + player.y);
                    break;
                case "a":
                    player.x--;
                    Console.WriteLine($"x: " + player.x + " y: " + player.y);
                    break;
                case "s":
                    player.y--;
                    Console.WriteLine($"x: " + player.x + " y: " + player.y);
                    break;
                case "d":
                    player.x++;
                    Console.WriteLine($"x: " + player.x + " y: " + player.y);
                    break;
            }
            //kollar om du vann
            if (player.x == endGoalX && player.y == endGoalY)
            {
                Console.WriteLine("Victory");
                break;
            }
            //randomizar attacker av fiender
            if (rnd.Next(0, 6) == 1)
            {
                combat.combatStart();
                Console.WriteLine($"x: " + player.x + " y: " + player.y);
                Console.WriteLine($"w = up, a = left, s = down, d = right");
                Console.WriteLine($"gå till x: " + endGoalX + " y: " + endGoalY);
            }
        }
        // ser till så spelaren ser slutmedelanden
        Console.ReadLine();
    }
    public void showMap()
    {
        for(int i = 0; i < 7; i++)
        {
            Console.WriteLine("");
            for(int j = 0; j < 7; j++)
            {
                Console.Write("[" + Program.map(i, j) + "]");
            }
        }
    }
}