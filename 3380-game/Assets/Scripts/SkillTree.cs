using System;
using System.Collections.Generic;

public enum SkillType
{
	Lightning,
	DarkMagic,
	TheForce,
	LightMagic,
	GreyMagic
}

public class Skill
{
	public string Name { get; private set; }
	public SkillType Type { get; private set; }
	public string Description { get; private set; }
	public int LevelRequired { get; private set; }
	public List<Skill> Dependencies { get; private set; }

	public Skill(string name, SkillType type, string description, int levelRequired)
	{
		Name = name;
		Type = type;
		Description = description;
		LevelRequired = levelRequired;
		Dependencies = new List<Skill>();
	}

	public void AddDependency(Skill skill)
	{
		Dependencies.Add(skill);
	}

	public bool CanUnlock(List<Skill> unlockedSkills, int currentLevel)
	{
		if (currentLevel < LevelRequired)
			return false;

		foreach (var dep in Dependencies)
		{
			if (!unlockedSkills.Contains(dep))
				return false;
		}

		return true;
	}
}

public class SkillTree
{
	public SkillType Type { get; private set; }
	public List<Skill> Skills { get; private set; }

	public SkillTree(SkillType type)
	{
		Type = type;
		Skills = new List<Skill>();
	}

	public void AddSkill(Skill skill)
	{
		if (skill.Type != Type)
			throw new ArgumentException("Skill type mismatch with tree.");
		Skills.Add(skill);
	}
}

public class PlayerSkills
{
	public int Level { get; set; }
	public List<Skill> UnlockedSkills { get; private set; }

	public PlayerSkills(int level)
	{
		Level = level;
		UnlockedSkills = new List<Skill>();
	}

	public bool UnlockSkill(Skill skill)
	{
		if (skill.CanUnlock(UnlockedSkills, Level))
		{
			UnlockedSkills.Add(skill);
			Console.WriteLine($"Unlocked: {skill.Name}");
			return true;
		}

		Console.WriteLine($"Cannot unlock: {skill.Name}");
		return false;
	}
}
