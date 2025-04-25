using Godot;
using System;

public partial class Story : Control
{
	[Export] public Label Text;
	[Export] public Camera2D camera;
	public SaveLoad SL = new SaveLoad();
	public CanvasLayer storyScreen;
	
	
	public override void _Ready(){
		SL.SavePosition(Position,"res://Scenes/Bedroom.tscn");
	}
	
	public void LoreDump(){
		Text.SetText("");
	}
	
	public void Skip(){
		camera.Enabled = false;
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}
}
