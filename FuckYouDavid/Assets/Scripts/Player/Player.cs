using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    public bool AllowedToMove = true;
    Movement mov => GetComponent<Movement>();

    void Update()
    {
        if (AllowedToMove) {
            mov.input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
}
