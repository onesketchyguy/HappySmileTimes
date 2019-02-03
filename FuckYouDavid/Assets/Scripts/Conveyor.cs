using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Vector2 dir = Vector2.right;
    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject Player = collision.gameObject;
            Player.GetComponent<Movement>().input = dir;
            CombatManager.gameState = CombatManager.GameState.OnConveyor;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject Player = collision.gameObject;
            CombatManager.gameState = CombatManager.GameState.Playing;
        }
    }
}