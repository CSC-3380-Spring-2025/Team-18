using Godot;
using System;

public partial class Enemy : Area2D
{
	[Export] public int MaxHealth { get; set; } = 50;
	[Export] public int Damage { get; set; } = 10;
	[Export] public float Speed { get; set; } = 100f;
	[Export] public float ChaseRadius { get; set; } = 200f;
	[Export] public float AttackRange { get; set; } = 20f;
	[Export] public String EnemyType {get; set; } = "Default";
	[Export] public bool EnemyRandom {get; set;} = false; //Can enemy be random spawn?
	[Export] public int RandIndex1 {get; set;} = 0; //Lowest enemy index for randomization
	[Export] public int RandIndex2 {get; set;} = 0; //high end of indexes 

	[Export] public NodePath[] PatrolPointsPaths { get; set; } = new NodePath[0];
	
	[Signal] public delegate void EnemyChangedEventHandler();
	[Signal] public delegate void EnemyNodeEventHandler(String enemyName); //sends to combat
	[Signal] public delegate void TelephoneEventHandler(String enemyName); //sends to self

	public Node area = null;
	public Node specificGuy = null;

	private Node currentMenu = null;
	private Vector2[] patrolPoints = new Vector2[0];
	private int currentPatrolIndex = 0;
	private int currentHealth;
	private Node2D player;
	private float attackCooldown = 1.5f;
	private float attackTimer = 0f;
	public AnimatedSprite2D enemy;
	public Node screens;
	public Node main;
	public override void _Ready()
	{
		main = GetTree().Root.GetChild(-1);
		screens = main.FindChild("Screens");
		

		
		enemy =  GetNode<AnimatedSprite2D>("Appearance");
		if(EnemyRandom){
			Randomize(RandIndex1, RandIndex2);
		} else {
			ChangeEnemy(EnemyType);
		}
		
		currentHealth = MaxHealth;
		

		patrolPoints = new Vector2[PatrolPointsPaths.Length];
		for (int i = 0; i < PatrolPointsPaths.Length; i++)
		{
			var pointNode = GetNode<Node2D>(PatrolPointsPaths[i]);
			patrolPoints[i] = pointNode.Position;
		}

		var rootScene = GetTree().Root;
		player = rootScene?.GetNode<Node2D>("/root/Main/World/Player");
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
		
		if (velocity.Length() > 0){
			enemy.Play();
		}
		
	}

	private void Attack(float dt)
	{
		AttackRange = 0f;
		Speed = 0;
		ChaseRadius = 0f;
		Node counter = screens.FindChild("Combat");
		if( counter != null ){
			GD.Print("Has Scene Already");
		} else{
			PackedScene scene = GD.Load<PackedScene>("res://Scenes/Combat.tscn");
			currentMenu = scene.Instantiate();
			screens.AddChild(currentMenu);
			EmitSignal(SignalName.Telephone, this.Name);
		}
	}

	public void TakeDamage(int amt)
	{
		currentHealth -= amt;
	}

	public void SpawnCombat(Node2D player){
		AttackRange = 0f;
		Speed = 0;
		ChaseRadius = 0f;
		Node counter = screens.FindChild("Combat");
		if( counter != null ){
			GD.Print("Has Scene Already");
		} else{
			PackedScene scene = GD.Load<PackedScene>("res://Scenes/Combat.tscn");
			currentMenu = scene.Instantiate();
			screens.CallDeferred(Node.MethodName.AddChild, currentMenu);
			//screens.AddChild(currentMenu);
			counter = screens.FindChild("Combat");
			EmitSignal(SignalName.Telephone, this.Name);
		}
	}

//This statement allows for automatic switching of enemy stats and shit 
	public void ChangeEnemy(String enemyType){
		
		/*"hey what the fuck is this?" im so glad you asked. it allows for the randomization of enemies, 
		using the second half of the when statements. :)
		*/
		switch(enemyType){
			case String n when(enemyType == "Default" || enemyType == "1"): 
				MaxHealth = 50;
				Damage = 10;
				Speed = 100;
				ChaseRadius = 200f;
				AttackRange = 20f;
				enemy.Animation = ("Default");
				break;
			case String n when(enemyType == "TrainingDummy" || enemyType == "2"):
				MaxHealth = 10;
				Damage = 1;
				Speed = 0;
				ChaseRadius = 0f;
				AttackRange = 0f;
				enemy.Animation = ("TrainingDummy");
				break;
			case String n when(enemyType == "Droid_1" || enemyType == "3"):
				MaxHealth = 10;
				Damage = 1;
				Speed = 100;
				ChaseRadius = 500f;
				AttackRange = 20f;
				enemy.Animation = ("Droid_1");
				break;
			case String n when(enemyType == "DroidSwarm" || enemyType == "4"):
				MaxHealth = 10;
				Damage = 10;
				Speed = 300;
				ChaseRadius = 300f;
				AttackRange = 20f;
				enemy.Animation = ("DroidSwarm");
				break;
		}
		
		
	}

public void Sender(String enemyPath){
	GD.Print("Sent from Sender "+enemyPath);
		EmitSignal(SignalName.EnemyNode, this.Name);

}

	public void Death(String enemyPath){
		GD.Print("Killing "+enemyPath);
		
		area = GetTree().Root.FindChild("Main").FindChild("World").GetChild(-1);
		specificGuy = area.FindChild(enemyPath);
		specificGuy.QueueFree();
		
		foreach(Enemy i in area.GetChildren()){
			if(Speed == 0 && ChaseRadius == 0f && EnemyType != "TrainingDummy"){
				this.QueueFree();
			}
		}
	}

	
	public void Randomize(int x, int y){
		var rand = new Random();
		int roll = rand.Next(x,y);
		ChangeEnemy(""+roll);
	}

}
