using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    public float rotationSpeed = 0;

    private float lastRotation;

    [SerializeField] public CombatUniversals combattant;

    [SerializeField] private bool attackOnSight => !GetComponent<Talker>();

    Movement mov => GetComponent<Movement>();

    [SerializeField] private float sightRange = 5;

    [SerializeField] private LayerMask Collisions;

    [SerializeField] private GameObject SeenAnimObject;

    Vector2 sightPosStart => transform.position + (getFaceDirection() * 0.5f);

    Vector2 sightPosEnd => transform.position + (getFaceDirection() * sightRange);

    Vector3 getFaceDirection ()
    {
        Vector2 dir = Vector2.zero;

        switch (mov.myFaceDirection)
        {
            case Movement.faceDirection.Down:
                dir = Vector2.down;
                break;
            case Movement.faceDirection.Left:
                dir = Vector2.left;
                break;
            case Movement.faceDirection.Up:
                dir = Vector2.up;
                break;
            case Movement.faceDirection.Right:
                dir = Vector2.right;
                break;
            default:
                break;
        }

        return dir;
    }

    bool PlayerSeen = false;

    RaycastHit2D sightHit => (Physics2D.Linecast(sightPosStart, sightPosEnd, Collisions));

    private void Start()
    {
        combattant.Inititialize();

        SeenAnimObject.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.gameState != GameManager.GameState.Playing && GameManager.gameState != GameManager.GameState.InChat)
            return;

        if (combattant.isDead)
        {
            //Open inventory added notification

            FindObjectOfType<Player>().inventory += inventory;

            string itemsRetrieved = $"Got {inventory.Money}";

            foreach (var item in inventory.items.ToArray())
            {
                itemsRetrieved += $", {item.name}";
            }

            itemsRetrieved += ".";

            float timeToDisplayNotification = itemsRetrieved.ToCharArray().Length/2;

            FindObjectOfType<DiologueManager>().DisplayMessage(itemsRetrieved, timeToDisplayNotification);

            Destroy(gameObject);
        }

        if (rotationSpeed > 0 && PlayerSeen == false)
        {
            if (lastRotation < Time.time)
            {
                Rotate();
            }
        }

        Debug.DrawLine(sightPosStart, sightPosEnd, Color.red);

        if (sightHit.transform != null)
        {
            if (sightHit.transform.tag == "Player")
            {
                CombatManager.combatant_1 = combattant;

                if (PlayerSeen == false)
                {
                    PlayerSeen = true;

                    GameManager.gameState = GameManager.GameState.InChat;

                    SeenAnimObject.SetActive(true);

                    SoundManager.Reference.PlayNotificationSound();
                }
            }
        }

        if (PlayerSeen && !SeenAnimObject.activeSelf)
        {
            if (attackOnSight)
            {
                Debug.Log("Commence to battling!");

                StartFight();
            }
            else
            {
                Debug.Log("Commence to chatting!");

                //Commence dialogue.

                Talker fightComponent = GetComponent<Talker>();

                if (fightComponent)
                {
                    fightComponent.Onchat();
                }
            }
        }
    }

    private void Rotate()
    {
        mov.RotateClockWise();

        lastRotation = Time.time + rotationSpeed;
    }

    public void StartFight()
    {
        GameManager.gameState = GameManager.GameState.InCombat;

        PlayerSeen = false;
    }
}
