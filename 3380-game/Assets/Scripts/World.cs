using Godot;
using System;

public partial class World : Node
{
	public Node Land;
	public Node2D player;
	public Node Main;
	public Node child = null;
	public SaveLoad SL = new SaveLoad();
	public String location;
	[Signal] public delegate void LocationEventHandler(string place);
	[Signal] public delegate void PositionSendEventHandler(Vector2 position);

	public override void _Ready(){
		Land = GetNode("/root/Main/World");
	}
	
	public void PositionChecker(String location){
		String place;
		int index = location.LastIndexOf('/');
		String store = location.Remove(0,index+1);
		int period = store.IndexOf('.');
		if(period >= 0){
			place = store.Remove(period);
			} else { place = store;}
			
			World world = this;
			world.CallDeferred(World.MethodName.SwitchLand,location);
	}
	
	public void GoHomeP(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/Bedroom.tscn");
	}
	
	public void LeaveHouse(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/housing.tscn");
	}
	
	public void Commons(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/Commons.tscn");
	}
	
	public void TrainingArea(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/training_area.tscn");
	}
	
	public void CouncilArea(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/council_area.tscn");
	}
	
	public void CouncilChambers(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/council_hall.tscn");
	}
	
	public void TrialStart(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/trial_start.tscn");
	}
	
	public void Courage(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/trial_of_courage.tscn");
	}
	public void Flesh(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/trial_of_flesh.tscn");
	}
	public void Insight(Node2D player){
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/trial_of_insight.tscn");
	}
	public void Spirit(Node2D player){
		player.Position = new Vector2(x: -3297, y: -4907);
		World world = this;
		world.CallDeferred(World.MethodName.SwitchLand,"res://Scenes/Areas/Ossus/trial_of_spirit.tscn");		
	}
	public void SwitchLand(string scenePath){
		EmitSignal(SignalName.Location, scenePath);
		location = scenePath;
		
		PackedScene scene = GD.Load<PackedScene>(scenePath);
		child = Land.GetChild(-1);
		
		if(child != null){
			child.QueueFree();
		}
		
		if(scene != null){
			child = scene.Instantiate();
			Land.AddChild(child);
			EmitSignal(SignalName.Location, scenePath);
			SL.SaveScene(scenePath);
		}
	}
}
