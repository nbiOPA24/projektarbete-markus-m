using System;
using System.ComponentModel;
class Program
{
    static void Main(string[] args)
    {
        //misc (isntansiering av spelaren och andra orelaterade saker)
        //int experiance = 0;
        //int level = 1;
        Console.WriteLine("Choose your name");
        string playerName = Console.ReadLine();
        var player = new animate(playerName, 10, 10, 5, 5, 0, 1);
        Random rnd = new Random();
        string textInput;
        int x = 0;
        int y = 0;
        Console.WriteLine($"x: " + x + " y: " + y);
        Console.WriteLine($"w = up, a = left, s = down, d = right");

        //används i strid
        string enemyName = "";
        int enemyLevel = 0;
        int enemyMaxHealth = 0;
        int enemyCurrantHealth = 0;
        int damageBlockedEnemy = 0;
        int damageBlockedPlayer = 0;
        int damageDealt = 0;
        int restEnergyGained = 0;
        bool playerTurnOver = false;
        while (true)
        {
            if (player.experiance >= (player.level * 10))
            {
                player.levelUp();
            }
            if (player.currantHealth < 1)
            {
                Console.ReadLine();
                break;
            }
            // movment switch
            switch (Console.ReadLine())
            {
                case "w":
                    y++;
                    Console.WriteLine($"x: " + x + " y: " + y);
                    break;
                case "a":
                    x--;
                    Console.WriteLine($"x: " + x + " y: " + y);
                    break;
                case "s":
                    y--;
                    Console.WriteLine($"x: " + x + " y: " + y);
                    break;
                case "d":
                    x++;
                    Console.WriteLine($"x: " + x + " y: " + y);
                    break;
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
                enemyLevel = rnd.Next(1, 3);
                // om du vill ändra max energi eller max health scaling får du göra det i animate updateMaxAttributes
                var enemy = new animate(enemyName, enemyMaxHealth, enemyCurrantHealth, 5, 5, 0, enemyLevel);
                enemy.updateMaxAttributes();
                enemy.attributesToMax();
                player.attributesToMax();
                Console.WriteLine($"Strid med " + enemyName + " level: " + enemyLevel);
                //Combat loop
                while (true)
                {
                    //printar fiendens och dina liv och energi värden samt de olika saker du kan göra i konsolen
                    Console.WriteLine($"////Attackera////Blocka////Vila////");
                    Console.WriteLine($"" + player.name + " hp: " + player.currantHealth + " av max " + player.maxHealth);
                    Console.WriteLine($"" + player.name + " energi: " + player.currantEnergy + " av max " + player.maxEnergy);
                    Console.WriteLine($"" + enemy.name + " hp: " + enemy.currantHealth + " av max " + enemy.maxHealth);
                    Console.WriteLine($"" + enemy.name + " energi: " + enemy.currantEnergy + " av max " + enemy.maxEnergy);
                    textInput = Console.ReadLine();
                    Console.WriteLine($"///////////////////////////////////");

                    // spelarens del av combat delen av scriptet

                    //randomizar ett värde och subtraherar fiendens health med det
                    if (textInput == "Attackera" && player.currantEnergy > 0)
                    {
                        damageBlockedPlayer = 0;
                        damageDealt = rnd.Next(1, 5);
                        enemy.currantHealth = enemy.currantHealth - (damageDealt + player.level - damageBlockedEnemy);
                        Console.WriteLine($"" + player.name + " attackerar och gör " + damageDealt + " skada");
                        playerTurnOver = true;
                        player.currantEnergy--;
                    }
                    //randomizar ett värde och sparar det till nästa gång fienden attackerar och subtraherar skadan med värdet damageBlockedPlayer
                    else if (textInput == "Blocka" && player.currantEnergy > 0)
                    {
                        damageBlockedPlayer = 0;
                        damageBlockedPlayer = rnd.Next(1, 4);
                        Console.WriteLine($"" + player.name + " blockerar för " + damageBlockedPlayer + " skada");
                        playerTurnOver = true;
                        player.currantEnergy--;
                    }
                    //randomizar ett värde och lägger till detta värde till currantEnergy
                    else if (textInput == "Vila")
                    {
                        damageBlockedPlayer = 0;
                        restEnergyGained = rnd.Next(1, 5);
                        player.currantEnergy = player.currantEnergy + restEnergyGained;
                        //ser till att currantEnergy inte blir större än maxEnergy
                        if (player.currantEnergy > player.maxEnergy)
                        {
                            player.currantEnergy = player.maxEnergy;
                        }
                        Console.WriteLine($"" + player.name + " vilar för " + restEnergyGained + " energi");
                        playerTurnOver = true;
                    }

                    // fiendens turn del av combat delen av scriptet (om playerTurnOver == true)

                    //randomizar ett värde för att bestämma vad fienden ska göra
                    int enmAttack = rnd.Next(1, 4);
                    if (enmAttack == 1 && playerTurnOver && enemy.currantEnergy > 0)
                    {
                        damageBlockedEnemy = 0;
                        //randomizar ett värde och subtraherar spelarens health med det
                        damageDealt = rnd.Next(1, 5);
                        player.currantHealth = player.currantHealth - (damageDealt + enemy.level - damageBlockedPlayer);
                        Console.WriteLine($"" + enemy.name + " attackerar och gör " + damageDealt + " skada");
                        playerTurnOver = false;
                        enemy.currantEnergy--;
                    }
                    //randomizar ett värde och sparar det till nästa gång spelaren attackerar och subtraherar skadan med värdet damageBlockedEnemy
                    else if (enmAttack == 2 && playerTurnOver && enemy.currantEnergy > 0)
                    {
                        damageBlockedEnemy = 0;
                        damageBlockedEnemy = rnd.Next(1, 4);
                        Console.WriteLine($"" + enemy.name + " blockerar för " + damageBlockedEnemy + " skada");
                        enemy.currantEnergy--;
                        playerTurnOver = false;
                    }
                    //randomizar ett värde och ger det till fienden som currantEnergy
                    else if ((enmAttack == 3 && playerTurnOver) || (enemy.currantEnergy <= 0 && playerTurnOver))
                    {
                        damageBlockedEnemy = 0;
                        restEnergyGained = rnd.Next(1, 5);
                        enemy.currantEnergy = enemy.currantEnergy + restEnergyGained;
                        if (enemy.currantEnergy > enemy.maxEnergy)
                        {
                            enemy.currantEnergy = enemy.maxEnergy;
                        }
                        Console.WriteLine($"" + enemy.name + " vilar för " + restEnergyGained + " energi");
                        playerTurnOver = false;
                    }

                    // extra checks orelaterat till fiendens eller spelarens turns

                    //ser till at spelarens health går över max health
                    if (player.currantHealth > player.maxHealth)
                    {
                        enemy.currantHealth = enemy.maxHealth;
                    }
                    //ser till at fiendens health går över max health
                    if (enemy.currantHealth > enemy.maxHealth)
                    {
                        enemy.currantHealth = enemy.maxHealth;
                    }
                    //om fienden har noll eller mindre currantHealth dör den
                    if (enemy.currantHealth <= 0)
                    {
                        player.experiance = player.experiance + enemy.maxHealth - enemy.currantHealth;
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