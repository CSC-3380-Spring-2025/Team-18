public class StarWarsSkillTreeDemo
{
	public static void Main()
	{
		var lightningTree = new SkillTree(SkillType.Lightning);
		var darkMagicTree = new SkillTree(SkillType.DarkMagic);
		var forceTree = new SkillTree(SkillType.TheForce);
		var lightMagicTree = new SkillTree(SkillType.LightMagic);
		var greyMagicTree = new SkillTree(SkillType.GreyMagic);

		// Lightning
		var forceSpark = new Skill("Force Spark", SkillType.Lightning, "Shoot a small spark of force lightning.", 1);
		var chainLightning = new Skill("Chain Lightning", SkillType.Lightning, "Lightning jumps to multiple targets.", 3);
		chainLightning.AddDependency(forceSpark);

		lightningTree.AddSkill(forceSpark);
		lightningTree.AddSkill(chainLightning);

		// Dark Magic
		var mindControl = new Skill("Mind Control", SkillType.DarkMagic, "Take control of enemy for a short time.", 2);
		darkMagicTree.AddSkill(mindControl);

		// The Force
		var forcePush = new Skill("Force Push", SkillType.TheForce, "Push enemies away using the Force.", 1);
		var forceChoke = new Skill("Force Choke", SkillType.TheForce, "Choke enemies from a distance.", 4);
		forceChoke.AddDependency(forcePush);

		forceTree.AddSkill(forcePush);
		forceTree.AddSkill(forceChoke);

		// Light Magic
		var healingAura = new Skill("Healing Aura", SkillType.LightMagic, "Heals nearby allies.", 2);
		var blindingLight = new Skill("Blinding Light", SkillType.LightMagic, "Temporarily blinds enemies.", 3);
		blindingLight.AddDependency(healingAura);

		lightMagicTree.AddSkill(healingAura);
		lightMagicTree.AddSkill(blindingLight);

		// Grey Magic
		var balanceShift = new Skill("Balance Shift", SkillType.GreyMagic, "Shift balance between light and dark.", 5);
		greyMagicTree.AddSkill(balanceShift);

		// Simulate Player Unlocking Skills
		var player = new PlayerSkills(level: 5);
		player.UnlockSkill(forceSpark);
		player.UnlockSkill(chainLightning);
		player.UnlockSkill(forcePush);
		player.UnlockSkill(forceChoke);
		player.UnlockSkill(balanceShift);
	}
}
