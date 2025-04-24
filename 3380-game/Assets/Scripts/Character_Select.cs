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

	public void NameTextChanged(String newText)
	{
		name = newText;
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
				StatSetter(10,2,5);
				break;
			case "Sentinel":
				ClassDescription.SetText("Sentinel: \n Stealth supremacist, striking hard and fast before bolting out. \n"+
				". Less Magic than Consular, more than Guardian. Mash up of a bard and a rogue.\n"+
				"Dexterity and Charisma++. At first level, 8 Health, 7 powers, 3 force points.\n"+
				"+3 FP, +2 powers, +8 HP per level.");
				StatSetter(8,3,7);
				break;
			case "Ascetic":
				ClassDescription.SetText("Ascetic: \n Hand-to-hand combat specialist. \n"+
				"Most versatile class. Least magic, but recovers quicker. Quarter-caster. Monk in DND terms, with a twist.\n"+
				"Dexterity and Strength++. At first level, 8 Health, 3 powers, 1 force point.\n"+
				"+1 FP, +2 powers, +8 HP per level.");
				StatSetter(8,1,3);
				break;
			case "Consular":
				ClassDescription.SetText("Consular: \n Ranged DPS/Support \n"+
				"Squishiest class, but most Magic. Full-caster. Blend of Sorceror and Wizard.\n"+
				"Wisdom and Charisma++. At first level, 6 Health, 9 powers, 4 force points.\n"+
				"+4 FP, +2 powers, +6 HP per level.");
				StatSetter(6,4,9);
				break;
			}
		
	}
	
	public void Randomizer(){
		RandomizeStats(7, 35);
		Str.SetText(stats.Strength + "("+((stats.Strength - 10)/2)+")");
		Dex.SetText(stats.Dexterity+ "("+((stats.Dexterity - 10)/2)+")");
		Int.SetText(stats.Intelligence+ "("+((stats.Intelligence - 10)/2)+")");
		Con.SetText(stats.Constitution+ "("+((stats.Constitution - 10)/2)+")");
		Wis.SetText(stats.Wisdom+ "("+((stats.Wisdom - 10)/2)+")");
		Cha.SetText(stats.Charisma+ "("+((stats.Charisma - 10)/2)+")");
	}
	
	public void RandomizeOutfit()
	{
		var random = new Random();
		SkinColor(RandomColor());
		HairColor(RandomColor());
		EyeColor(RandomColor());
		PatternColor(RandomColor());
		HairSelect(random.Next(1, 7));
		EyeSelect(random.Next(0, 4));
		PatternSelect(random.Next(0, 4));
	}

	private Color RandomColor()
	{
		var random = new Random();
		return new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
	}
	
	public void Continue(){
		if (playerData.Class.Equals("No Input") || playerData.Name.Equals("No Input")) return;
		EmitSignal(SignalName.PDat, playerData);
		EmitSignal(SignalName.PStats, stats);
		SaveLoad SL = new SaveLoad();
		SL.Save(stats, Position, playerData);

		foreach (var child in GetTree().GetRoot().GetChildren())
		{
			if (child is MainMenu mainMenu)
			{
				mainMenu.Visible = false;
			}
		}
		GetTree().ChangeSceneToFile("res://Scenes/main.tscn");
	}


	
			/// <summary>
	/// Randomizes the stats for a character given a minimum amount of points per stat and a number of points to distribute
	/// </summary>
	/// <param name="minimumPerStat">The minimum number of points each stat should start with</param>
	/// <param name="points">The total number of points that should be randomly distributed</param>
public void RandomizeStats(int minimumPerStat, int points){
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
	
public void StatSetter(int health, int FP, int powersKnown){
		stats.MaxHealth = health;
		stats.CurrentHealth = health;
		stats.ClassFP = FP;
		stats.FP = FP;
		stats.PwrsKnown = powersKnown;
		EmitSignal(SignalName.PStats, stats);

	}
	
	public string StatMod(int stat){
		string mod;
		bool positive = false;
		if(((stat - 10)/2) > 0){
			positive = true;
		}
		
		if(positive){
			mod = (" (+ "+((stat-10)/2)+")");
		} else {
			mod = (" ("+((stat-10)/2)+")"); 
			}
		
		return mod;
	}
	
//end Character_select
}
