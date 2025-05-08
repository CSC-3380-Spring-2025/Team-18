using Godot;
using System;

public partial class StatScreen : Control
{
		[Signal] public delegate void FreezeEventHandler();
		[Signal] public delegate void PositionSetEventHandler(Vector2 position);


		[Export] public Label ClassDescription, MaxHP, CurrentHP, AC, Level, Str, Dex, Con, Int, Wis, Cha;
		public Attributes stats = new Attributes();
		
		public override void _Ready(){
			EmitSignal(SignalName.PositionSet, new Vector2(x: (0),y: (0) ) );
			EmitSignal(SignalName.Freeze);
			StatLoad();
		}

	public void StatLoad(){
			using var file = FileAccess.Open("user://playerStats.dat", FileAccess.ModeFlags.Read);
		if (file is not null){
			var statsHolder = file.GetVar(true).As<Godot.Collections.Array>();
			stats.CharacterClass = statsHolder[0].AsString();
			stats.Level = statsHolder[1].As<int>();
//			stats.XP= statsHolder[2].As<int>();
			stats.MaxHealth = statsHolder[3].As<int>();
			stats.CurrentHealth = statsHolder[4].As<int>();
			stats.Damage = statsHolder[5].As<int>();
			stats.Armor = statsHolder[6].As<int>();
			
			stats.ClassFP = statsHolder[7].As<int>();
			stats.FP = statsHolder[8].As<int>();
			stats.PwrsKnown = statsHolder[9].As<int>();
			stats.PwrMax = statsHolder[10].As<int>();
			
			stats.Strength = statsHolder[11].As<int>();
			stats.Dexterity = statsHolder[12].As<int>();
			stats.Constitution = statsHolder[13].As<int>();
			stats.Intelligence = statsHolder[14].As<int>();
			stats.Wisdom= statsHolder[15].As<int>();
			stats.Charisma= statsHolder[16].As<int>();
		}
		
		Str.SetText("Strength: "+stats.Strength + StatMod(stats.Strength));
		Dex.SetText("Dexterity: "+stats.Dexterity+ StatMod(stats.Dexterity));
		Con.SetText("Constitution: "+stats.Constitution+ StatMod(stats.Constitution));
		Int.SetText("Intelligence: "+stats.Intelligence+ StatMod(stats.Intelligence));
		Wis.SetText("Wisdom: "+stats.Wisdom+StatMod(stats.Wisdom));
		Cha.SetText("Charisma: "+stats.Charisma+ StatMod(stats.Charisma));
		
		CurrentHP.SetText("CurrentHP: "+ (((stats.MaxHealth +((stats.Constitution - 10)/2)) * stats.Level) - stats.Damage));
		MaxHP.SetText("Max: "+ ((stats.MaxHealth +((stats.Constitution - 10)/2)) * stats.Level));
		AC.SetText("AC: "+stats.Armor);
		Level.SetText("Level: "+stats.Level);
		ClassDescription.SetText(""+stats.CharacterClass);
	}
	
	public string StatMod(int stat){
		string mod;
		bool positive = false;
		if(((stat - 10)/2) > 0){
			positive = true;
		}
		if(positive){
			mod = (" (+"+((stat-10)/2)+")");
		} else {
			mod = (" ("+((stat-10)/2)+")"); 
			}
		return mod;
	}
}
