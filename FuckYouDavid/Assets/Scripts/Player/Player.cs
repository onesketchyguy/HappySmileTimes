using UnityEngine;
using DavidRules;
[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public Inventory inventory;

    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();

    [SerializeField] public CombatUniversals combattant = new CombatUniversals { };

    private void Start()
    {
        if (GameManager.instance.player != null)
        {
            if (GameManager.instance.player.inventory != null)
                inventory = GameManager.instance.player.inventory;

            if (GameManager.instance.player.combattant != null)
                combattant = GameManager.instance.player.combattant;
        }

        if (combattant.Class == CombatUniversals.CLASSTYPE.Weak)
        {
            combattant.Class = GameManager.playerClass;
        }

        combattant.Inititialize();
    }

    void Update()
    {
        combattant.Name = GameManager.PlayerName;

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

        GameManager.instance.player = this;
    }
}
