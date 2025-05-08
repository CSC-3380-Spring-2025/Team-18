using Godot;
using System;
using Game.Assets.Scripts;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Equipment : Control
{
	[Signal] public delegate void PositionSetEventHandler(Vector2 position);
	[Signal] public delegate void FreezeEventHandler();
	[Signal] public delegate void StatChangeEventHandler();
	
	[Export] public Label CurrentHP, MaxHP, AC;
	 
	public Attributes stats = new Attributes();

	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		EmitSignal(SignalName.PositionSet, new Vector2(x: (-540),y: (-150) ) );
		EmitSignal(SignalName.Freeze);
		StatLoad();
	}
	

	
	public void StatLoad(){
			using var file = FileAccess.Open("user://playerStats.dat", FileAccess.ModeFlags.Read);
		if (file is not null){
			var statsHolder = file.GetVar(true).As<Godot.Collections.Array>();
			
			stats.CharacterClass = statsHolder[0].AsString();
			stats.Level = statsHolder[1].As<int>();
			stats.XP.AddPoints(statsHolder[2].As<uint>());
			
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
		
		CurrentHP.SetText(""+ (((stats.MaxHealth +((stats.Constitution - 10)/2)) * stats.Level) - stats.Damage));
		MaxHP.SetText(""+ ((stats.MaxHealth +((stats.Constitution - 10)/2)) * stats.Level));
		AC.SetText(""+stats.Armor);
	}
	
	//end equipment
}
