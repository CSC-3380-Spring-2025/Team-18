using Godot;
using System;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class MainMenu : CanvasLayer
{
	private bool _isFirstLoad = true;
	private PauseScreen pauseScreen;
	// Called when the node enters the scene tree for the first time.
	///Makes it so the play and exit button both function as intended when esc 	is pressed or game is 'paused'.
	public override void _Ready(){
		ProcessMode = ProcessModeEnum.Always;
	}
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	///set the visibility of the main menu screen to true, as well as call the GetTree.Paused to pause the game when 'esc' is pressed.
	public override void _Process(double delta){

		//if(Input.IsActionJustPressed("ui_cancel") && !Visible){
//
			//GetTree().Paused = !GetTree().Paused;
			//TogglePauseScreen();
		//}
	}
	
	//private void TogglePauseScreen(){
		//if(pauseScreen == null){
			//PackedScene pauseScene = GD.Load<PackedScene>("res://Scenes//pauseScreen.tscn");
		//if(pauseScene != null){
			//pauseScreen = (PauseScreen)pauseScene.Instantiate();
			//GetParent().AddChild(pauseScreen);
			//pauseScreen.Visible = false;
		//} 
		//else{ 
			//GD.PrintErr("Failed to load PauseScreen");
			//return;
			//}
		//}
		//
		//if(GetTree().Paused){pauseScreen.Visible = true;}
		//if(GetTree().Paused == false){pauseScreen.Visible = false;}
	//}
	
	///Creates the functionality of the Play button when pressed
	public void OnPlayPressed(){
		// Do not allow playing if the character creator save file has not been created
		using var file = FileAccess.Open("user://playerSprite0.dat", FileAccess.ModeFlags.Read);
		if (file is null) return;
		
		Visible = false;
		GetTree().Paused = false;
		
		var mainChildren = GetParent().GetChildren();
		foreach(var child in mainChildren){
			//If statement is so whenever the start button is pressed, it doesn't continously create a new node of Player scene
			if(child.Name == "Player")
				return;
		}
		
		//Once the button is clicked, it moves the current scene (main menu) to the Player scene
		
		sceneSwap("main");
	}
	
	///Functionality for the exit button
	public void OnExitPressed(){
		// Save any savables on exit
		foreach (var child in GetParent().GetChildren())
		{
			if (child is Savable savable)
			{
				savable.Save();
			}
		}
		GetTree().Quit();
	}
	
	//functionality for new game button
	public void OnNewGamePressed(){
		GetTree().ChangeSceneToFile("res://Scenes/character_creation.tscn");
	}
	
	public void sceneSwap(string scene){
		var gameScene = GD.Load<PackedScene>("res://Scenes//"+scene+".tscn");
		var gameSceneNode = gameScene.Instantiate();
		GetParent().AddChild(gameSceneNode);
		
		
		
		if (_isFirstLoad)
		{
			foreach (var child in GetParent().GetChildren())
			{
				if (child is Loadable loadable)
				{
					loadable.Load();
				}
			}
			_isFirstLoad = false;
		}
	}
}
