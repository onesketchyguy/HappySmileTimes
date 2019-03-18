using UnityEngine;

[CreateAssetMenu(menuName = "Create new move", fileName = "New move")]
public class Move : ScriptableObject
{
    public string description;

    public GameManager.States Effect = GameManager.States.Normal;

    public int power;

    internal int experience = -1;

    public int maxExperience => power * 5;

    /// <summary>
    /// Leave empty for no upgrades.
    /// </summary>
    public Move Upgrade;

    public int powerLevelRequired;
}
