using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();
    public InventoryManager Inventory;

    [SerializeField] public CombatUniversals combattant;

    private void Start()
    {
        Inventory = GameObject.FindObjectOfType<InventoryManager>();
        combattant.Inititialize();
        combattant.Name = "You";
    }

    void Update()
    {
        if (combattant.isDead)
        {
            GameManager.instance.LoadScene("Splash");
        }

        if (GameManager.gameState == GameManager.GameState.InCombat)
        {
            CombatManager.combatant_0 = combattant;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            switch (GameManager.gameState)
            {
                case GameManager.GameState.Playing:
                    GameManager.gameState =( GameManager.GameState.InBag);
                    break;
                case GameManager.GameState.InCombat:
                    break;
                case GameManager.GameState.InBag:
                    GameManager.gameState = ( GameManager.GameState.Playing);
                    break;
              
                default:
                    break;
            }
         

        }

        AllowedToMove = GameManager.gameState == GameManager.GameState.Playing;

        if (AllowedToMove)
        {
            mov.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
