[System.Serializable]
public class CombatUniversals
{
    public string Name = "Nameless";

    public UnityEngine.Sprite Image;

    //Stats
    public int Stength = 1; //attack
    public int Agility = 1; //attackspeed
    public int Chin = 1; //defence
    public int StaminaStat = 1;

    // Main Values
    public int MaxHealth => 5 + (Chin * 5);
    public int MaxStamina => 5 + (StaminaStat * 5);
    public int CurrentStamina;
    public int CurrentHealth;

    public int PowerLevel => StaminaStat + Stength + Chin + Agility;

    public void Inititialize()
    {
        CurrentHealth = MaxHealth;
        CurrentHealth = MaxStamina;
    }

    public System.Collections.Generic.List<Move> attacks = new System.Collections.Generic.List<Move>()
    {
        new Move { name = "Punch", power = 5, powerType = Move.reliance.Strength }
    };

    [System.Serializable]
    public class Move
    {
        public string name;

        public string description;

        public enum reliance { Strength, Agility, Chin, Stamina }

        public reliance powerType;

        public int power;
    }
}
