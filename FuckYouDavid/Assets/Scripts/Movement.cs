using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum faceDirection { Down, Left, Up, Right }

    internal faceDirection myFaceDirection = faceDirection.Down;

    [SerializeField] private float speed = 5;

    internal Vector2 input;

    private Rigidbody2D myRigidBody => GetComponent<Rigidbody2D>();

    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>();
    }

    void Update()
    {
        myFaceDirection = (input.x > 0 ? faceDirection.Right : input.y > 0 ? faceDirection.Up : input.x < 0 ? faceDirection.Left : input.y < 0 ? faceDirection.Down : myFaceDirection);

        myRigidBody.velocity = Vector3.MoveTowards(myRigidBody.velocity, (Vector3)(input * speed), 50 * Time.deltaTime);
    }

    public void RotateClockWise()
    {
        switch (myFaceDirection)
        {
            case faceDirection.Down:
                myFaceDirection = faceDirection.Left;
                break;
            case faceDirection.Left:
                myFaceDirection = faceDirection.Up;
                break;
            case faceDirection.Up:
                myFaceDirection = faceDirection.Right;
                break;
            case faceDirection.Right:
                myFaceDirection = faceDirection.Down;
                break;
            default:
                break;
        }
    }
}
