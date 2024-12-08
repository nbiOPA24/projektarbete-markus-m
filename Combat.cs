using System;

public class combat
{
    public static void combatStart()
    {
        var player = Program.player;
        Random rnd = new Random();
        string textInput;
        string enemyName = "";
        int enemyLevel = 0;
        int enemyMaxHealth = 0;
        int enemyCurrantHealth = 0;
        int damageBlockedEnemy = 0;
        int damageBlockedPlayer = 0;
        int damageDealt = 0;
        int restEnergyGained = 0;
        bool playerTurnOver = false;
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
        // om du vill �ndra max energi eller max health scaling f�r du g�ra det i animate updateMaxAttributes
        var enemy = new animate(enemyName, enemyMaxHealth, enemyCurrantHealth, 5, 5, 0, enemyLevel, 0, 0, 0, 0);
        enemy.updateMaxAttributes();
        enemy.attributesToMax();
        player.attributesToMax();
        Console.WriteLine($"Strid med " + enemyName + " level: " + enemyLevel);
        //Combat loop
        while (true)
        {
            //printar fiendens och dina liv och energi v�rden samt de olika saker du kan g�ra i konsolen
            Console.WriteLine($"////Attackera////Blocka////Vila////");
            Console.WriteLine($"" + player.name + " hp: " + player.currantHealth + " av max " + player.maxHealth);
            Console.WriteLine($"" + player.name + " energi: " + player.currantEnergy + " av max " + player.maxEnergy);
            Console.WriteLine($"" + enemy.name + " hp: " + enemy.currantHealth + " av max " + enemy.maxHealth);
            Console.WriteLine($"" + enemy.name + " energi: " + enemy.currantEnergy + " av max " + enemy.maxEnergy);
            textInput = Console.ReadLine();
            Console.WriteLine($"///////////////////////////////////");

            // spelarens del av combat delen av scriptet

            //randomizar ett v�rde och subtraherar fiendens health med det
            if (textInput == "Attackera" && player.currantEnergy > 0)
            {
                damageBlockedPlayer = 0;
                damageDealt = rnd.Next(1, 5);
                enemy.currantHealth = enemy.currantHealth - (damageDealt + player.level - damageBlockedEnemy);
                Console.WriteLine($"" + player.name + " attackerar och g�r " + damageDealt + " skada");
                playerTurnOver = true;
                player.currantEnergy--;
            }
            //randomizar ett v�rde och sparar det till n�sta g�ng fienden attackerar och subtraherar skadan med v�rdet damageBlockedPlayer
            else if (textInput == "Blocka" && player.currantEnergy > 0)
            {
                damageBlockedPlayer = 0;
                damageBlockedPlayer = rnd.Next(1, 4);
                Console.WriteLine($"" + player.name + " blockerar f�r " + damageBlockedPlayer + " skada");
                playerTurnOver = true;
                player.currantEnergy--;
            }
            //randomizar ett v�rde och l�gger till detta v�rde till currantEnergy
            else if (textInput == "Vila")
            {
                damageBlockedPlayer = 0;
                restEnergyGained = rnd.Next(1, 5);
                player.currantEnergy = player.currantEnergy + restEnergyGained;
                //ser till att currantEnergy inte blir st�rre �n maxEnergy
                if (player.currantEnergy > player.maxEnergy)
                {
                    player.currantEnergy = player.maxEnergy;
                }
                Console.WriteLine($"" + player.name + " vilar f�r " + restEnergyGained + " energi");
                playerTurnOver = true;
            }

            // fiendens turn del av combat delen av scriptet (om playerTurnOver == true)
            if (playerTurnOver)
            {
                if (enemy.currantEnergy > 0)
                {
                    //randomizar ett v�rde f�r att best�mma vad fienden ska g�ra
                    int enemyChoice = rnd.Next(1, 3);
                    if (enemyChoice == 1 && enemy.currantEnergy > 0)
                    {
                        damageBlockedEnemy = 0;
                        //randomizar ett v�rde och subtraherar spelarens health med det
                        damageDealt = rnd.Next(1, 5);
                        player.currantHealth = player.currantHealth - (damageDealt + enemy.level - damageBlockedPlayer);
                        Console.WriteLine($"" + enemy.name + " attackerar och g�r " + damageDealt + " skada");
                        playerTurnOver = false;
                        enemy.currantEnergy--;
                    }
                    //randomizar ett v�rde och sparar det till n�sta g�ng spelaren attackerar och subtraherar skadan med v�rdet damageBlockedEnemy
                    else if (enemyChoice == 2 && enemy.currantEnergy > 0)
                    {
                        damageBlockedEnemy = 0;
                        damageBlockedEnemy = rnd.Next(1, 4);
                        Console.WriteLine($"" + enemy.name + " blockerar f�r " + damageBlockedEnemy + " skada");
                        enemy.currantEnergy--;
                        playerTurnOver = false;
                    }
                }
                //randomizar ett v�rde och ger det till fienden som currantEnergy
                else
                {
                    damageBlockedEnemy = 0;
                    restEnergyGained = rnd.Next(1, 5);
                    enemy.currantEnergy = enemy.currantEnergy + restEnergyGained;
                    if (enemy.currantEnergy > enemy.maxEnergy)
                    {
                        enemy.currantEnergy = enemy.maxEnergy;
                    }
                    Console.WriteLine($"" + enemy.name + " vilar f�r " + restEnergyGained + " energi");
                    playerTurnOver = false;
                }
            }

            // extra checks orelaterat till fiendens eller spelarens turns

            //ser till att spelarens health inte g�r �ver max health
            if (player.currantHealth > player.maxHealth)
            {
                enemy.currantHealth = enemy.maxHealth;
            }
            //ser till at fiendens health g�r �ver max health
            if (enemy.currantHealth > enemy.maxHealth)
            {
                enemy.currantHealth = enemy.maxHealth;
            }
            //om fienden har noll eller mindre currantHealth d�r den
            if (enemy.currantHealth <= 0)
            {
                player.experiance = player.experiance + enemy.maxHealth - enemy.currantHealth;
                Console.WriteLine($"" + enemy.name + " �r dr�pt");
                Console.WriteLine($"" + player.name + " har nu " + player.experiance + " xp");
                Console.WriteLine($"x: " + player.x + " y: " + player.y);
                Console.WriteLine($"w = up, a = left, s = down, d = right");
                Program.player = player;
                //Console.WriteLine($"g� till x: " + endGoalX + " y: " + endGoalY);
                break;
            }
            //om spelaren har noll eller mindre currantHealth d�r den
            if (player.currantHealth <= 0)
            {
                Console.WriteLine($"" + player.name + " �r d�d");
                break;
            }
        }
    }
}