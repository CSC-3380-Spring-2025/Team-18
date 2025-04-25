using Godot;
using System;
using System.Threading.Tasks;

public partial class Combat : CanvasLayer
{
	[Export] public Node playerUnitRoot;
	[Export] public Node enemyUnitRoot;

	[Export] public RichTextLabel combatLog;
	[Export] public Button basicAttackButton;
	[Export] public Button forceAttackButton;
	[Export] public Button itemsButton;
	[Export] public Button fleeButton;

	private Unit playerUnit;
	private Unit enemyUnit;

	private enum BattleState { PlayerTurn, EnemyTurn, Win, Lose }
	private BattleState state;

	public override void _Ready()
	{
		playerUnit = playerUnitRoot.GetNode<Unit>("CombatStats");
		enemyUnit = enemyUnitRoot.GetNode<Unit>("CombatStats");

		state = BattleState.PlayerTurn;
		combatLog.Text = "Enemy Attacks!\n";
	}

	private void OnBasicAttackPressed()
	{
		if (state != BattleState.PlayerTurn)
			return;

		int damage = playerUnit.Attack(enemyUnit);
		combatLog.Text += $"You dealt {damage} damage!\n";

		CheckBattleOutcome();
		if (state == BattleState.PlayerTurn)
			_ = ToEnemyTurn();
	}

	private void OnForceAttackPressed()
	{
		if (state != BattleState.PlayerTurn)
			return;

		int damage = playerUnit.ForceAttack(enemyUnit);
		combatLog.Text += $"You used a Force Attack and dealt {damage}!\n";

		CheckBattleOutcome();
		if (state == BattleState.PlayerTurn)
			_ = ToEnemyTurn();
	}

	private void OnItemsPressed()
	{
		combatLog.Text += "You checked your items...but you have none!\n";
	}

	private void OnFleePressed()
	{
		combatLog.Text += "You've fled the battle!\n";
		state = BattleState.Lose;
	}

	private async Task ToEnemyTurn()
	{
		state = BattleState.EnemyTurn;
		await ToSignal(GetTree().CreateTimer(1.0f), "timeout");

		int action = GD.RandRange(0, 1);
		if (action == 0)
		{
			int damage = enemyUnit.Attack(playerUnit);
			combatLog.Text += $"Enemy attacks and deals {damage} damage!\n";
		}
		else
		{
			int heal = enemyUnit.Heal();
			combatLog.Text += $"Enemy heals for {heal}.\n";
		}

		CheckBattleOutcome();
		if (state != BattleState.Lose)
			state = BattleState.PlayerTurn;
	}

	private void CheckBattleOutcome()
	{
		if (playerUnit.IsDead)
		{
			combatLog.Text += "You have fallen.\n";
			state = BattleState.Lose;
		}
		else if (enemyUnit.IsDead)
		{
			combatLog.Text += "Enemy defeated! You've won!\n";
			state = BattleState.Win;
		}
	}
}
