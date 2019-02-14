using UnityEngine;
using DavidRules;
[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public Inventory inventory;

    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();
    public GameObject Spawn;
    public BedManager bedM;

    [SerializeField] public CombatUniversals combattant = new CombatUniversals { };

    private void Start()
    {
        bedM = FindObjectOfType<BedManager>();
    
        if (GameManager.player != null)
        {
            if (GameManager.player.inventory != null)
                inventory = GameManager.player.inventory;

            if (GameManager.player.combattant != null)
                combattant = GameManager.player.combattant;
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

        GameManager.player = this;
    }

    public void Respawn() {
        bedM.Sleep(this);
        gameObject.transform.position = Spawn.transform.position;
    }
}
