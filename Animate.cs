using System;
namespace Animate;
public class animate
{
	public string name;
	public int maxHealth;
	public int currantHealth;
	public animate(string name, int maxHealth, int currantHealth)
	{
		this.name = name;
		this.maxHealth = maxHealth;
		this.currantHealth = currantHealth;
	}
}
