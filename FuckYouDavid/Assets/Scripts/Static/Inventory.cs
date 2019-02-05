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
}