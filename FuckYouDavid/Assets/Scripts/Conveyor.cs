using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Vector2 dir = Vector2.right;

    private void Start()
    {
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();

        boxCollider.isTrigger = true;

        boxCollider.size = new Vector2(0.8f, 0.8f);

        GetComponent<SpriteRenderer>().sortingOrder = -10;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject Player = collision.gameObject;
            Player.GetComponent<Movement>().input = dir;
            GameManager.gameState = GameManager.GameState.OnConveyor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject Player = collision.gameObject;
            GameManager.gameState = GameManager.GameState.Playing;
        }
    }
}