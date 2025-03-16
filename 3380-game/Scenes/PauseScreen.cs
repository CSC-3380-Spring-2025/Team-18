using Godot;
using System;

public partial class PauseScreen : CanvasLayer
{
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
		SwitchScene("res://Scenes//inventory.tscn");
	}
	public void StatsPressed(){
		SwitchScene("res://Scenes//stats.tscn");
	}
	public void SettingsPressed(){
		SwitchScene("res://Scenes//settings.tscn");
	}
	private void SwitchScene(string scenePath){
		//Visible = false;
		GetTree().Paused = false;
		GetTree().ChangeSceneToFile(scenePath);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
