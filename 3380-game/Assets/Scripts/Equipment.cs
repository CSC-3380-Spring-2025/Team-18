using Godot;
using System;

public partial class Equipment : Control
{
	[Signal] public delegate void PositionSetEventHandler(Vector2 position);
	[Signal] public delegate void FreezeEventHandler();
	
	public override void _Ready()
	{
		EmitSignal(SignalName.PositionSet, new Vector2(x: (260),y: (396) ) );
		EmitSignal(SignalName.Freeze);
	}
	
	
}
