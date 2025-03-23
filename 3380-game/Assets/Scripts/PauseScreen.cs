using Godot;
using System;

public partial class PauseScreen : CanvasLayer
{
	private Node currentMenu = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		ProcessMode = ProcessModeEnum.Always;
		
	}
	public void ResumePressed(){
		if(currentMenu != null){
			currentMenu.QueueFree();
			currentMenu = null;
		}
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
		if(currentMenu != null){
			currentMenu.QueueFree();
			currentMenu = null;
			return;
		}
		PackedScene scene = GD.Load<PackedScene>(scenePath);
		if(scene != null){
			currentMenu = scene.Instantiate();
			GetParent().AddChild(currentMenu);
		} else { 
			GD.PrintErr($"Failed to load scene: {scenePath}");
		}
		//GetTree().Paused = false;
		//GetTree().ChangeSceneToFile(scenePath);
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
