using Godot;
using System;

public partial class Interact : Node
{
	//[Export] public String ObjectName;
	[Export] public Label ObjectText;
	[Export] public int timer;
	[Export] public bool pickup = false;
	[Export] public bool random = false;
	[Export] public String ItemName = null;
	[Export] public String ItemDescription = null;
	[Export] public Godot.Collections.Array Options = new Godot.Collections.Array();

	
	[Signal] public delegate void PickupEventHandler(String itemName, String itemDescription);
	[Signal] public delegate void SendNodeEventHandler(Area2D node);
	
	public Random rand = new Random();

	private double holder = 0;
	private bool check = false;
	[Export] public Color textColor;
	
	private void InReach(Node2D player){

		

		if(ItemDescription != null && !random){
			ObjectText.SetText(ItemDescription);
		}
		
		if(ItemDescription != null && random){
			int size = Options.Count;
			String option = (String)Options[rand.Next(0, size)];
			String msg = String.Format(ItemDescription, option);
			ObjectText.SetText(msg);
			
		}

			ObjectText.Visible = true;
			textColor.A = 1;
			ObjectText.Modulate = textColor;
			check = true;
			holder = 0;
			
			if(this.IsInGroup("Pickup")){
				Node thisNode = this.GetNode(""+this.Name);
				EmitSignal(SignalName.Pickup, ItemName, ItemDescription);
				EmitSignal(SignalName.SendNode, thisNode);
			}
	}
	
	public override void _Ready(){ }
	
	public override void _Process(double delta){
		
		holder += delta;
		float fDelta = (float)delta;
		
		if(check == true && holder >= timer){
			if(textColor.A > 0){
				textColor.A -= fDelta;
				ObjectText.Modulate = (textColor);
			} else {check = false; ObjectText.Visible = false;}
		}
		

	//end process
	}
	
	//end interact
}
