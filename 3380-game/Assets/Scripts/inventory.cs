using Godot;
using System;

public partial class Inventory : ItemList
{
	[Export] int inventorySize = 60;
	[Export] Texture2D blankIcon;
	
	private Item[] items;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		items = new Item[inventorySize];
		
		for(int i = 0; i < inventorySize; i++){
			AddItem(" ", blankIcon);
		}
		ItemClicked += OnInventoryClicked;
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	
	public bool AddInventoryItem(Item item){
		if(item == null || item.Qty <= 0) {
			return false;
		}	
		bool couldPickup = AddStackableItem(item);
		
		if(item.Qty == 0){
			return true;
		}
		for(int i = 0; i < inventorySize; i++){
			if(items[i] != null){
				continue;
			}
			items[i] = item;
			SetItemIcon(i, item.Icon);
			
			if(item.MaxQty > 1){
				SetItemText(i, items[i].Qty.ToString());
				
			}
			return true;
		}
		return couldPickup;
	}
	
	public void RemoveInventoryItem(int index){
		if(index < 0 || index >= inventorySize){
			return;
		}
		items[index] = null;
		SetItemText(index, " ");
	}
	
	public Item GetInventoryItem(int index){
		if(index < 0 || index >= inventorySize){
			return null;
		}
		return items[index];
	}
	private void OnInventoryClicked(long index, Vector2 pos, long mouseButtonIndex){
		if(mouseButtonIndex == 2){
			Item item = GetInventoryItem((int)index);
			
			if(item == null){
				GD.Print("No item here");
				return;
			}
			RemoveInventoryItem((int)index);
			
			GD.Print($"You dropped {item.Qty} of {item.Name}");
		} else if (mouseButtonIndex == 1){
			Item item = GetInventoryItem((int)index);
			
			if(item == null){
					GD.Print("No Item Here");
					return;
			}
			GD.Print($"You clicked {item.Name} and there is a total of {item.Qty} of them");
		}
	}
	
	private bool AddStackableItem(Item item){
		bool couldPickup = false;
		
		for(int i = 0; i < items.Length; i++){
			if(items[i] == null){
				continue;
			}
			if(items[i].ID != item.ID || items[i].Qty >= items[i].MaxQty){
				continue;
			}
			if(items[i].Qty + item.Qty > items[i].MaxQty){
				int amtToRemove = items[i].MaxQty - items[i].Qty;
				
				items[i].Qty = items[i].MaxQty;
				
				item.Qty = item.Qty - amtToRemove;
				
				couldPickup = true;
				SetItemText(i, items[i].Qty.ToString());
				continue;
			}
			items[i].Qty = item.Qty + items[i].Qty;
			item.Qty = 0;
			SetItemText(i, items[i].Qty.ToString());
			return true;

		}
		return couldPickup;
	}
}
public class Item{
	public int ID; 
	public string Name;
	public Texture2D Icon; 
	public int MaxQty;
	public int Qty; 
	
}
