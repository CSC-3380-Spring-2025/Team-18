using Godot;
using System;
using Game.Assets.Scripts.Loading;
using Game.Assets.Scripts.Saving;

public partial class Settings : CanvasLayer
{
	[Signal] public delegate void SaveEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnQuit(){

		EmitSignal(SignalName.Save);
		GetTree().Quit();
	}
	
	
}
