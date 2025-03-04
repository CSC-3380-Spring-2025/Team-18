using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 200; // How fast the player will move (pixels/sec).
	//[Signal]
	//public delegate void HitEventHandler();
	
	
	public Vector2 ScreenSize;
	
	public string species = "Human";
	public string bodyType = "b1";

public override void _Ready() //called on start
{
	ScreenSize = GetViewportRect().Size;
	
}

public override void _Process(double delta) //called in real time
{

	var velocity = Vector2.Zero; // The player's movement vector.

//input handlers
	if (Input.IsActionPressed("move_right"))
	{
		velocity.X += 1;
	}
	if (Input.IsActionPressed("move_left"))
	{
		velocity.X -= 1;
	}

	if (Input.IsActionPressed("move_down"))
	{
		velocity.Y += 1;
	}

	if (Input.IsActionPressed("move_up"))
	{
		velocity.Y -= 1;
	}

//animation variables
	var body = GetNode<AnimatedSprite2D>("Body");
	var head = GetNode<AnimatedSprite2D>("Head");

//movement and animation controls
if (velocity.Length() > 0){
		velocity = velocity.Normalized() * Speed;
		body.Play();
		head.Play();
	}
	else{
		body.Stop();
		head.Stop();
}
	
	//actual thing that makes movement work
	Position += velocity * (float)delta;
	Position = new Vector2(
	x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
	y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
	);
	
	//animation swapping
	if (velocity.X != 0){
		body.Animation = bodyType+"_left-right";
		body.FlipV = false;
		body.FlipH = velocity.X < 0;
		
		head.Animation = (species+"_side");
		head.FlipH = velocity.X < 0;
	}
	else if (velocity.Y != 0){
		body.Animation = bodyType+"_up-down";
		
		if(velocity.Y < 0){
			head.Animation = (species+"_back");
		} else{
			head.Animation = (species+"_front");
		}
	}

//end of delta
}


//end of code
}
