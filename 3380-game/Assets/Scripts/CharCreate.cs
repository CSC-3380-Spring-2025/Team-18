using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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
	string hair = "1", string eye = "1", string pattern = "1", Color skinColor = new Color(), 
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
			{"intelligence", minimumPerStat},
			{"constitution", minimumPerStat},
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
		Strength = statDictionary["strength"];
		Dexterity = statDictionary["dexterity"];
		Intelligence = statDictionary["intelligence"];
		Constitution = statDictionary["constitution"];
		Wisdom = statDictionary["wisdom"];
		Charisma = statDictionary["charisma"];
	}
	


public void DisplayCharacter()
{
		Console.WriteLine("\nCharacter Created!");
		Console.WriteLine($"Name: {Name}");
		Console.WriteLine($"Class: {Class}");
		Console.WriteLine($"Race: {Race}");
		Console.WriteLine($"Feature: {Feature}");
		Console.WriteLine($"Eye Color: {EyeColor}");
		Console.WriteLine($"Hair Color: {HairColor}");
		Console.WriteLine($"Facial Markings: {FacialMarkings}");
		Console.WriteLine($"Strength: {Strength}");
		Console.WriteLine($"Dexterity: {Dexterity}");
		Console.WriteLine($"Intelligence: {Intelligence}");
		Console.WriteLine($"Constitution: {Constitution}");
		Console.WriteLine($"Wisdom: {Wisdom}");
		Console.WriteLine($"Charisma: {Charisma}");
	}
}
class Program
{
	static void Main(string[] args)
	{
		Console.Write("Enter character name: ");
		string name = Console.ReadLine();

		Console.Write("Enter character class: ");
		string characterClass = Console.ReadLine();

		Console.Write("Choose Race (Human/Twi'lek/Zabrak/Togruta): ");
		string race = Console.ReadLine().ToLower();

		string hairColor = "None";
		string facialMarkings = "None";

		Console.Write("Choose Eye Color (Blue, Green, Brown, Red, Yellow, etc.): ");
		string eyeColor = Console.ReadLine();

		if (race == "human")
		{
			Console.Write("Choose Hair Color (Black, Blonde, Brown, Red, White, etc.): ");
			hairColor = Console.ReadLine();
		}
		else
		{
			Console.Write("Choose Facial Markings Color (Red, Black, White, Blue, etc.): ");
			facialMarkings = Console.ReadLine();
		}

		string hair = "", eye = "", pattern = "";

		PlayerData player = new PlayerData(name, characterClass, race, hair, eye, pattern,
		 new Color(), new Color(), new Color(), new Color());
		player.RandomizeStats(3, 20);
		player.DisplayCharacter();
	}
}
