using System;
using System.ComponentModel;
public class Program
{
    public static animate player = new animate("", 10, 10, 5, 5, 0, 1, 0, 0, 0, 0);
    public static string[,] mapString = new string[7, 7];
    static void Main(string[] args)
    {
        Console.WriteLine("Choose your name");
        player.name = Console.ReadLine();
        player.maxHealth = 10;
        player.maxEnergy = 5;
        player.currantHealth = 10;
        player.currantEnergy = 5;
        player.level = 1;
        map.nextFloor();
        map.movment();
    }
}