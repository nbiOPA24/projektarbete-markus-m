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
        // om du vill ändra max energi eller max health scaling får du göra det i animate updateMaxAttributes
        var enemy = new animate(enemyName, enemyMaxHealth, enemyCurrantHealth, 5, 5, 0, enemyLevel, 0, 0, 0, 0);
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
                player.block = 0;
                player.attack = rnd.Next(1, 5);
                enemy.currantHealth = enemy.currantHealth - (player.attack + player.level - enemy.block);
                Console.WriteLine($"" + player.name + " attackerar och gör " + (player.attack + player.level) + " skada");
                playerTurnOver = true;
                player.currantEnergy--;
            }
            //randomizar ett värde och sparar det till nästa gång fienden attackerar och subtraherar skadan med värdet damageBlockedPlayer
            else if (textInput == "Blocka" && player.currantEnergy > 0)
            {
                player.block = 0;
                player.block = rnd.Next(1, 4);
                Console.WriteLine($"" + player.name + " blockerar för " + player.block + " skada");
                playerTurnOver = true;
                player.currantEnergy--;
            }
            //randomizar ett värde och lägger till detta värde till currantEnergy
            else if (textInput == "Vila")
            {
                player.block = 0;
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
            if (playerTurnOver)
            {
                if (enemy.currantEnergy > 0)
                {
                    switch(rnd.Next(1, 3))
                    {
                        case 1:
                            enemy.block = 0;
                            //randomizar ett värde och subtraherar spelarens health med det
                            enemy.attack = rnd.Next(1, 5);
                            player.currantHealth = player.currantHealth - (enemy.attack + enemy.level - player.block);
                            Console.WriteLine($"" + enemy.name + " attackerar och gör " + (enemy.attack + enemy.level) + " skada");
                            playerTurnOver = false;
                            enemy.currantEnergy--;
                            break;
                        case 2:
                            enemy.block = 0;
                            enemy.block = rnd.Next(1, 4);
                            Console.WriteLine($"" + enemy.name + " blockerar för " + enemy.block + " skada");
                            enemy.currantEnergy--;
                            playerTurnOver = false;
                            break;
                    }
                }
                //randomizar ett värde och ger det till fienden som currantEnergy
                else
                {
                    enemy.block = 0;
                    restEnergyGained = rnd.Next(1, 5);
                    enemy.currantEnergy = enemy.currantEnergy + restEnergyGained;
                    if (enemy.currantEnergy > enemy.maxEnergy)
                    {
                        enemy.currantEnergy = enemy.maxEnergy;
                    }
                    Console.WriteLine($"" + enemy.name + " vilar för " + restEnergyGained + " energi");
                    playerTurnOver = false;
                }
            }

            // extra checks orelaterat till fiendens eller spelarens turns

            //ser till att spelarens health inte går över max health
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
                Console.WriteLine($"" + player.name + " har nu " + player.experiance + " xp");
                Program.player = player;
                break;
            }
            //om spelaren har noll eller mindre currantHealth dör den
            if (player.currantHealth <= 0)
            {
                Console.WriteLine($"" + player.name + " är död");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}