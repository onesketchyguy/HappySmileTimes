using System;
using System.Collections.Generic;

[Serializable]
public class CombatUniversals
{
    public enum CLASSTYPE { Weak, Tank, Puncher, Runner }

    public CLASSTYPE Class = CLASSTYPE.Weak;

    public string Name = "Nameless";

    public UnityEngine.Sprite Image;

    //Stats
    public Stat Strength; //attack
    public Stat Agility; //attackspeed
    public Stat Chin; //defence
    public Stat StaminaStat; // PP

    public int experience;

    public GameManager.States CurrentState = GameManager.States.Normal;

    // Main Values
    public void FillHealth()
    {
        CurrentHealth = MaxHealth;
    }

    public int MaxHealth => 5 + (Chin.level * 5);
    public int CurrentHealth;

    public int MaxStamina => 5 + (StaminaStat.level * 5);
    public int CurrentStamina;

    public bool isDead => CurrentHealth <= 0;

    public int PowerLevel => StaminaStat.level + Strength.level + Chin.level + Agility.level;

    public void Inititialize()
    {
        if (attacks.Count < 1)
        {
            attacks.Add(GameManager.instance.moves.ToArray()[0]);
        }

        if (Strength.experience == 0 && StaminaStat.experience == 0 && Agility.experience == 0 && Chin.experience == 0)
        {
            switch (Class)
            {
                case CLASSTYPE.Weak:

                    Strength.level = 1;

                    StaminaStat.level = 1;

                    Agility.level = 1;

                    Chin.level = 1;

                    break;
                case CLASSTYPE.Tank:

                    Strength.level = 3;

                    StaminaStat.level = 2;

                    Agility.level = 2;

                    Chin.level = 5;

                    break;
                case CLASSTYPE.Puncher:

                    Strength.level = 5;

                    StaminaStat.level = 3;

                    Agility.level = 2;

                    Chin.level = 2;

                    break;
                case CLASSTYPE.Runner:

                    Strength.level = 2;

                    StaminaStat.level = 3;

                    Agility.level = 5;

                    Chin.level = 2;

                    break;
                default:
                    break;
            }

            Strength.experience = 0;
            StaminaStat.experience = 0;
            Agility.experience = 0;
            Chin.experience = 0;
        }

        FillHealth();
        CurrentStamina = MaxStamina;
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

        public GameManager.States Effect = GameManager.States.Normal;

        public int power;

        internal int experience = -1;

        public int maxExperience => power * 5;

        public List<Move> Upgrade = new List<Move> { };
    }
}
