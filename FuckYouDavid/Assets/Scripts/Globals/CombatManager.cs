using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public enum GameState { Playing, InCombat, InBag, InChat, Paused }

    public static GameState gameState = GameState.Playing;

    public GameObject CombatScreen;

    private void Update()
    {
        CombatScreen.SetActive(gameState == GameState.InCombat);
    }
}