using System.Collections.Generic;
/// <summary>
/// Items available.
/// </summary>
[System.Serializable]
public class Inventory
{
    public  List <ItemDefinition> items = new List<ItemDefinition> { };

    public List<string> keys = new List<string> { };

    public float Money = 0;

    public static Inventory operator + (Inventory a, Inventory b)
    {
        foreach (var item in b.items) { a.items.Add(item); }

        foreach (var key in b.keys) { a.keys.Add(key); }

        a.Money += b.Money;

        b.keys = new List<string> { };

        b.items = new List<ItemDefinition> { };

        b.Money = 0;

        return a;
    }
}
/// <summary>
/// Defines what an item is.
/// </summary>
[System.Serializable]
public class ItemDefinition
{
    public string name;

    public UnityEngine.Sprite image;

    public int count;

    public GameManager.States Effect;

    public float value;

    /// <summary>
    /// Chance of effect.
    /// Range of 0-1
    /// if 0 no chance, if 1 always succeed.
    /// </summary>
    public float ChanceOfEffect = 1;
}