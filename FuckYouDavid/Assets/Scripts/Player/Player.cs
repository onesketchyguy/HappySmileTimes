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

    private void Start()
    {
        bedM = FindObjectOfType<BedManager>();
    
        if (GameManager.player != null)
        {
            if (GameManager.player.inventory != null)
                inventory = GameManager.player.inventory;
        }

        GameManager.playerCombat.Inititialize();

        GameManager.playerCombat.Image = GameManager.instance.playerIcon;
    }

    void Update()
    {
        GameManager.player = this;

        GameManager.playerCombat.Name = GameManager.PlayerName;

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

    public void Respawn()
    {
        bedM.Sleep(this);
        gameObject.transform.position = Spawn.transform.position;
    }
}
