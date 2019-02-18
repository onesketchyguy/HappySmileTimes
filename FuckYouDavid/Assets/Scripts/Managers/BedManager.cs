using UnityEngine;

public class BedManager : MonoBehaviour
{
    public Player player;

    float timeToSleep = 2;

    public void Sleep(Player localPlayer)
    {
        GameManager.playerCombat.FillHealth();

        Vector2[] lineIn = new Vector2[] { transform.position + Vector3.up * 0.5f, transform.position + Vector3.right * 0.5f, transform.position + Vector3.down * 0.5f, transform.position + Vector3.left * 0.5f };

        Vector2[] lineOut = new Vector2[] { transform.position + Vector3.up, transform.position + Vector3.right, transform.position + Vector3.down, transform.position + Vector3.left };

        Vector3 spawnPoint = transform.position;

        for (int i = 0; i < lineOut.Length; i++)
        {
            Vector2 startPoint = lineIn[i];
            Vector2 hitPoint = lineOut[i];

            RaycastHit2D hit2D = Physics2D.Linecast(startPoint, hitPoint, LayerMask.NameToLayer("Default"));

            if (hit2D.transform == null)
            {
                spawnPoint = hitPoint;
            }
        }


        Invoke("showRestedMessage", timeToSleep + 1f);
    }

    void showRestedMessage()
    {
        DialogueManager.instance.DisplayMessage("Health restored!", $"{GameManager.playerCombat.Name}", 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Ask if the player wants to sleep.

            player = collision.gameObject.GetComponent<Player>();

            DialogueManager.instance.DisplayMessage("Sleep until healed?");

            DialogueManager.instance.SetButtonsActive(true);

            DialogueManager.option_A += Yes;

            DialogueManager.option_B += No;
        }
    }

    public void Yes ()
    {
        Sleep(player);

        DialogueManager.option_A -= Yes;

        DialogueManager.option_B -= No;

        DialogueManager.instance.SetButtonsActive(false);

        GameManager.instance.TogglePanelOn(timeToSleep + 1f);

        DialogueManager.instance.DisplayMessage("...zZzZzZz...", $"{GameManager.playerCombat.Name}", timeToSleep);
    }

    public void No ()
    {
        DialogueManager.option_A -= Yes;

        DialogueManager.option_B -= No;

        DialogueManager.instance.ClearDialogueBox();

        DialogueManager.instance.SetButtonsActive(false);
    }
}