using Godot;
using System;

public partial class Enemy : Area2D
{
	[Export] public int MaxHealth { get; set; } = 50;
	[Export] public int Damage { get; set; } = 10;
	[Export] public float Speed { get; set; } = 100f;
	[Export] public float ChaseRadius { get; set; } = 200f;
	[Export] public float AttackRange { get; set; } = 20f;
	[Export] public NodePath[] PatrolPointsPaths { get; set; } = new NodePath[0];

	private Vector2[] patrolPoints = new Vector2[0];
	private int currentPatrolIndex = 0;
	private int currentHealth;
	private Node2D player;
	private float attackCooldown = 1.5f;
	private float attackTimer = 0f;

	public override void _Ready()
	{
		currentHealth = MaxHealth;

		patrolPoints = new Vector2[PatrolPointsPaths.Length];
		for (int i = 0; i < PatrolPointsPaths.Length; i++)
		{
			var pointNode = GetNode<Node2D>(PatrolPointsPaths[i]);
			patrolPoints[i] = pointNode.Position;
		}

		var rootScene = GetTree().CurrentScene;
		player = rootScene?.GetNode<Node2D>("Player");
		if (player == null)
			GD.PrintErr("Enemy: Player node not found.");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (currentHealth <= 0)
		{
			QueueFree();
			return;
		}

		Vector2 me = Position;
		Vector2 velocity = Vector2.Zero;

		if (player != null && me.DistanceTo(player.Position) < ChaseRadius)
		{
			var toPlayer = player.Position - me;
			if (toPlayer.Length() > AttackRange)
				velocity = toPlayer.Normalized() * Speed;
			else
				Attack((float)delta);
		}
		else if (patrolPoints.Length > 0)
		{
			var target = patrolPoints[currentPatrolIndex];
			var toTarget = target - me;
			if (toTarget.Length() < 10f)
				currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
			else
				velocity = toTarget.Normalized() * Speed;
		}

		Position += velocity * (float)delta;
	}

	private void Attack(float dt)
	{
		attackTimer -= dt;
		if (attackTimer <= 0f && player is Player p)
		{
			if (p.Position.DistanceTo(Position) <= AttackRange)
				p.stats.CurrentHealth -= Damage;
			attackTimer = attackCooldown;
		}
	}

	public void TakeDamage(int amt)
	{
		currentHealth -= amt;
	}
}
