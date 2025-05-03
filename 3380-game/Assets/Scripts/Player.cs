using Godot;
using System;
using System.Linq;
using Game.Assets.Scripts;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Player : CharacterBody2D, Savable, Loadable
{
	[Export]public int Speed { get; set; } = 150; // How fast the player will move (pixels/sec).
	[Export] public Camera2D camera;
	[Signal]public delegate void HitEventHandler();
	[Signal] public delegate void LocationChangeEventHandler(String location);
	[Signal] public delegate void DamageEventHandler(int dmgTaken);
	[Signal] public delegate void PositionSendEventHandler(Vector2 position);
	[Signal] public delegate void EnemySendEventHandler(String enemyNode);
	
	public double holder = 0; //nodes to supplement interactions
	public bool check = false;
	public Label objLabel = null;
	public Color labelColor = new Color(0,0,0,1);
	
	
	public Vector2 screenSize;
	public bool Frozen = false;
	public PlayerData playerData = new PlayerData();
	public Attributes stats = new Attributes();
	public SaveLoad SL = new SaveLoad();
	public string bodyType = "1";
	public string Location;
//takes in the variables from character select, so the player changes in real time.
	public void PDatTaker(PlayerData PDat){
		playerData = new PlayerData(PDat.Name, PDat.Class, PDat.Race, PDat.Hair, PDat.Eye, PDat.Pattern,
		PDat.SkinColor, PDat.EyeColor, PDat.HairColor, PDat.FacialMarkings);
	}
	
	public void CameraChange(){
		camera.Enabled = true;
	}
	
	public void EnemyTaker(String enemyPath){
		GD.Print("Enemy Taken: "+enemyPath);
		EmitSignal(SignalName.EnemySend, enemyPath);
	}
	
	public void LocationTaker(string place){
		Location = place;
	}
	public void PositionSetTaker(Vector2 newPos){
		Position = new Vector2( x: (newPos.X), y: (newPos.Y) );
	}
	
	public void Freezer(){
		Frozen = !Frozen;
	}
//animation variables
	public AnimatedSprite2D body, head, eye, hair, hairBack, pattern;
	public Vector2 Velocity;
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
	public void GetInput()
	{
		if (!IsGamePaused() && !Frozen)
		{Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		Velocity = inputDir * Speed;
		}
	}
	
	public override void _PhysicsProcess(double delta)
	{
		holder += delta;
		float fDelta = (float)delta;
		
		GetInput();
		var collision = MoveAndCollide(Velocity * (float)delta);
		if (collision != null){ 
			/* What does this do? this lets the player interact with shit and have text pop up. 
			*/
			if( ((Node)collision.GetCollider()).IsInGroup("Interactable")){
				//GD.Print(((Node)collision.GetCollider()).Name);
				if(Input.IsActionPressed("interact"))
				{
					var collided = ((Node)collision.GetCollider());
					objLabel = (Label)collided.FindChild("Label");
					labelColor = objLabel.GetSelfModulate();
					objLabel.Visible = true;
					labelColor.A = 1;
					objLabel.Modulate = (labelColor);
					check = true;
					holder = 0;
				}
			}
			
			if( ((Node)collision.GetCollider()).IsInGroup("Enemy")){
				//GD.Print(((Node)collision.GetCollider()).Name);
				EmitSignal(SignalName.EnemySend, ((Node)collision.GetCollider()).Name);
				
			}
			//GetTree().GetNodesInGroup("Interactable");
		}
		
		if(check == true && holder >= 2){
			if(labelColor.A > 0){
				labelColor.A -= fDelta;
				objLabel.SelfModulate = (labelColor);
			} else {check = false; objLabel.Visible = false;}
		}
		
	//end phys process	
	}

public override void _Process(double delta) //called in real time
{
//direction handlers
		if (Input.IsActionPressed("move_right")){direction = "side";}
		if (Input.IsActionPressed("move_left")){direction = "side";}
		if (Input.IsActionPressed("move_down")){direction = "front";}
		if (Input.IsActionPressed("move_up")){direction = "back";}
	
//movement and animation controls
	AnimationTurn(body, Velocity, bodyType, "body");
	body.SelfModulate = (playerData.SkinColor);
	AnimationTurn(head, Velocity);
	head.SelfModulate = (playerData.SkinColor);
	AnimationTurn(eye, Velocity, playerData.Eye, "eye");
	eye.SelfModulate = (playerData.EyeColor);
	eye.Play();
	
	if(playerData.Race == "Human" && playerData.Hair != "0"){
		if(StringExtensions.ToInt(playerData.Hair) > 10){
			AnimationTurn(hairBack, Velocity, playerData.Hair, "hair");
			hairBack.SelfModulate = (playerData.HairColor);
			hairBack.Show();
		} else {
			hairBack.Hide();
		}
		AnimationTurn(hair, Velocity, playerData.Hair, "hair");
		hair.SelfModulate = (playerData.HairColor);		
		hair.Show();
		
		if(playerData.Pattern.ToInt() == 0){
			pattern.Hide();
		} else {
			pattern.Show();
			AnimationTurn(pattern, Velocity, playerData.Pattern);
			pattern.SelfModulate = (playerData.FacialMarkings);
		}
	} else {
		hair.Hide();
		hairBack.Hide();
		pattern.SelfModulate = (playerData.FacialMarkings);
		
		}
	
	if (Velocity.Length() > 0){
		MoveAndSlide();
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
	Position += Velocity * (float)delta;
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
	GD.Print(this.GetPosition());
	EmitSignal(SignalName.PositionSend, this.GetPosition());
	SL.SavePosition(this.GetPosition());
	SL.SaveSprite(playerData);
	SL.SaveStats(stats);
}
public void Load(){
	Vector2 posHolder = new Vector2();
	
	SL.LoadPosition(posHolder);
	SL.LoadSprite(playerData);
	SL.LoadStats(stats);
	SL.LoadScene(Location);
	
	Position = posHolder;
}

	private bool IsGamePaused() => GetTree().Paused;

//end of code <- keep right before final bracket
}
