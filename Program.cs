using System;

class Program
{
    static void Main(string[] args)
    {
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
            if (rnd.Next(0, 11) == 1)
            {
                while (true)
                {
                    Console.WriteLine("Battle");
                    break;
                }
            }
        }
    }
}