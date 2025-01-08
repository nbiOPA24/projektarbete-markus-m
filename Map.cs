using System;
using System.Collections.Generic;
using System.Media;
using System.Security.Policy;

public class map
{
    public static List<animate> hostiles = new List<animate>();
    public static int dungeonLevel = 0;
    public static void showMap()
    {
        Console.Clear();
        Console.WriteLine($"w = up, a = left, s = down, d = right");
        string[,] mapString = Program.mapString;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                Console.Write(mapString[i, j]);
            }
            Console.WriteLine("");
        }
    }
    public static void nextFloor()
    {
        bool exitGenerated = false;
        dungeonLevel++;
        int enemyLevel = dungeonLevel;
        hostiles.Clear();
        hostiles.TrimExcess();
        Random rnd = new Random();
        var player = Program.player;
        player.x = 3;
        player.y = 3;
        string[,] mapString = Program.mapString;
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                mapString[i, j] = "[_]";
            }
        }
        mapString[3, 3] = "[P]";
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (j != 3 && i != 3 && rnd.Next(1, 3) == 1)
                {
                    string enemyName = "";
                    switch (rnd.Next(1, 3))
                    {
                        case 1:
                            enemyName = "skeleton";
                            break;
                        case 2:
                            enemyName = "imp";
                            break;
                    }
                    mapString[i, j] = "[E]";
                    var enemy = new animate(enemyName, 0, 0, 0, 0, 0, enemyLevel, 0, 0, j, i);
                    enemy.updateMaxAttributes();
                    enemy.attributesToMax();
                    hostiles.Add(enemy);
                }
            }
        }
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (mapString[i, j] == "[_]")
                {
                    mapString[i, j] = "[H]";
                    exitGenerated = true;
                    break;
                }
            }
            if (exitGenerated)
            {
                break;
            }
        }
    }
    public static void movment()
    {
        var player = Program.player;
        string[,] mapString = Program.mapString;
        while (true)
        {
            showMap();
            switch (Console.ReadLine())
            {
                case "w":
                    player.y--;
                    mapString[player.y + 1, player.x] = "[_]";
                    break;
                case "a":
                    player.x--;
                    mapString[player.y, player.x + 1] = "[_]";
                    break;
                case "s":
                    player.y++;
                    mapString[player.y - 1, player.x] = "[_]";
                    break;
                case "d":
                    player.x++;
                    mapString[player.y, player.x - 1] = "[_]";
                    break;
            }
            if (player.y < 0)
            {
                player.y++;
            }
            else if (player.y > 6)
            {
                player.y--;
            }
            else if (player.x < 0)
            {
                player.x++;
            }
            else if (player.x > 6)
            {
                player.x--;
            }

            if (mapString[player.y, player.x] == "[E]")
            {
                for (int i = 0; i < hostiles.Count; i++)
                {
                    if(hostiles[i].x == player.x && hostiles[i].y == player.y)
                    {
                        Console.Clear();
                        combat.enemy = hostiles[i];
                        combat.combatStart();
                        hostiles.RemoveAt(i);
                    }
                }
            }
            else if (mapString[player.y, player.x] == "[H]")
            {
                Console.WriteLine("nästa nivå nådd");
                nextFloor();
            }
            mapString[player.y, player.x] = "[P]";
        }
    }
}
