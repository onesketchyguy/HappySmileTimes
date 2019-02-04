using System;
using System.Collections.Generic;

[Serializable]
public class CombatUniversals
{
    public string Name = "Nameless";

    public UnityEngine.Sprite Image;

    //Stats
    public Stat Stength; //attack
    public Stat Agility; //attackspeed
    public Stat Chin; //defence
    public Stat StaminaStat;

    public int experience;

    // Main Values
    public int MaxHealth => 5 + (Chin.level * 5);
    public int MaxStamina => 5 + (StaminaStat.level * 5);
    public int CurrentStamina;
    public int CurrentHealth;

    public bool isDead => CurrentHealth <= 0;

    public int PowerLevel => StaminaStat.level + Stength.level + Chin.level + Agility.level;

    public void Inititialize()
    {
        CurrentHealth = MaxHealth;
        CurrentHealth = MaxStamina;

        if (attacks.Count < 1)
        {
            attacks.Add(GameManager.instance.moves.ToArray()[0]);
        }
    }

    internal List<Move> attacks = new List<Move>() { };

    [Serializable]
    public class Stat
    {
        public int level = 1;

        public int experience = 0;
        public int maxExperience => level * 10;
    }

    [Serializable]
    public class Move
    {
        public string name;

        public string description;

        public enum reliance { Strength, Agility, Chin, Stamina }

        public reliance powerType;

        public int power;

        internal int experience;

        public int maxExperience => power * 10;

        public Move[] Upgrade;
    }
}
