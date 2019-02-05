using UnityEngine;

public class BedManager : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.combattant.CurrentHealth = player.combattant.MaxHealth;

            Debug.Log("Slept...");
        }
    }
}