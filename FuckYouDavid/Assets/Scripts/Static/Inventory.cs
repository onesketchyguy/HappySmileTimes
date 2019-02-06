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
        Inventory invToReturn = new Inventory { };

        foreach (var item in a.items) { invToReturn.items.Add(item); }

        foreach (var item in b.items) { invToReturn.items.Add(item); }

        foreach (var key in a.keys) { invToReturn.keys.Add(key); }

        foreach (var key in b.keys) { invToReturn.keys.Add(key); }

        invToReturn.Money = (a.Money + b.Money);

        return invToReturn;
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

    /// <summary>
    /// Chance of effect.
    /// Range of 0-1
    /// if 0 no chance, if 1 always succeed.
    /// </summary>
    public float ChanceOfEffect = 1;
}