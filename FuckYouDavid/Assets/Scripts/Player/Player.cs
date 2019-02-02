using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();

    [SerializeField] public CombatUniversals combattant;

    void Update()
    {
        if (AllowedToMove) {
            mov.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
