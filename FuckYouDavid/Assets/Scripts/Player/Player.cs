using UnityEngine;
using DavidRules;
[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public Inventory myInventory;

    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();

    [SerializeField] public CombatUniversals combattant;

    private void Start()
    {
        S.p("sadas" );
        combattant.Inititialize();
        combattant.Name = GameManager.PlayerName;
    }

    void Update()
    {
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
