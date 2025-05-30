using Godot;
using System.Collections.Generic;

public class Gear
{
	public string Name;
	public int AttackBonus;
	public int DefenseBonus;
	public string IconPath;

	public Gear(string name, int attackBonus, int defenseBonus, string iconPath)
	{
		Name = name;
		AttackBonus = attackBonus;
		DefenseBonus = defenseBonus;
		IconPath = iconPath;
	}
}

public partial class EquipmentScreen : Control
{
	private GridContainer gearContainer;
	private Label statsLabel;
	private TextureRect gearPreview;

	private List<Gear> availableGear = new List<Gear>();
	private Gear equippedGear;

	// Example base stats
	private int baseAttack = 10;
	private int baseDefense = 5;

	public override void _Ready()
	{
		gearContainer = GetNode<GridContainer>("GearContainer");
		statsLabel = GetNode<Label>("StatsLabel");
		gearPreview = GetNode<TextureRect>("GearPreview");

		// Add some sample gear items
		availableGear.Add(new Gear("Iron Sword", 5, 0, "res://Icons/iron_sword.png"));
		availableGear.Add(new Gear("Steel Shield", 0, 5, "res://Icons/steel_shield.png"));
		availableGear.Add(new Gear("Leather Armor", 0, 3, "res://Icons/leather_armor.png"));

		BuildGearUI();
		UpdateStatsDisplay();
	}

	private void BuildGearUI()
	{
		// Clear any existing children
		foreach (Node child in gearContainer.GetChildren())
			child.QueueFree();

		// Create a button for each gear item
		foreach (Gear gear in availableGear)
		{
			Button gearButton = new Button();
			gearButton.Name = gear.Name;
			gearButton.Text = gear.Name;
			//gearButton.RectMinSize = new Vector2(150, 50);
//			gearButton.Connect("pressed", new Callable(this, "OnGearSelected"), new Godot.Collections.Array { gear.Name });
//			gearContainer.AddChild(gearButton);
		}
	}

	public void OnGearSelected(string gearName)
	{
		foreach (Gear gear in availableGear)
		{
			if (gear.Name == gearName)
			{
				EquipGear(gear);
				break;
			}
		}
	}

	private void EquipGear(Gear gear)
	{
		equippedGear = gear;
		UpdateStatsDisplay();
		UpdateGearPreview();
		GD.Print("Equipped: " + gear.Name);
	}

	private void UpdateStatsDisplay()
	{
		int totalAttack = baseAttack, totalDefense = baseDefense;
		if (equippedGear != null)
		{
			totalAttack += equippedGear.AttackBonus;
			totalDefense += equippedGear.DefenseBonus;
		}
		statsLabel.Text = $"Attack: {totalAttack}\nDefense: {totalDefense}";
	}

	private void UpdateGearPreview()
	{
		if (equippedGear != null && gearPreview != null)
		{
			Texture newTexture = ResourceLoader.Load<Texture>(equippedGear.IconPath);
//			if (newTexture != null)
//				gearPreview.Texture = newTexture;
		}
	}
}
