using System.Collections.Generic;
/// <summary>
/// Resources available to any given unit.
/// </summary>
[System.Serializable]
public class Inventory
{
    public  Dictionary<string, int> items = new Dictionary<string, int> { };

    public List<string> keys = new List<string> { };

    public float Money = 0;
}