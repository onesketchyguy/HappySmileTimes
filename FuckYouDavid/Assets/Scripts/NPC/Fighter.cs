using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float rotationSpeed = 0;

    private float lastRotation;

    [SerializeField] public CombatUniversals combattant;

    [SerializeField] private bool attackOnSight = false;

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
        if (combattant.isDead)
        {
            Destroy(gameObject);
        }

        if (rotationSpeed > 0)
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

                    rotationSpeed = 0;

                    GameManager.gameState = GameManager.GameState.Playing;

                    SeenAnimObject.SetActive(true);
                }
            }
        }

        if (PlayerSeen && !SeenAnimObject.activeSelf)
        {
            if (attackOnSight)
            {
                Debug.Log("Commence to battling!");

                GameManager.gameState = GameManager.GameState.InCombat;
            }
            else
            {
                Debug.Log("Commence to chatting!");

                //Commence dialogue.

                //GameManager.gameState = GameManager.GameState.InCombat;
            }
        }
    }

    private void Rotate()
    {
        mov.RotateClockWise();

        lastRotation = Time.time + rotationSpeed;
    }
}
