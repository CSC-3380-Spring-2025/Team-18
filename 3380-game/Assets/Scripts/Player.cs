using Godot;
using System;
using System.Linq;
using Game.Assets.Scripts;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Player : Area2D, Loadable, Savable
{
	[Export]
	public int Speed { get; set; } = 150; // How fast the player will move (pixels/sec).
	
	[Signal]
	public delegate void HitEventHandler();
	
	public Vector2 screenSize;
	public PlayerData playerData = new PlayerData();
	public Attributes stats = new Attributes();
	public string bodyType = "1";

	public void PDatTaker(PlayerData PDat){
		playerData = new PlayerData(PDat.Name, PDat.Class, PDat.Race, PDat.Hair, PDat.Eye, PDat.Pattern,
		PDat.SkinColor, PDat.EyeColor, PDat.HairColor, PDat.FacialMarkings);
	}
	
	public void StatTaker(Attributes PStats){
		stats = new Attributes(PStats.CharacterClass, PStats.Level, PStats.XP, PStats.MaxHealth, 
		PStats.CurrentHealth, PStats.Strength, PStats.Dexterity, PStats.Constitution, PStats.Intelligence,
		 PStats.Wisdom, PStats.Charisma);
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
		AnimationTurn(pattern, velocity, playerData.Pattern);
		pattern.SelfModulate = (playerData.FacialMarkings);

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
	if(playerData.Pattern.ToInt() != 0){ pattern.Show();
		pattern.Animation = (playerData.Race+"_"+playerData.Pattern+"_"+direction);} else {pattern.Animation = "0";}
	if(StringExtensions.ToInt(playerData.Hair) > 10){
		hairBack.Animation = ("hair_"+playerData.Hair+"_"+direction);}
	if(playerData.Hair != "0" && playerData.Race == "Human" ){
		hair.Show(); hair.Animation = ("hair_"+playerData.Hair+"_"+direction);} else{hair.Animation = "0";}
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
public void Load()
	{   //for some reason once i run the bitch enough times it breaks. for no reason. if that happens to you,
		//just change the file name to something that hasnt been used yet and it should be fine.
		using var file = FileAccess.Open("user://playerData_v3.dat", FileAccess.ModeFlags.Read);
		if (file is not null)
		{	
			Position = (Vector2)file.GetVar();
		}
		LoadSprite();
		LoadStats();
	}

//i KNOW this is stupid shut up ive been trying to interface with the existing load for a WEEK
public void LoadStats(){
	using var file = FileAccess.Open("user://playerStats.dat", FileAccess.ModeFlags.Read);
	if (file is not null){
			var statsHolder = file.GetVar(true).As<Godot.Collections.Array>();
			stats.CharacterClass = statsHolder[0].AsString();
			stats.Level = statsHolder[1].As<int>();
			stats.XP= statsHolder[2].As<int>();
			stats.MaxHealth = statsHolder[3].As<int>();
			stats.CurrentHealth = statsHolder[4].As<int>();
			stats.Armor = statsHolder[5].As<int>();
			stats.Strength = statsHolder[6].As<int>();
			stats.Dexterity = statsHolder[7].As<int>();
			stats.Constitution = statsHolder[8].As<int>();
			stats.Intelligence = statsHolder[9].As<int>();
			stats.Wisdom= statsHolder[10].As<int>();
			stats.Charisma= statsHolder[11].As<int>();
	}
}

public void LoadSprite(){
	using var file = FileAccess.Open("user://playerSprite0.dat", FileAccess.ModeFlags.Read);
	if (file is not null)
	{	
		var holder = file.GetVar(true).As<Godot.Collections.Array>();
		playerData = new PlayerData();
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
		SaveStats();
	}
	
	public void SaveStats(){
			using var file = FileAccess.Open("user://playerStats.dat", FileAccess.ModeFlags.Write);
			var statHolder = new Godot.Collections.Array();
			statHolder.Add(stats.CharacterClass);
			statHolder.Add(stats.Level);
			statHolder.Add(stats.XP);
			statHolder.Add(stats.MaxHealth);
			statHolder.Add(stats.CurrentHealth);
			statHolder.Add(stats.Armor);
			statHolder.Add(stats.Strength);
			statHolder.Add(stats.Dexterity);
			statHolder.Add(stats.Constitution);
			statHolder.Add(stats.Intelligence);
			statHolder.Add(stats.Wisdom);
			statHolder.Add(stats.Charisma);
			
			file.StoreVar(statHolder);
	}

	private bool IsGamePaused() => GetTree().Paused;
//end of code <- keep right before final bracket
}
