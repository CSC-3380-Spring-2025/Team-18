using Godot;
using System;

public partial class Insight : Sprite2D
{
	public struct PlateHolder{
		public String itemName;
		public String itemDesc;
	};

	[Export] public CollisionPolygon2D Gate;
	public Godot.Collections.Array array = new Godot.Collections.Array();
	public PlateHolder holder = new PlateHolder();
	public String ExpectedSocket = null;
	public CollisionPolygon2D tablet = null;
	public Node obj = null;
	public Label SocketLabel = null;
	public Node Socket = null;
	public Button menu = null;
	public String SocketName = null;
	public Color labelColor = new Color();
	public bool pressed = false;
	public float time = 0;
	
	//gets the tablet's node
	public void NodeTaker(Node node){
		tablet = (CollisionPolygon2D)node;
	}
	
	public void ItemTaker(String itemName, String itemDescription){
		holder.itemName = itemName;
		holder.itemDesc = itemDescription;
		ExpectedSocket = itemName;
		
		GD.Print("ExpectedSocket: "+ExpectedSocket);
		GD.Print("Holder: "+holder.itemName+" , "+holder.itemDesc);
	}
	
	public void SocketFinder(Node socket){
		SocketLabel = (Label)socket.FindChild("Label");
		labelColor = SocketLabel.GetSelfModulate();
		menu = (Button)SocketLabel.FindChild("MenuButton");
		SocketName = socket.Name;
	}
	
	public void MenuPressed(){
		pressed = true;
		SocketLabel.Visible = true;
		labelColor = SocketLabel.GetSelfModulate();
		labelColor.A = 1;
		SocketLabel.SelfModulate = (labelColor);
		time = 0;
		
		GD.Print("SocketName: "+SocketName);
		
		if(SocketName == ExpectedSocket && SocketName != null){
				array.Add(SocketName);
				GD.Print("array: "+array);
				ExpectedSocket = null;
			} else {
				GD.Print("ExpectedSocket: "+ExpectedSocket+", SocketName: "+SocketName);
			}
			if(array.Contains(SocketName)){
				SocketLabel.SetText("You have already placed this tablet.");
			} else if(SocketName == null){
				SocketLabel.SetText("You do not have a tablet to place.");
			} else if(SocketName == ExpectedSocket && SocketName != null){
				SocketLabel.SetText("The socket gives off a faint, almost pleased, glow.");
				tablet.Disabled = true;
			} else {
				SocketLabel.SetText("The socket seems to shudder, before ejecting the tablet.");
			}
			
	}
	
	public override void _Ready(){
		
		
	}
	
	public void GateOpen(){
		Gate.Disabled = true;
	}
	
	public override void _Process(double delta){
	
		if(pressed == true){
			
			
			if(labelColor.A > 0){
				labelColor.A -= (float)delta;
				SocketLabel.SelfModulate = (labelColor);
			} else{pressed = false; SocketLabel.Visible = false;}
			
			
		}

		if(array.Count > 9){
				GateOpen();
		}

	//end process
	}

	
}
