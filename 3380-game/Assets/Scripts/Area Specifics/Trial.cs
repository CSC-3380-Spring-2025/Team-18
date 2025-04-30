using Godot;
using System;

public partial class Trial : Node2D
{
	[Export] public CanvasItem Waves1, Waves2, W1U, W1L, W2U, W2L;
	[Export] public CollisionPolygon2D Waves1Upper, Waves1Lower, Waves2Upper, Waves2Lower;
	public double holder = 0;
	public override void _Ready(){
		
	}
	public override void _Process(double delta){
		//delta == 1/60, shows the time passed since last frame! ugh of course
		holder += delta;
		//60 ticks per second
		//Math.Round(holder, MidpointRounding.AwayFromZero)
		if( (holder % (delta*60 * 2) ) <= 0.016){
			//GD.Print("Flipped! Holder val: "+holder);
			WFlip();
		} else {
			//GD.Print(holder);
		}
	} 
	
	public void WFlip(){
		Waves1.Visible = !Waves1.Visible;
		Waves1Upper.Disabled = !Waves1Upper.Disabled;
		Waves1Lower.Disabled = !Waves1Lower.Disabled;
		
		Waves2.Visible = !Waves2.Visible;
		Waves2Upper.Disabled = !Waves2Upper.Disabled;
		Waves2Lower.Disabled = !Waves2Lower.Disabled;
	}

}
