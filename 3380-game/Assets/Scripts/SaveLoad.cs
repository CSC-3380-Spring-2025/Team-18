//namespace.Game.Assets.Scripts.SaveLoad
using Godot;
using System;
using Game.Assets.Scripts;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class SaveLoad : Node, Savable, Loadable
{
	public PlayerData playerData;
	public Attributes stats;
	public Vector2 Position;
	
	public void Save(){
		
	}
	public void Load(){
		
	}
	public void Save(Attributes stats, Vector2 Position, PlayerData playerData)
	{
		SaveStats(stats);
		SaveSprite(playerData);
		using var file = FileAccess.Open("user://playerData_v3.dat", FileAccess.ModeFlags.Write);
		file.StoreVar(Position);
		file.Store32(stats.XP.Points);
	}
	
	public void Load(Attributes stats, Vector2 Position, PlayerData playerData)
	{   //for some reason once i run the bitch enough times it breaks. for no reason. if that happens to you,
		//just change the file name to something that hasnt been used yet and it should be fine.
		using var file = FileAccess.Open("user://playerData_v3.dat", FileAccess.ModeFlags.Read);
		if (file is not null)
		{	
			Position = (Vector2)file.GetVar();
			stats.XP.AddPoints(file.Get32());
		}
		LoadSprite(playerData);
		LoadStats(stats);
	}
	
	public void SaveSprite(PlayerData playerData){
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
	}
	
	public void LoadSprite(PlayerData playerData){
		using var file = FileAccess.Open("user://playerSprite0.dat", FileAccess.ModeFlags.Read);
		if (file is not null){
			var holder = file.GetVar(true).As<Godot.Collections.Array>();
			playerData.Name = holder[0].AsString();
			playerData.Class= holder[1].AsString();
			playerData.Race= holder[2].AsString();
			playerData.Hair= holder[3].AsString();
			playerData.Eye= holder[4].AsString();
			playerData.Pattern= holder[5].AsString();
			playerData.SkinColor= holder[6].As<Color>();
			playerData.EyeColor= holder[7].As<Color>();
			playerData.HairColor= holder[8].As<Color>();
			playerData.FacialMarkings= holder[9].As<Color>();

	}
}
	public void SaveStats(Attributes stats){
		using var file = FileAccess.Open("user://playerStats.dat", FileAccess.ModeFlags.Write);
			var statHolder = new Godot.Collections.Array();
			statHolder.Add(stats.CharacterClass);
			statHolder.Add(stats.Level);
			statHolder.Add(stats.XP.Points);
			statHolder.Add(stats.MaxHealth);
			statHolder.Add(stats.CurrentHealth);
			statHolder.Add(stats.Damage);
			statHolder.Add(stats.Armor);
			
			statHolder.Add(stats.ClassFP);
			statHolder.Add(stats.FP);
			statHolder.Add(stats.PwrsKnown);
			statHolder.Add(stats.PwrMax);
			
			statHolder.Add(stats.Strength);
			statHolder.Add(stats.Dexterity);
			statHolder.Add(stats.Constitution);
			statHolder.Add(stats.Intelligence);
			statHolder.Add(stats.Wisdom);
			statHolder.Add(stats.Charisma);
			
			file.StoreVar(statHolder);
	}
public void LoadStats(Attributes stats){
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
}


}
