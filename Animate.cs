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
	public animate(string name, int maxHealth, int currantHealth, int maxEnergy, int currantEnergy, int experiance, int level)
	{
		this.name = name;
		this.maxHealth = maxHealth;
		this.currantHealth = currantHealth;
		this.maxEnergy = maxEnergy;
		this.currantEnergy = currantEnergy;
		this.experiance = experiance; 
		this.level = level;
	}
}
