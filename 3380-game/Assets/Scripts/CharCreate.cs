using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerData
{
	public string Name { get; set; }
	public string Class { get; set; }
	public string Race { get; set; }
	public string Feature { get; set; }
	public string EyeColor { get; set; }
	public string HairColor { get; set; }
	public string FacialMarkings { get; set; }

	public int Strength { get; set; }
	public int Dexterity { get; set; }
	public int Intelligence { get; set; }
	public int Constitution { get; set; }
	public int Wisdom { get; set; }
	public int Charisma { get; set; }

	public PlayerData(string name, string characterClass, string race, string eyeColor, string hairColor, string facialMarkings)
	{
		Name = name;
		Class = characterClass;
		Race = race;
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

		if (Race.ToLower() == "twi'lek" || Race.ToLower() == "zabrak" || Race.ToLower() == "togruta")
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

		PlayerData player = new PlayerData(name, characterClass, race, eyeColor, hairColor, facialMarkings);
		player.RandomizeStats(3, 20);
		player.DisplayCharacter();
	}
}
