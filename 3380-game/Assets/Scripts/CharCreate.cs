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
<<<<<<< Updated upstream
	public Experience Experience { get; private set; }


	public int Strength { get; set; }
	public int Dexterity { get; set; }
	public int Intelligence { get; set; }
	public int Constitution { get; set; }
	public int Wisdom { get; set; }
	public int Charisma { get; set; }

=======

	
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
		Experience = new Experience();

		Random rand = new Random();
		Strength = rand.Next(1, 21);
		Dexterity = rand.Next(1, 21);
		Intelligence = rand.Next(1, 21);
		Constitution = rand.Next(1, 21);
		Wisdom = rand.Next(1, 21);
		Charisma = rand.Next(1, 21);

		if (Race.ToLower() == "Twilek" || Race.ToLower() == "Zabrak" || Race.ToLower() == "Togruta")
		{
			Feature = "Facial Markings";
		}
		else if (Race.ToLower() == "human")
		{
			Feature = "Hair";
		}
		else
		{
			Feature = "Unknown";
		}
=======
>>>>>>> Stashed changes
	}
	// player.RandomizeStats(3, 20);

	//end PlayerData
}
