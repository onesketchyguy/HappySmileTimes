using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();

    [SerializeField] public CombatUniversals combattant;

    private void Start()
    {
        combattant.Inititialize();
    }

    void Update()
    {
        if (CombatManager.gameState == CombatManager.GameState.InCombat)
        {
            CombatManager.combatant_0 = combattant;
        }

        AllowedToMove = CombatManager.gameState == CombatManager.GameState.Playing;

        if (AllowedToMove)
        {
            mov.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
