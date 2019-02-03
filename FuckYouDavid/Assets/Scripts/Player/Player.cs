using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();
    public GameObject Inventory;

    [SerializeField] public CombatUniversals combattant;

    private void Start()
    {
        combattant.Inititialize();
        combattant.Name = "You";
    }

    void Update()
    {
        if (CombatManager.gameState == CombatManager.GameState.InCombat)
        {
            CombatManager.combatant_0 = combattant;
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            print("I");
            CombatManager.gameState = CombatManager.GameState.InChat;
            Inventory.SetActive(true);

        }
        AllowedToMove = CombatManager.gameState == CombatManager.GameState.Playing;

        if (AllowedToMove)
        {
            mov.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
