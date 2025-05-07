using Godot;
using System;

public partial class Spirit : Node
{
	public struct PlateHolder{
		public String itemName;
		public String itemDesc;
	};
	
	[Export] public CollisionPolygon2D Door;
	[Export] public Label DoorLabel;
	public Godot.Collections.Array array = new Godot.Collections.Array();
	public PlateHolder holder = new PlateHolder();
	public String ExpectedSocket = "null";
	public CollisionPolygon2D cluster = null;
	public Node obj = null;
	public Label SocketLabel = null;
	public Node Socket = null;
	public Button menu = null;
	public String SocketName = null;
	public Color labelColor = new Color();
	public bool pressed = false;
	public float time = 0;
	public Godot.Collections.Array keyHolder = new Godot.Collections.Array();

	//gets the tablet's node
	public void NodeTaker(Node node){
		cluster = (CollisionPolygon2D)node;
		String objName = cluster.Name;
		if((objName).Contains("Crystal")){
			cluster.CallDeferred(CollisionPolygon2D.MethodName.SetDisabled, true);
		}
	}
	
	public void ItemTaker(String itemName, String itemDescription){
		holder.itemName = itemName;
		holder.itemDesc = itemDescription;
		if(itemName.Contains("Keystone")){
			ExpectedSocket = itemName;
		}
	
	}
	
	public void SocketFinder(Node socket){
		SocketLabel = (Label)socket.FindChild("Label");
		labelColor = SocketLabel.GetSelfModulate();
		menu = (Button)SocketLabel.FindChild("Button");
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
		GD.Print("ExpectedSocket: "+ExpectedSocket);
		if(ExpectedSocket.Contains("Keystone") && array.Contains(SocketName) == false){
				array.Add(SocketName);
				GD.Print("array: "+array);
				GD.Print("KeyHolder: "+keyHolder);
			} 
			
			if(ExpectedSocket.Contains("Keystone") && keyHolder.Contains(SocketName)){
				String msg = String.Format("The keystone slots into place. {0} Left.", (3-array.Count));
				SocketLabel.SetText(msg);
				keyHolder.Remove(SocketName);
				cluster.Disabled = true;
				ExpectedSocket = "null";
			} else if(array.Contains(SocketName) && keyHolder.Contains(SocketName) == false){
				SocketLabel.SetText("There is already a keystone in place.");
			} else if(ExpectedSocket.Contains("Keystone") == false  && array.Contains(SocketName) == false){
				SocketLabel.SetText("You do not have a Keystone to place.");
			}
			
	}
	
	public override void _Ready(){
		keyHolder.Add("Slot1");
		keyHolder.Add("Slot2");
		keyHolder.Add("Slot3");
	}
	
	public void GateOpen(){
		Door.Disabled = true;
		DoorLabel.Visible = true;
		DoorLabel.SetText("The Gate slides open. You've Passed your trials. Welcome to the new world, young knight.");
	}
	
	public override void _Process(double delta){
		if(pressed == true){
			
			if(labelColor.A > 0){
				labelColor.A -= (float)delta;
				SocketLabel.SelfModulate = (labelColor);
			} else{
				pressed = false; SocketLabel.Visible = false;}
			
		}
		if(array.Count > 2){
				GateOpen();
				
		}
		
	//end process
	}
	
}
