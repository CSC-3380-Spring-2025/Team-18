using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	[Export] public Timer TimerNode;
	[Export] public ProgressBar DamageBar;

	private int _health;

	public int Health
	{
		get => _health;
		set => SetHealth(value);
	}

	public override void _Ready()
	{
		TimerNode = GetNode<Timer>("Timer");
		DamageBar = GetNode<ProgressBar>("DamageBar");
	}

	private void SetHealth(int newHealth)
	{
		int prevHealth = _health;
		_health = Mathf.Clamp(newHealth, 0, (int)MaxValue);
		Value = _health;

		if (_health <= 0)
		{
			QueueFree();
		}

		if (_health < prevHealth)
		{
			TimerNode.Start();
		}
		else
		{
			DamageBar.Value = _health;
		}
	}

	public void InitHealth(int health)
	{
		_health = health;
		MaxValue = health;
		Value = health;
		DamageBar.MaxValue = health;
		DamageBar.Value = health;
	}

public void OnTimerTimeout(){
	DamageBar.Value = _health;
}
}
