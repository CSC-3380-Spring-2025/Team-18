using Godot;
using System;

public partial class Npc : Node2D
{
	[Export] public String name;
	[Export] public String Body, Head, Pattern, Eyes, Hair;
	[Export] public Color HairColor, BodyColor, MarkColor, EyeColor;
	[Export] public String direction;
	[Export] public bool Dialogue;
	[Export] public bool PassiveChatter;
	[Export] public String[] ChatterBox;
	[Export] public NodePath[] Routine;
	
	public AnimatedSprite2D body, head, eye, hair, hairBack, pattern;
	public Vector2 Velocity;
	
	
	public override void _Ready() //called on start
	{	
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
	AnimationTurn(body, Velocity, "1", "body");
	body.SelfModulate = (this.BodyColor);
	AnimationTurn(head, Velocity);
	head.SelfModulate = (this.BodyColor);
	AnimationTurn(eye, Velocity, this.Eyes, "eye");
	eye.SelfModulate = (this.EyeColor);
	eye.Play();
	
	if(this.Head == "Human" && this.Hair != "0"){
		if(StringExtensions.ToInt(this.Hair) > 10){
			AnimationTurn(hairBack, Velocity, this.Hair, "hair");
			hairBack.SelfModulate = (this.HairColor);
			hairBack.Show();
		} else {
			hairBack.Hide();
		}
		AnimationTurn(hair, Velocity, this.Hair, "hair");
		hair.SelfModulate = (this.HairColor);
		hair.Show();
		
		if(this.Pattern.ToInt() == 0){
			pattern.Hide();
		} else {
			pattern.Show();
			AnimationTurn(pattern, Velocity, this.Pattern);
			pattern.SelfModulate = (this.MarkColor);
		}
		
	} else {
		hair.Hide();
		hairBack.Hide();
		pattern.SelfModulate = (this.MarkColor);
		}
	
	if (Velocity.Length() > 0){
		body.Play();
		head.Play();
		pattern.Play();
	} else {
		body.Stop();
		head.Stop();
		pattern.Stop();
	
	head.Animation = (this.Head+"_"+direction);
	body.Animation = ("body_"+1+"_"+direction);
	if(direction == "back"){ eye.Hide();} else{eye.Show(); eye.Animation = ("eye_"+this.Eyes+"_"+direction);}
	if(this.Pattern != "0"){ pattern.Show();
		pattern.Animation = (this.Head+"_"+this.Pattern+"_"+direction);} else {pattern.Hide(); pattern.Animation = "0";}
	if(StringExtensions.ToInt(this.Hair) > 10){
		hairBack.Animation = ("hair_"+this.Hair+"_"+direction);}
	if(this.Hair != "0" && this.Head == "Human" ){
		hair.Show(); hair.Animation = ("hair_"+this.Hair+"_"+direction);} else{hair.Hide(); hair.Animation = "0";}
	}
}	
	//stolen from Player
public void AnimationTurn(AnimatedSprite2D node, Vector2 velocity, string choice = "", string type = ""){
	if(choice != ""){choice = ("_"+choice);}
	if(type == ""){type = Head;}
	
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

	
}
