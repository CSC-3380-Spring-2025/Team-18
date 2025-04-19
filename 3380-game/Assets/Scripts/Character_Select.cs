using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Character_Select : Control
{
	[Export] public OptionButton Species, Hair, Eye, Pattern, Class;
	[Export] public Label ClassDescription, Str, Dex, Con, Int, Wis, Cha;
	[Signal] public delegate void PDatEventHandler(PlayerData PlayerData);
	[Signal] public delegate void PStatsEventHandler(Attributes PlayerStats);
	[Signal] public delegate void PositionSetEventHandler(Vector2 position);
	[Signal] public delegate void FreezeEventHandler();

	
	public PlayerData playerData;
	public Color blank = new Color("White");
	//PlayerData(string name, string characterClass, string race, string hair, string eye, string pattern, 
	//	Color skinColor, string eyeColor, string hairColor, string facialMarkings)
	
	public string species = "Human", hair = "1", eye = "1", pattern = "0", name = "No Input", playerClass = "No Input";
	
	//class, lvl, xp, max hp,current hp, ac, str, dex, con, int, wis, cha
	
	public Attributes stats = new Attributes();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		playerData = new PlayerData(name, playerClass, species, hair, eye, pattern, blank, blank, blank, blank);
	 	stats = new Attributes();
		EmitSignal(SignalName.PDat, playerData);
		EmitSignal(SignalName.PStats, stats);
		EmitSignal(SignalName.PositionSet, new Vector2(x: (800),y: (220)));
		EmitSignal(SignalName.Freeze);

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
	public void NameSet(string name){
		//name = "Placeholder";
		playerData.Name = name;
		EmitSignal(SignalName.PDat, playerData);
	}
	
	public void ClassSelect(int idx){
		if(idx != 0){
			playerClass = Class.GetItemText(idx);
			playerData.Class = playerClass;
			stats.CharacterClass = playerClass;
			EmitSignal(SignalName.PDat, playerData);
		}
		
		//changes text to a description of each class.
		switch(playerClass){
			case "Guardian":
				ClassDescription.SetText("Guardian: \n A blend of \"magic\" and melee combat. \n"+
				"Tankiest class. Half-caster, Paladin/Cleric equivalent in DND terms.\n"+
				"Strength and Charisma++. At first level, 10 Health, 5 powers, 2 force points.\n"+
				"+2 FP, +2 powers, +10 HP per level.");
				break;
			case "Sentinel":
				ClassDescription.SetText("Sentinel: \n Stealth supremacist, striking hard and fast before bolting out. \n"+
				". Less Magic than Consular, more than Guardian. Mash up of a bard and a rogue.\n"+
				"Dexterity and Charisma++. At first level, 8 Health, 7 powers, 3 force points.\n"+
				"+3 FP, +2 powers, +8 HP per level.");
				break;
			case "Ascetic":
				ClassDescription.SetText("Ascetic: \n Hand-to-hand combat specialist. \n"+
				"Most versatile class. Least magic, but recovers quicker. Quarter-caster. Monk in DND terms, with a twist.\n"+
				"Dexterity and Strength++. At first level, 8 Health, 3 powers, 1 force point.\n"+
				"+1 FP, +2 powers, +8 HP per level.");
				break;
			case "Consular":
				ClassDescription.SetText("Consular: \n Ranged DPS/Support \n"+
				"Squishiest class, but most Magic. Full-caster. Blend of Sorceror and Wizard.\n"+
				"Strength and Charisma++. At first level, 6 Health, 9 powers, 4 force points.\n"+
				"+4 FP, +2 powers, +6 HP per level.");
				break;
			}
		
	}
	
	public void Randomizer(){
		RandomizeStats(7, 40);
		Str.SetText(stats.Strength + "("+((stats.Strength - 10)/2)+")");
		Dex.SetText(stats.Dexterity+ "("+((stats.Dexterity - 10)/2)+")");
		Int.SetText(stats.Intelligence+ "("+((stats.Intelligence - 10)/2)+")");
		Con.SetText(stats.Constitution+ "("+((stats.Constitution - 10)/2)+")");
		Wis.SetText(stats.Wisdom+ "("+((stats.Wisdom - 10)/2)+")");
		Cha.SetText(stats.Charisma+ "("+((stats.Charisma - 10)/2)+")");
	}
	
	public void Continue(){
		EmitSignal(SignalName.PDat, playerData);
		EmitSignal(SignalName.PStats, stats);
		Save();
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}

	public void Save(){
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
		
		file.StoreVar(holder, true);
		SaveStats();
	}

	public void SaveStats(){
		using var file = FileAccess.Open("user://playerStats.dat", FileAccess.ModeFlags.Write);
			var statHolder = new Godot.Collections.Array();
			statHolder.Add(stats.CharacterClass);
			statHolder.Add(stats.Level);
			statHolder.Add(stats.XP);
			statHolder.Add(stats.MaxHealth);
			statHolder.Add(stats.CurrentHealth);
			statHolder.Add(stats.Armor);
			statHolder.Add(stats.Strength);
			statHolder.Add(stats.Dexterity);
			statHolder.Add(stats.Constitution);
			statHolder.Add(stats.Intelligence);
			statHolder.Add(stats.Wisdom);
			statHolder.Add(stats.Charisma);
		file.StoreVar(statHolder, true);
	}
	
			/// <summary>
	/// Randomizes the stats for a character given a minimum amount of points per stat and a number of points to distribute
	/// </summary>
	/// <param name="minimumPerStat">The minimum number of points each stat should start with</param>
	/// <param name="points">The total number of points that should be randomly distributed</param>
public void RandomizeStats(int minimumPerStat, int points)
	{
		var statDictionary = new Dictionary<string, int>
		{
			{"strength", minimumPerStat},
			{"dexterity", minimumPerStat},
			{"constitution", minimumPerStat},
			{"intelligence", minimumPerStat},
			{"wisdom", minimumPerStat},
			{"charisma", minimumPerStat},
		};
		var random = new Random();
		while (points > 0)
		{
			var statKey = statDictionary.Keys.ElementAt(random.Next(0, statDictionary.Count));
			if (statDictionary[statKey] < 21)
			{
				statDictionary[statKey]++;
				points--;
			}
		}
		stats.Strength = statDictionary["strength"];
		stats.Dexterity = statDictionary["dexterity"];
		stats.Constitution = statDictionary["constitution"];
		stats.Intelligence = statDictionary["intelligence"];
		stats.Wisdom = statDictionary["wisdom"];
		stats.Charisma = statDictionary["charisma"];
	}
	
//end Character_select
}
