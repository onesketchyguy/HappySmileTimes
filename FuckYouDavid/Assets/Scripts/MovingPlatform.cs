using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 Right = Vector2.right;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameObject Player = collision.gameObject;
            Player.GetComponent<Movement>().input = Right;
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