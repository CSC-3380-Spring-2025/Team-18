using Godot;
using System;
using Game.Assets.Scripts;
using System.Collections.Generic;
using System.Linq;

public partial class Attributes : GodotObject
{
	public int Strength { get; set; }
	public int Dexterity { get; set; }
	public int Constitution { get; set; }
	public int Intelligence { get; set; }
	public int Wisdom { get; set; }
	public int Charisma { get; set; }
	
	public int MaxHealth { get; set; }
	public int CurrentHealth {get; set;}
	
	
	public string CharacterClass { get; set; }
	public int Armor { get; set; }
	
	public int Level {get;set;}
	public int XP {get; set;}
	
	//public int ClassFP {get; set;}
	//public int ClassHP {get; set;}
	//public int PwrsKnown {get; set;}
	//public int PwrMax {get;set;}
	//

	public Attributes(string charClass = "placeholder", int lvl = 1, int xp = 0, int hp = 1, int cHp = 1, int armor = 8, 
	int str = 8, int dex = 8, int con = 8, int intel = 8, int wis = 8, int cha = 8)
	{
		CharacterClass = charClass;
		MaxHealth = hp;
		CurrentHealth = cHp;
		Armor = armor;
		Level = lvl;
		XP = xp;
		Strength = str;
		Dexterity = dex;
		Constitution = con;
		Intelligence = intel;
		Wisdom = wis;
		Charisma = cha;
		
	}
	
	

}
