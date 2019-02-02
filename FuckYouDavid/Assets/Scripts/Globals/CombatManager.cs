using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public enum GameState { Playing, InCombat, InBag, InChat, OnConveyor, Paused }

    public static GameState gameState = GameState.Playing;

    public GameObject CombatScreen;

    [SerializeField] private Image[] icons;

    [SerializeField] private Slider[] sliders;

    public static CombatUniversals combatant_0, combatant_1;

    private void Update()
    {
        CombatScreen.SetActive(gameState == GameState.InCombat);

        if (combatant_0 != null && combatant_1 != null)
        {
            icons[0].sprite = combatant_0.CombatSprite;

            icons[1].sprite = combatant_1.CombatSprite;
        }
    }
}