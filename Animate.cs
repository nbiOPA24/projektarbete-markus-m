using System;
namespace Animate;
public class animate
{
	public string name;
	public int maxHealth;
	public int currantHealth;
	public int maxEnergy;
	public int currantEnergy;
	public animate(string name, int maxHealth, int currantHealth, int maxEnergy, int currantEnergy)
	{
		this.name = name;
		this.maxHealth = maxHealth;
		this.currantHealth = currantHealth;
		this.maxEnergy = maxEnergy;
		this.currantEnergy = currantEnergy;
	}
}
