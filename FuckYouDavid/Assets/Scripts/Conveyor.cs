using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Vector2 dir = Vector2.right;
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;

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