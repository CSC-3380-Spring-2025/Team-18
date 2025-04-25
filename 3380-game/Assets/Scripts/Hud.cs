using Godot;
using System;

public partial class Hud : Control
{
	
	private Node currentMenu = null;
	public Node screens;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		screens = GetNode("/root/Main/Screens");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnEquipPressed(){
		SwitchScene("res://Scenes/equipment.tscn");
	}
	
	public void OnInvenPressed(){
		SwitchScene("res://Scenes/inventory.tscn");
	}
	public void OnStatsPressed(){
		SwitchScene("res://Scenes/stats.tscn");
	}
	public void OnAbilPressed(){
		SwitchScene("res://Scenes/abilities.tscn");
	}
	public void OnDialogPressed(){
		SwitchScene("res://Scenes/dialogue.tscn");
	}
	public void OnQuestPressed(){
		SwitchScene("res://Scenes/QuestBook.tscn");
	}
	public void OnMapPressed(){
		SwitchScene("res://Scenes/Map.tscn");
		
	}
	public void OnSettingsPressed(){
		SwitchScene("res://Scenes/settings.tscn");
	}
	
	public void OnResumePressed(){
		Node node = GetNode("/root/Main/Screens").GetChild(-1);
		node.QueueFree();
	}

public void SwitchScene(string scenePath){
		screens.RemoveChild(screens.GetChild(-1));
		
		PackedScene scene = GD.Load<PackedScene>(scenePath);
		if(scene != null){
			currentMenu = scene.Instantiate();
			screens.AddChild(currentMenu);
		} else { 
			GD.PrintErr($"Failed to load scene: {scenePath}");
		}
		
	}
	//end HUD
}
