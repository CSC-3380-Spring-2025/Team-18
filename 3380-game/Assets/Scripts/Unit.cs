using Godot;
using System;

public partial class Unit : Node
{
	[Export] public string UnitName = "Unit";
	[Export] public int maxHp = 100;
	[Export] public int attackPower = 10;
	[Export] public int healPower = 5;
	[Export] public HealthBar healthBar;

	private int currentHp;
	private Random rng = new Random();

	public bool IsDead => currentHp <= 0;

	public override void _Ready()
	{
		currentHp = maxHp;
		healthBar.InitHealth(maxHp);
	}

	public int Attack(Unit target)
	{
		float mod = (float)(rng.NextDouble() / 2 + 0.75);
		int damage = Mathf.RoundToInt(attackPower * mod);
		target.TakeDamage(damage);
		return damage;
	}

	public void TakeDamage(int amount)
	{
		currentHp = Mathf.Max(currentHp - amount, 0);
		healthBar.Health = currentHp;
	}

	public int Heal()
	{
		float mod = (float)(rng.NextDouble() / 2 + 0.75);
		int heal = Mathf.RoundToInt(healPower * mod);
		currentHp = Mathf.Min(currentHp + heal, maxHp);
		healthBar.Health = currentHp;
		return heal;
	}

	public int ForceAttack(Unit target)
	{
		int basePower = attackPower + 10;
		float mod = (float)(rng.NextDouble() / 2 + 0.75);
		int damage = Mathf.RoundToInt(basePower * mod);
		target.TakeDamage(damage);
		return damage;
	}
}
