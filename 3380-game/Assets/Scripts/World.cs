using Godot;
using System;

public partial class World : Node
{
	public Node Land;
	public Node child = null;
	public SaveLoad SL = new SaveLoad();
	[Signal] public delegate void PositionSetEventHandler(Vector2 position);
	[Signal] public delegate void LocationEventHandler(string place);
	
	public override void _Ready(){
		 Land = GetNode("/root/Main/World");
		
	}
	
	public void GoHomeP(Node2D player){
		EmitSignal(SignalName.PositionSet, new Vector2(x: 43,y: -27 ));
		SwitchLand("res://Scenes/Areas/Ossus/Bedroom.tscn");
		
	}
	
	public void LeaveHouse(Node2D player){
		SwitchLand("res://Scenes/Areas/Ossus/housing.tscn");
		EmitSignal(SignalName.PositionSet, new Vector2(x: 1440,y: 600 ));
		
	}
	
	public void Commons(Node2D player){
		SwitchLand("res://Scenes/Areas/Ossus/Commons.tscn");
	}
	
	public void TrainingArea(Node2D player){
		SwitchLand("res://Scenes/Areas/Ossus/training_area.tscn");
	}
	
	public void Template(Node2D player){
		SwitchLand("res://Scenes/Areas/Ossus/training_area.tscn");
	}
	public void SwitchLand(string scenePath){
		EmitSignal(SignalName.Location, scenePath);
		PackedScene scene = GD.Load<PackedScene>(scenePath);
		child = Land.GetChild(0);
		
		if(child != null){
			child.QueueFree();
		}
		
		if(scene != null){
			child = scene.Instantiate();
			Land.AddChild(child);
		}
			
	}
}
