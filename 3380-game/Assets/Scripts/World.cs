using Godot;
using System;

public partial class World : Node
{
	public Node Land;
	public Node child = null;
	public SaveLoad SL = new SaveLoad();
	public Vector2 PlayerPosition;
	public String location;
	[Signal] public delegate void PositionSetEventHandler(Vector2 position);
	[Signal] public delegate void LocationEventHandler(string place);
	
	public override void _Ready(){
		 Land = GetNode("/root/Main/World");
		
		
	}
	
	public void PositionChecker(String location){
		Node node = GetNode(location);
		if(node == Land.GetChild(0)){
			GD.Print("PositionChecker says node exists already");
		}else {
			
			SwitchLand(location);
		}
	}
	
	public void PositionTaker(Vector2 position){
		PlayerPosition = new Vector2(x: position.X, y: position.Y);
		GD.Print("World PositionTaker:"+ PlayerPosition);
		SL.SavePosition(PlayerPosition, location);
	}
	
	public void GoHomeP(Node2D player){
		//EmitSignal(SignalName.PositionSet, new Vector2(x: 43,y: -27 ));
		SwitchLand("res://Scenes/Areas/Ossus/Bedroom.tscn");
		
	}
	
	public void LeaveHouse(Node2D player){
		SwitchLand("res://Scenes/Areas/Ossus/housing.tscn");
		//EmitSignal(SignalName.PositionSet, new Vector2(x: 1440,y: 600 ));
		
	}
	
	public void Commons(Node2D player){
		//EmitSignal(SignalName.PositionSet, new Vector2(x: 43,y: -27 ));
		SwitchLand("res://Scenes/Areas/Ossus/Commons.tscn");
	}
	
	public void TrainingArea(Node2D player){
		SwitchLand("res://Scenes/Areas/Ossus/training_area.tscn");
	}
	
	public void CouncilArea(Node2D player){
		SwitchLand("res://Scenes/Areas/Ossus/council_area.tscn");
	}
	
	
	
	public void SwitchLand(string scenePath){
		EmitSignal(SignalName.Location, scenePath);
		location = scenePath;
		SL.LoadPosition(PlayerPosition, location);
		GD.Print("World PP:"+PlayerPosition);
		GD.Print("World location:"+location);
		PackedScene scene = GD.Load<PackedScene>(scenePath);
		child = Land.GetChild(0);
		
		if(child != null){
			child.QueueFree();
		}
		
		if(scene != null){
			child = scene.Instantiate();
			Land.AddChild(child);
			SL.SavePosition(PlayerPosition, scenePath);
		}
			
	}
}
