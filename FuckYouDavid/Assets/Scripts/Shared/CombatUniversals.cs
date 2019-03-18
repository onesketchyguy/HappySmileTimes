using System;
using System.Collections.Generic;

[Serializable]
public class CombatUniversals
{
    public string Name = "Nameless";

    public UnityEngine.Sprite Image;

    public enum CLASSTYPE { Weak, Tank, Puncher, Runner }

    public CLASSTYPE Class = CLASSTYPE.Weak;

    public GameManager.States weakness = GameManager.States.Burn;

    /// <summary>
    /// Use to increase the power of the given character
    /// </summary>
    public int powerLevel;

    //Stats
    public Stat Strength; //attack
    public Stat Agility; //attackspeed
    public Stat Chin; //defence
    public Stat StaminaStat; // PP

    internal int experience;

    internal GameManager.States CurrentState = GameManager.States.Normal;

    // Main Values
    internal void FillHealth()
    {
        CurrentHealth = MaxHealth;
    }

    internal int MaxHealth => 5 + (Chin.level * 5);
    internal int CurrentHealth;

    internal int MaxStamina => 5 + (StaminaStat.level * 5);
    internal int CurrentStamina;

    internal bool isDead => CurrentHealth <= 0;

    internal int PowerLevel => StaminaStat.level + Strength.level + Chin.level + Agility.level;

    internal void Inititialize()
    {
        if (Strength.level == -1 || StaminaStat.level == -1 || Agility.level == -1 || Chin.level == -1 || Strength.experience == -1 || StaminaStat.experience == -1 || Agility.experience == -1 || Chin.experience == -1)
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


        if (attacks.Count == 0)
        {
            attacks = GameManager.instance.attacks;
        }

        while (PowerLevel < powerLevel)
        {
            int r = UnityEngine.Random.Range(0, 3);
            //Instead of random use the class system.
            switch (r)
            {
                case 0:
                    Strength.level += 1;
                    break;
                case 1:
                    StaminaStat.level += 1;
                    break;
                case 2:
                    Agility.level += 1;
                    break;
                case 3:
                    Chin.level += 1;
                    break;
                default:
                    break;
            }
        }

        FillHealth();
        CurrentStamina = MaxStamina;
    }

    public List<Move> attacks = new List<Move>() { };

    [Serializable]
    public class Stat
    {
        public int level = 1;

        public int experience = 0;
        public int maxExperience => level * 10;
    }
}
