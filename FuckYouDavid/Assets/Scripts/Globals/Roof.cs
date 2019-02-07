using UnityEngine;
using UnityEngine.Tilemaps;

public class Roof : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRoom = false;
        }
    }

    bool playerInRoom = false;

    private void Update()
    {
        GetComponent<TilemapRenderer>().enabled = !playerInRoom;
    }
}