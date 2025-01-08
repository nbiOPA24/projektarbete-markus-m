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
                player.block = 0;
                player.attack = rnd.Next(1, 5);
                enemy.currantHealth = enemy.currantHealth - (player.attack + player.level - enemy.block);
                Console.WriteLine($"" + player.name + " attackerar och g�r " + (player.attack + player.level) + " skada");
                playerTurnOver = true;
                player.currantEnergy--;
            }
            //randomizar ett v�rde och sparar det till n�sta g�ng fienden attackerar och subtraherar skadan med v�rdet damageBlockedPlayer
            else if (textInput == "Blocka" && player.currantEnergy > 0)
            {
                player.block = 0;
                player.block = rnd.Next(1, 4);
                Console.WriteLine($"" + player.name + " blockerar f�r " + player.block + " skada");
                playerTurnOver = true;
                player.currantEnergy--;
            }
            //randomizar ett v�rde och l�gger till detta v�rde till currantEnergy
            else if (textInput == "Vila")
            {
                player.block = 0;
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
                    switch(rnd.Next(1, 3))
                    {
                        case 1:
                            enemy.block = 0;
                            //randomizar ett v�rde och subtraherar spelarens health med det
                            enemy.attack = rnd.Next(1, 5);
                            player.currantHealth = player.currantHealth - (enemy.attack + enemy.level - player.block);
                            Console.WriteLine($"" + enemy.name + " attackerar och g�r " + (enemy.attack + enemy.level) + " skada");
                            playerTurnOver = false;
                            enemy.currantEnergy--;
                            break;
                        case 2:
                            enemy.block = 0;
                            enemy.block = rnd.Next(1, 4);
                            Console.WriteLine($"" + enemy.name + " blockerar f�r " + enemy.block + " skada");
                            enemy.currantEnergy--;
                            playerTurnOver = false;
                            break;
                    }
                }
                //randomizar ett v�rde och ger det till fienden som currantEnergy
                else
                {
                    enemy.block = 0;
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
                Program.player = player;
                break;
            }
            //om spelaren har noll eller mindre currantHealth d�r den
            if (player.currantHealth <= 0)
            {
                Console.WriteLine($"" + player.name + " �r d�d");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}