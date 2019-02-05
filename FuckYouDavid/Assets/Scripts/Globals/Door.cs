using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string Key = "";

    private BoxCollider2D boxCollider => GetComponent<BoxCollider2D>() ?? gameObject.AddComponent<BoxCollider2D>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Key != "")
            {
                Player player = collision.gameObject.GetComponent<Player>();

                if (player.myInventory.keys.Contains(Key))
                {
                    OpenDoor();
                }
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        boxCollider.isTrigger = true;
    }

    public void CloseDoor()
    {
        boxCollider.isTrigger = false;
    }
}