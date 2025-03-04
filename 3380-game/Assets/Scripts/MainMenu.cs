using Godot;
using System;

public partial class MainMenu : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	///Makes it so the play and exit button both function as intended when esc is pressed or game is 'paused'.
	public override void _Ready(){
		ProcessMode = ProcessModeEnum.Always;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	///set the visibility of the main menu screen to true, as well as call the GetTree.Paused to pause the game when 'esc' is pressed.
	public override void _Process(double delta){
		if(Input.IsActionJustPressed("ui_cancel")){
			Visible = true; 
			GetTree().Paused = true;
		}
	}
	///Creates the functionality of the Play button when pressed
	public void OnPlayPressed(){
		Visible = false;
		GetTree().Paused = false;
		
		var mainChildren = GetParent().GetChildren();
		
		foreach(var child in mainChildren){
			//If statement is so whenever the start button is pressed, it doesn't continously create a new node of Player scene
			if(child.Name == "Player")
				return;
		}
		
		//Once the button is clicked, it moves the current scene (main menu) to the Player scene
		var gameScene = GD.Load<PackedScene>("res://Scenes//Player.tscn");
		var gameSceneNode = gameScene.Instantiate();
		GetParent().AddChild(gameSceneNode);
		
	}
	///Functionality for the exit button
	public void OnExitPressed(){
		GetTree().Quit();
	}
}
