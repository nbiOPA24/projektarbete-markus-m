using System;
namespace Animate;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose your name");
        string playerName = Console.ReadLine();
        var player = new animate(playerName, 10, 10, 5, 5);
        //används i strid
        string enemyName = "";
        int enemyMaxHealth = 0;
        int enemyCurrantHealth = 0;
        int damageBlockedEnemy = 0;
        int damageBlockedPlayer = 0;
        int damageDealt = 0;
        int restEnergyGained = 0;
        bool playerTurnOver = false;
        int overflow = 0;

        Random rnd = new Random();
        string textInput;
        int x = 0;
        int y = 0;
        Console.WriteLine($"x: " + x + " y: " + y);
        Console.WriteLine($"w = up, a = left, s = down, d = right");
        while (true)
        {
            if (player.currantHealth < 1)
            {
                Console.ReadLine();
                break;
            }
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
                var enemy = new animate(enemyName, enemyMaxHealth, enemyCurrantHealth, 5, 5);
                Console.WriteLine($"Battle with " + enemyName);
                player.currantHealth = player.maxHealth;
                while (true)
                {
                    Console.WriteLine($"////Attackera////Blocka////Vila////");
                    Console.WriteLine($"Spelarens hp: " + player.currantHealth + " av max " + player.maxHealth);
                    Console.WriteLine($"Spelarens energi: " + player.currantEnergy + " av max " + player.maxEnergy);
                    Console.WriteLine($"Fiendens hp: " + enemy.currantHealth + " av max " + enemy.maxHealth);
                    Console.WriteLine($"Fiendens energi: " + enemy.currantEnergy + " av max " + enemy.maxEnergy);
                    textInput = Console.ReadLine();
                    Console.WriteLine($"///////////////////////////////////");
                    if (textInput == "Attackera" && player.currantEnergy > 0)
                    {
                        damageBlockedPlayer = 0;
                        //randomizar ett värde och subtraherar fiendens health med det
                        damageDealt = rnd.Next(1, 5);
                        enemy.currantHealth = enemy.currantHealth - (damageDealt - damageBlockedEnemy);
                        Console.WriteLine($"" + player.name + " attackerar och gör " + damageDealt + " skada");
                        playerTurnOver = true;
                        player.currantEnergy--;
                    }
                    else if (textInput == "Blocka" && player.currantEnergy > 0)
                    {
                        damageBlockedPlayer = 0;
                        //randomizar ett värde och sparar det till nästa gång fienden attackerar och subtraherar skadan med värdet damageBlockedPlayer
                        damageBlockedPlayer = rnd.Next(1, 4);
                        Console.WriteLine($"" + player.name + " blockerar för " + damageBlockedPlayer + " skada");
                        playerTurnOver = true;
                        player.currantEnergy--;
                    }
                    else if (textInput == "Vila")
                    {
                        //randomizar ett värde och lägger till detta värde till currantEnergy
                        damageBlockedPlayer = 0;
                        restEnergyGained = rnd.Next(1, 5);
                        player.currantEnergy = player.currantEnergy + restEnergyGained;
                        //ser till att currantEnergy inte blir större än maxEnergy
                        if (player.currantEnergy > player.maxEnergy)
                        {
                            overflow = player.maxEnergy - player.currantEnergy;
                            player.currantEnergy = player.currantEnergy + overflow;
                        }
                        Console.WriteLine($"" + player.name + " vilar för " + restEnergyGained + " energi");
                        playerTurnOver = true;
                    }
                    //randomizar ett värde för att bestämma vad fienden ska göra
                    int enmAttack = rnd.Next(1, 4);
                    if (enmAttack == 1 && playerTurnOver && enemy.currantEnergy > 0)
                    {
                        damageBlockedEnemy = 0;
                        //randomizar ett värde och subtraherar spelarens health med det
                        damageDealt = rnd.Next(1, 5);
                        player.currantHealth = player.currantHealth - (damageDealt - damageBlockedPlayer);
                        Console.WriteLine($"" + enemy.name + " attackerar och gör " + damageDealt + " skada");
                        playerTurnOver = false;
                        enemy.currantEnergy--;
                    }
                    else if (enmAttack == 2 && playerTurnOver && enemy.currantEnergy > 0)
                    {
                        damageBlockedEnemy = 0;
                        //randomizar ett värde och sparar det till nästa gång spelaren attackerar och subtraherar skadan med värdet damageBlockedEnemy
                        damageBlockedEnemy = rnd.Next(1, 4);
                        Console.WriteLine($"" + enemy.name + " blockerar för " + damageBlockedEnemy + " skada");
                        enemy.currantEnergy--;
                        playerTurnOver = false;
                    }
                    else if ((enmAttack == 3 && playerTurnOver) || (enemy.currantEnergy <= 0 && playerTurnOver))
                    {
                        damageBlockedEnemy = 0;
                        //randomizar ett värde och ger det till fienden som currantEnergy
                        restEnergyGained = rnd.Next(1, 5);
                        enemy.currantEnergy = enemy.currantEnergy + restEnergyGained;
                        if (enemy.currantEnergy > enemy.maxEnergy)
                        {
                            overflow = enemy.maxEnergy - enemy.currantEnergy;
                            enemy.currantEnergy = enemy.currantEnergy + overflow;
                        }
                        Console.WriteLine($"" + enemy.name + " vilar för " + restEnergyGained + " energi");
                        playerTurnOver = false;
                    }
                    //ser till at spelarens health går över max health
                    if (player.currantHealth > player.maxHealth)
                    {
                        overflow = player.maxHealth - player.currantHealth;
                        player.currantHealth = player.currantHealth + overflow;
                    }
                    //ser till at fiendens health går över max health
                    if (enemy.currantHealth > enemy.maxHealth)
                    {
                        overflow = enemy.maxHealth - enemy.currantHealth;
                        enemy.currantHealth = enemy.currantHealth + overflow;
                    }
                    //om fienden har noll eller mindre currantHealth dör den
                    if (enemy.currantHealth <= 0)
                    {
                        Console.WriteLine($"" + enemy.name + " är dräpt");
                        Console.WriteLine($"x: " + x + " y: " + y);
                        Console.WriteLine($"w = up, a = left, s = down, d = right");
                        break;
                    }
                    //om spelaren har noll eller mindre currantHealth dör den
                    if (player.currantHealth <= 0)
                    {
                        Console.WriteLine($"" + player.name + " är död");
                        break;
                    }
                }
            }
        }
    }
}