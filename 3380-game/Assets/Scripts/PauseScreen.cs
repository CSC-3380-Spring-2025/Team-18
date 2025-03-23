using Godot;
using System;

public partial class PauseScreen : CanvasLayer
{
	[Export] private Button resume, quit, inventory, stats, settings;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	}
	public void ResumePressed(){
			Visible = false;
			GetTree().Paused = false;
			}
	public void QuitPressed(){
		GetTree().Quit();
	}
	public void InventoryPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/inventory.tscn");
	}
	public void StatsPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/stats.tscn");
	}
	public void SettingsPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/settings.tscn");
	}
	
	//private void SwitchScene(string scenePath){
		////Visible = false;
		//GetTree().Paused = false;
		//GetTree().ChangeSceneToFile(scenePath);
	//}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		
	}
}
