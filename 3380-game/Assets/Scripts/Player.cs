using Godot;
using System;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 200; // How fast the player will move (pixels/sec).
	//[Signal]
	//public delegate void HitEventHandler();
	
	public Vector2 ScreenSize;
	
	public string species = "Twilek";
	public string bodyType = "1";
	public string pChoice = "1", eChoice = "3", hChoice = "12";
	
public override void _Ready() //called on start
{
	ScreenSize = GetViewportRect().Size;
	 Load();
}

public override void _Process(double delta) //called in real time
{
	var velocity = Vector2.Zero; // The player's movement vector.
//input handlers
	if (Input.IsActionPressed("move_right")){velocity.X += 1;}
	if (Input.IsActionPressed("move_left")){velocity.X -= 1;}
	if (Input.IsActionPressed("move_down")){velocity.Y += 1;}
	if (Input.IsActionPressed("move_up")){velocity.Y -= 1;}

//animation variables
	var body = GetNode<AnimatedSprite2D>("Body");
	var head = GetNode<AnimatedSprite2D>("Head");
	var eye = GetNode<AnimatedSprite2D>("Eyes");
	var hair = GetNode<AnimatedSprite2D>("Hair");
	var hairBack = GetNode<AnimatedSprite2D>("Hair_Back");
	var pattern = GetNode<AnimatedSprite2D>("Head/Pattern");
	
//movement and animation controls
if (velocity.Length() > 0){
	velocity = velocity.Normalized() * Speed;
		
	AnimationTurn(body, velocity, bodyType, "body");
	body.Play();
	AnimationTurn(head, velocity);
	head.Play();
	AnimationTurn(eye, velocity, eChoice, "eye");
	eye.Play();
	
	//check to make sure only the ones with patterns have the node active
	if(species == "Human" || species == "Pureblood"){
		pattern.Hide();
		if(StringExtensions.ToInt(hChoice) > 10){
			AnimationTurn(hairBack, velocity, hChoice, "hair");
			AnimationTurn(hair, velocity, hChoice, "hair");
			hairBack.Show();
		} else {
			AnimationTurn(hair, velocity, hChoice, "hair");
			hairBack.Hide();
			}
		hair.Show();
	}else {
		pattern.Show();
		hair.Hide();
		hairBack.Hide();
		AnimationTurn(pattern, velocity, pChoice);
		pattern.Play();
		}
} else {
	body.Stop();
	head.Stop();
	pattern.Stop();
}
	
	//actual thing that makes movement work
	Position += velocity * (float)delta;
	Position = new Vector2(
	x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
	y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
	);
//end of delta
}

/* animation swapping handler.
@params 
node& velocity are self explanatory, just the node you need to switch and the players movement vector
choice deals with which ever enumerated choice of whichever thing. so like, for eye one, choice is
"eChoice" which is currently set to 1. 
type is where you pass in body, eye, hair,species, etc. 
*/
public void AnimationTurn(AnimatedSprite2D node, Vector2 velocity, string choice = "", string type = ""){
	if(choice != ""){
		choice = ("_"+choice);
	}
	if(type == ""){
		type = species;
	}
		
	if(velocity.X != 0){
		node.Animation = (type+choice+"_side");
		node.FlipH = velocity.X < 0;
		
		if(type == "eye"){ node.Show(); }
	} else if(velocity.Y < 0){
		if(type == "eye"){
			node.Hide();
		} else {
			node.Animation = (type+choice+"_back");
		}
	} else{
		node.Animation = (type+choice+"_front");
		if(type == "eye"){ node.Show(); }
	}
	//end AnimationTurn()
}

public void Load()
	{
		using var file = FileAccess.Open("user://player_save.dat", FileAccess.ModeFlags.Read);
		if (file is not null)
		{
			Position = (Vector2)file.GetVar();
		}
	}

	public void Save()
	{
		using var file = FileAccess.Open("user://player_save.dat", FileAccess.ModeFlags.Write);
		file.StoreVar(Position);
	}

//end of code <- keep right before final bracket
}
