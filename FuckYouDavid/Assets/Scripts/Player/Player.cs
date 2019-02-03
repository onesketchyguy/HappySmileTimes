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

        combattant.Name = "You";
    }

    void Update()
    {
        if (GameManager.gameState == GameManager.GameState.InCombat)
        {
            CombatManager.combatant_0 = combattant;
        }

        AllowedToMove = GameManager.gameState == GameManager.GameState.Playing;

        if (AllowedToMove)
        {
            mov.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
