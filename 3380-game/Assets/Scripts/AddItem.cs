using Godot;
using System;

public partial class AddItem : Button
{
	[Export] inventory inv; 
	[Export] int id; 
	[Export] string name; 
	[Export] Texture2D itmIcon;
	[Export] int maxQty; 
	[Export] int qty;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Pressed += () => AddAllItems();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void AddAllItems(){
		Item item = new Item{
			ID = id,
			Name = name, 
			Icon = itmIcon, 
			MaxQty = qty
		};
		inv.AddInventoryItem(item);
	}
}
