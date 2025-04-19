using Godot;
using System;
using System.Linq;
using Game.Assets.Scripts;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Player : Area2D, Loadable, Savable
{
	[Export]
	public int Speed { get; set; } = 200; // How fast the player will move (pixels/sec).
	
	[Signal]
	public delegate void HitEventHandler();
	
	public Vector2 screenSize;
	public PlayerData playerData;
	public string bodyType = "1";

	public void PDatTaker(PlayerData PDat){
		playerData = new PlayerData(PDat.Name, PDat.Class, PDat.Race, PDat.Hair, PDat.Eye, PDat.Pattern, PDat.SkinColor,
		PDat.EyeColor, PDat.HairColor, PDat.FacialMarkings);
	}
	
//animation variables
	public AnimatedSprite2D body, head, eye, hair, hairBack, pattern;
	public Vector2 velocity;
	public string direction;
	
public override void _Ready() //called on start
{
	screenSize = GetViewportRect().Size;
	LoadSprite();
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
	
	if(playerData.Race == "Human" || playerData.Race == "Pureblood"){
		pattern.Hide();
		if(StringExtensions.ToInt(playerData.Hair) > 10){
			AnimationTurn(hairBack, velocity, playerData.Hair, "hair");
			AnimationTurn(hair, velocity, playerData.Hair, "hair");
			hair.SelfModulate = (playerData.HairColor);
			hairBack.SelfModulate = (playerData.HairColor);
			hairBack.Show();
		} else {
			AnimationTurn(hair, velocity, playerData.Hair, "hair");
			hair.SelfModulate = (playerData.HairColor);
			hairBack.Hide();
		}
		hair.Show();
	} else {
		pattern.Show();
		hair.Hide();
		hairBack.Hide();
		AnimationTurn(pattern, velocity, playerData.Pattern);
		pattern.SelfModulate = (playerData.FacialMarkings);

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
	if(playerData.Race == "Togruta" || playerData.Race == "Twilek"){
		pattern.Animation = (playerData.Race+"_"+playerData.Pattern+"_"+direction);}
	if(StringExtensions.ToInt(playerData.Hair) > 10){hairBack.Animation = ("hair_"+playerData.Hair+"_"+direction);}
	hair.Animation = ("hair_"+playerData.Hair+"_"+direction);
}
	//actual thing that makes movement work
	Position += velocity * (float)delta;
	Position = new Vector2(
	x: Mathf.Clamp(Position.X, 0, screenSize.X),
	y: Mathf.Clamp(Position.Y, 0, screenSize.Y)
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
	if(choice != ""){
		choice = ("_"+choice);
	}
	if(type == ""){
		type = playerData.Race;
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

//public void ColorChanger(string Case, ){
	//
//}


public void Load()
	{   //for some reason once i run the bitch enough times it breaks. for no reason. if that happens to you,
		//just change the file name to something that hasnt been used yet and it should be fine.
		using var file = FileAccess.Open("user://playerData_v3.dat", FileAccess.ModeFlags.Read);
		if (file is not null)
		{	
			Position = (Vector2)file.GetVar();
			playerData.Experience.AddPoints(file.Get32());
		}
	}

//i KNOW this is stupid shut up ive been trying to interface with the existing load for a WEEK
public void LoadSprite(){
	using var file = FileAccess.Open("user://playerSprite0.dat", FileAccess.ModeFlags.Read);
	if (file is not null)
		{	
			playerData = new PlayerData();
		
			var holder = file.GetVar(true).As<Godot.Collections.Array>();
			
		playerData.Name = holder[0].AsString();
		playerData.Class= holder[1].AsString();
		playerData.Race= holder[2].AsString();
		playerData.Hair= holder[3].AsString();
		playerData.Eye= holder[4].AsString();
		playerData.Pattern= holder[5].AsString();
		playerData.SkinColor= holder[6].As<Color>();
		playerData.EyeColor= holder[7].As<Color>();
		playerData.HairColor= holder[8].As<Color>();
		playerData.FacialMarkings= holder[9].As<Color>();
		}
}

	public void Save()
	{
		using var file = FileAccess.Open("user://playerData_v3.dat", FileAccess.ModeFlags.Write);
		file.StoreVar(Position);
		file.Store32(playerData.Experience.Points);
	}
	
	private bool IsGamePaused() => GetTree().GetRoot().GetChildren().Any(child => child is PauseScreen { Visible: false });
//end of code <- keep right before final bracket
}
