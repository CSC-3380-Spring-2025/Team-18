using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts;

public partial class PlayerData : GodotObject
{
	public string Name { get; set; }
	public string Class { get; set; }
	public string Race { get; set; }
	public string Hair { get; set; }
	public string Eye { get; set; }
	public string Pattern { get; set; }
	public string Feature { get; set; }
	public Color SkinColor { get ; set; } 
	public Color EyeColor { get; set; }
	public Color HairColor { get; set; }
	public Color FacialMarkings { get; set; }


	public int Strength { get; set; }
	public int Dexterity { get; set; }
	public int Intelligence { get; set; }
	public int Constitution { get; set; }
	public int Wisdom { get; set; }
	public int Charisma { get; set; }


	
	public PlayerData(string name = "Placeholder", string characterClass = "Null", string race = "Human", 
	string hair = "1", string eye = "1", string pattern = "0", Color skinColor = new Color(), 
	Color eyeColor = new Color(), Color hairColor = new Color(), Color facialMarkings = new Color())
	{
		Name = name;
		Class = characterClass;
		Race = race;
		Hair = hair;
		Eye = eye;
		Pattern = pattern;
		SkinColor = skinColor;
		EyeColor = eyeColor;
		HairColor = hairColor;
		FacialMarkings = facialMarkings;
	}
	// player.RandomizeStats(3, 20);

	//end PlayerData
}
