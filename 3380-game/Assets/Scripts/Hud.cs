using Godot;
using System;

public partial class Hud : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnEquipPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/equipment.tscn");
	}
	
	public void OnInvenPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/inventory.tscn");
	}
	public void OnStatsPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/stats.tscn");
	}
	public void OnAbilPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/abilities.tscn");
	}
	public void OnDialogPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/dialogue.tscn");
	}
	public void OnQuestPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/QuestBook.tscn");
	}
	public void OnMapPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/Map.tscn");
	}
	public void OnSettingsPressed(){
		GetTree().ChangeSceneToFile("res://Scenes/settings.tscn");
	}
}
