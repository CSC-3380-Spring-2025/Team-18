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
	public int Damage {get;set;} //damage taken
	
	public string CharacterClass { get; set; }
	public int Armor { get; set; }
	
	public int Level {get;set;}
	public Experience XP {get; private set;}
	
	public int ClassFP {get; set;} //number of base points, determined by class
	public int FP {get;set;} //how many points the player can spend on "magic"
	public int PwrsKnown {get; set;} //how many "magic" things the character knows
	public int PwrMax {get;set;}//the max level of thing the player can cast
	

	public Attributes(
		string charClass = "placeholder",
		int lvl = 1, int xp = 0, 
		int hp = 1, int cHp = 1, int dmg = 0,int armor = 10, 
		int fp = 1, int ttlFP = 1, int pwrKwn = 0, int pwrMx = 1,
		int str = 8, int dex = 8, int con = 8, int intel = 8, int wis = 8, int cha = 8)
	{
		CharacterClass = charClass;
		Level = lvl;
		XP = new Experience();
		MaxHealth = hp*lvl;
		CurrentHealth = hp-dmg;
		Damage = dmg;
		Armor = armor;
		
		ClassFP = fp;
		FP = fp*lvl;
		PwrsKnown = pwrKwn;
		PwrMax = pwrMx;
		
		Strength = str;
		Dexterity = dex;
		Constitution = con;
		Intelligence = intel;
		Wisdom = wis;
		Charisma = cha;
	}
}
