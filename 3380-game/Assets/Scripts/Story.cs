using Godot;
using System;

public partial class Story : Control
{
	[Export] public Label Text, Lore, Warning, Explained;
	[Export] public Button LoreButton, SkipButton, ExplainButton;
	[Export] public Camera2D camera;
	public SaveLoad SL = new SaveLoad();
	public CanvasLayer storyScreen;
	
	
	public override void _Ready(){
		SL.SavePosition(new Vector2(x: 130, y: 312));
		SL.SaveScene("res://Scenes/Areas/Ossus/Bedroom.tscn");
	}
	
	public void LoreDump(){
		Text.Visible = false;
		Warning.Visible = false;
		LoreButton.Visible = false;
		Lore.Visible = true;
		ExplainButton.Visible = true;
	}
	
	public void ExplainPls(){
		Lore.Visible = false;
		Explained.Visible = true;
	}
		
	public void Skip(){
		camera.Enabled = false;
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}
}
