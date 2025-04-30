using Godot;
using System;

public partial class Main : Node
{
	public SaveLoad SL = new SaveLoad();
	public Node root;
	public Node mainScene;
	public Vector2 PosHolder;
	public String LocHolder;
	[Signal] public delegate void LocationEventHandler(string place);
	[Signal] public delegate void PositionEventHandler(Vector2 position);
	[Signal] public delegate void LoadEventHandler();
	
	public override void _Ready(){
		LoadPos();
		LoadScene();
	}
	
	public void LocationSender(string place){
		SL.SaveScene(place);
	}
	
	public void PositionSender(Vector2 position){
		
		SL.SavePosition(position);
	}
	
	void LoadPos(){
		using var file = FileAccess.Open("user://playerData_v3.dat", FileAccess.ModeFlags.Read);
		if (file is not null)
		{	
			var holder = file.GetVar(true).As<Godot.Collections.Array>();
			PosHolder = new Vector2(x: (holder[0].As<int>()),y: (holder[1].As<int>()));
			EmitSignal(SignalName.Position, PosHolder);
		}
	}
	void LoadScene(){
		using var file = FileAccess.Open("user://PlayerScene.dat", FileAccess.ModeFlags.Read);
		if (file is not null)
		{	
			LocHolder = file.GetAsText();
		}
		EmitSignal(SignalName.Location, LocHolder);
	}
}
