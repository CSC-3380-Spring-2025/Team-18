using Godot;
using System;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Character_Select : Control
{
	[Export] public OptionButton Species, Hair, Eye, Pattern;
	[Signal] public delegate void PDatEventHandler(PlayerData PlayerData);
	
	
	public PlayerData playerData;
	public Color blank = new Color("White");
	//PlayerData(string name, string characterClass, string race, string hair, string eye, string pattern, 
	//	Color skinColor, string eyeColor, string hairColor, string facialMarkings)
	
	public string species = "Human", hair = "1", eye = "1", pattern = "1", name = "Null", Class = "Null";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerData = new PlayerData(name, Class, species, hair, eye, pattern, blank, blank, blank, blank);
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SpeciesSelect(int idx){
		species = Species.GetItemText(idx);
		playerData.Race = species;
		EmitSignal(SignalName.PDat, playerData);
	}
	
	public void SkinColor(Color color){
		playerData.SkinColor = color;
		EmitSignal(SignalName.PDat, playerData);
	}
	
	public void HairSelect(int idx){
		hair = Hair.GetItemText(idx);
		playerData.Hair = hair;
		EmitSignal(SignalName.PDat, playerData);	
	}
	public void HairColor(Color color){
		playerData.HairColor = color;
		//GD.Print(playerData.HairColor);
		EmitSignal(SignalName.PDat, playerData);
	}
	
	public void EyeSelect(int idx){
		eye = Eye.GetItemText(idx);
		playerData.Eye = eye;
		EmitSignal(SignalName.PDat, playerData);
	}
	
	public void EyeColor(Color color){
		playerData.EyeColor = color;
		EmitSignal(SignalName.PDat, playerData);
	}
	
	public void PatternSelect(int idx){
		pattern = Pattern.GetItemText(idx);
		playerData.Pattern = pattern;
		EmitSignal(SignalName.PDat, playerData);
	}
	public void PatternColor(Color color){
		playerData.FacialMarkings = color;
		EmitSignal(SignalName.PDat, playerData);
	}
	public void NameSet(){
		name = "Placeholder";
		playerData.Name = name;
		EmitSignal(SignalName.PDat, playerData);
	}
	public void Continue(){
		EmitSignal(SignalName.PDat, playerData);
		Save();
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}

public void Save()
	{
		using var file = FileAccess.Open("user://playerSprite0.dat", FileAccess.ModeFlags.Write);
		
		var holder = new Godot.Collections.Array();
		holder.Add(playerData.Name);
		holder.Add(playerData.Class);
		holder.Add(playerData.Race);
		holder.Add(playerData.Hair);
		holder.Add(playerData.Eye);
		holder.Add(playerData.Pattern);
		holder.Add(playerData.SkinColor);
		holder.Add(playerData.EyeColor);
		holder.Add(playerData.HairColor);
		holder.Add(playerData.FacialMarkings);
		

		//GD.Print(holder[0]);
		file.StoreVar(holder, true);
	}
}
