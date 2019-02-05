using System.Collections.Generic;
/// <summary>
/// Resources available to any given unit.
/// </summary>
[System.Serializable]
public class Inventory
{
    public  Dictionary<string, ItemDefinition> items = new Dictionary<string, ItemDefinition> { };

    public List<string> keys = new List<string> { };

    public float Money = 0;
}

[System.Serializable]
public class ItemDefinition
{
    public UnityEngine.Sprite image;
    public int count;
}