using Godot;
using System;

public partial class Story : Control
{
	[Export] public Label Text;
	[Export] public Camera2D camera;
	public CanvasLayer storyScreen;
	
	
	public override void _Ready(){		
	}
	
	public void LoreDump(){
		Text.SetText("");
	}
	
	public void Skip(){
		camera.Enabled = false;
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}
}
