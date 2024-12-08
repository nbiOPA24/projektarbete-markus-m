using System;
public class animate
{
	public string name;
	public int maxHealth;
	public int currantHealth;
	public int maxEnergy;
	public int currantEnergy;
	public int experiance;
	public int level;
	public int attack;
	public int block;
	public int x;
	public int y;
	public animate(string name, int maxHealth, int currantHealth, int maxEnergy, int currantEnergy, int experiance, int level, int attack, int block, int x, int y)
	{
		this.name = name;
		this.maxHealth = maxHealth;
		this.currantHealth = currantHealth;
		this.maxEnergy = maxEnergy;
		this.currantEnergy = currantEnergy;
		this.experiance = experiance; 
		this.level = level;
		this.attack = attack;
		this.block = block;
		this.x = x;
		this.y = y;
	}
	// updaterar så maxHealth och maxEnergy har korrekt värden för sin level
	public void updateMaxAttributes()
	{
		maxHealth = level * 10;
		maxEnergy = level * 5;
	}
	// sätter currantHealth och currantEnergy till sina max värden
	public void attributesToMax()
	{
		currantHealth = maxHealth;
		currantEnergy = maxEnergy;
	}
	// tar bort rätt mängd experiance, ger en level och startar updateMaxAttributes()
	public void levelUp()
	{
		experiance = experiance - level * 10;
		level++;
		updateMaxAttributes();
    }
}
