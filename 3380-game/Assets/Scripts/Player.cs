using Godot;
using System;
using System.Linq;
using Game.Assets.Scripts;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Player : Area2D, Savable, Loadable
{
	[Export]
	public int Speed { get; set; } = 150; // How fast the player will move (pixels/sec).
	
	[Signal]public delegate void HitEventHandler();
	[Signal] public delegate void DamageEventHandler(int dmgTaken);
	
	public Vector2 screenSize;
	public PlayerData playerData = new PlayerData();
	public Attributes stats = new Attributes();
	public string bodyType = "1";

	public void PDatTaker(PlayerData PDat){
		playerData = new PlayerData(PDat.Name, PDat.Class, PDat.Race, PDat.Hair, PDat.Eye, PDat.Pattern,
		PDat.SkinColor, PDat.EyeColor, PDat.HairColor, PDat.FacialMarkings);
	}
	
	
	public void PositionSetTaker(Vector2 newPos){
		Position = new Vector2(x: (newPos.X), y: (newPos.Y) );
	}
	
	public void Freezer(){
		velocity.X = 0; velocity.Y = 0; Speed = 0;
	}
//animation variables
	public AnimatedSprite2D body, head, eye, hair, hairBack, pattern;
	public Vector2 velocity;
	public string direction;
	
public override void _Ready() //called on start
{
	screenSize = GetViewportRect().Size;
	Load();
	direction = "front";
	body = GetNode<AnimatedSprite2D>("Body");
	head = GetNode<AnimatedSprite2D>("Head");
	eye = GetNode<AnimatedSprite2D>("Eyes");
	hair = GetNode<AnimatedSprite2D>("Hair");
	hairBack = GetNode<AnimatedSprite2D>("Hair_Back");
	pattern = GetNode<AnimatedSprite2D>("Head/Pattern");
	
	
	
	
	//end _Ready
}

public override void _Process(double delta) //called in real time
{
	velocity = Vector2.Zero; // The player's movement vector.
	if (!IsGamePaused())
	{
		if (Input.IsActionPressed("move_right")){velocity.X += 1; direction = "side";}
		if (Input.IsActionPressed("move_left")){velocity.X -= 1; direction = "side";}
		if (Input.IsActionPressed("move_down")){velocity.Y += 1; direction = "front";}
		if (Input.IsActionPressed("move_up")){velocity.Y -= 1; direction = "back";}
	}
//input handlers

//movement and animation controls
	AnimationTurn(body, velocity, bodyType, "body");
	body.SelfModulate = (playerData.SkinColor);
	AnimationTurn(head, velocity);
	head.SelfModulate = (playerData.SkinColor);
	AnimationTurn(eye, velocity, playerData.Eye, "eye");
	eye.SelfModulate = (playerData.EyeColor);
	eye.Play();
	
	if(playerData.Race == "Human" && playerData.Hair != "0"){
		if(StringExtensions.ToInt(playerData.Hair) > 10){
			AnimationTurn(hairBack, velocity, playerData.Hair, "hair");
			hairBack.SelfModulate = (playerData.HairColor);
			hairBack.Show();
		} else {
			hairBack.Hide();
		}
		AnimationTurn(hair, velocity, playerData.Hair, "hair");
		hair.SelfModulate = (playerData.HairColor);		
		hair.Show();
	} else {
		hair.Hide();
		hairBack.Hide();
		
		if(playerData.Pattern.ToInt() == 0){
			pattern.Hide();
		} else {
			pattern.Show();
			AnimationTurn(pattern, velocity, playerData.Pattern);
			pattern.SelfModulate = (playerData.FacialMarkings);
		}
		}
	
	if (velocity.Length() > 0){
		velocity = velocity.Normalized() * Speed;
		body.Play();
		head.Play();
		pattern.Play();
	} else {
		body.Stop();
		head.Stop();
		pattern.Stop();
	
	head.Animation = (playerData.Race+"_"+direction);
	body.Animation = ("body_"+bodyType+"_"+direction);
	if(direction == "back"){ eye.Hide();} else{eye.Show(); eye.Animation = ("eye_"+playerData.Eye+"_"+direction);}
	if(playerData.Pattern != "0"){ pattern.Show();
		pattern.Animation = (playerData.Race+"_"+playerData.Pattern+"_"+direction);} else {pattern.Hide(); pattern.Animation = "0";}
	if(StringExtensions.ToInt(playerData.Hair) > 10){
		hairBack.Animation = ("hair_"+playerData.Hair+"_"+direction);}
	if(playerData.Hair != "0" && playerData.Race == "Human" ){
		hair.Show(); hair.Animation = ("hair_"+playerData.Hair+"_"+direction);} else{hair.Hide(); hair.Animation = "0";}
	}
	//actual thing that makes movement work
	Position += velocity * (float)delta;
	Position = new Vector2(
	x: (Position.X),
	y: (Position.Y)
	);
//end of delta
}

/* animation swapping handler.
@params 
node& velocity are self explanatory, just the node you need to switch and the players movement vector
choice deals with which ever enumerated choice of whichever thing.
type is where you pass in body, eye, hair,species, etc. 
*/
public void AnimationTurn(AnimatedSprite2D node, Vector2 velocity, string choice = "", string type = ""){
	if(choice != ""){choice = ("_"+choice);}
	if(type == ""){type = playerData.Race;}
	
	if(choice == "0"){
		node.Hide();
	} else {
		node.Show();
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
	}
}

public void Save(){
	SaveLoad SL = new SaveLoad();
	SL.Save(stats, Position, playerData);
}
public void Load(){
	SaveLoad SL = new SaveLoad();
	SL.Load(stats, Position, playerData);
}

	private bool IsGamePaused() => GetTree().Paused;

//end of code <- keep right before final bracket
}
