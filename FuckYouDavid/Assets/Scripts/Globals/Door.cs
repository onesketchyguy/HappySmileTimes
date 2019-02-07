using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string Key = "";

    [SerializeField] private Sprite[] sprites;

    private SpriteRenderer spriteRenderer => GetComponent<SpriteRenderer>() ?? gameObject.AddComponent<SpriteRenderer>();

    private BoxCollider2D boxCollider => GetComponent<BoxCollider2D>() ?? gameObject.AddComponent<BoxCollider2D>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Key != "")
            {
                Player player = collision.gameObject.GetComponent<Player>();

                if (player.inventory.keys.Contains(Key))
                {
                    OpenDoor();
                }
                else
                {
                    DiologueManager dManager = FindObjectOfType<DiologueManager>();

                    if (dManager)
                    {
                        dManager.DisplayMessage("Door Locked...", 1);
                    }
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

        SoundManager.Reference.PlayDoorSound(0);

        spriteRenderer.sprite = sprites[1];
    }

    public void CloseDoor()
    {
        boxCollider.isTrigger = false;

        SoundManager.Reference.PlayDoorSound(1);

        spriteRenderer.sprite = sprites[0];
    }
}