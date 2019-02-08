using UnityEngine;

public class BedManager : MonoBehaviour
{
    private Player player;

    private void Sleep(Player localPlayer)
    {
        localPlayer.combattant.CurrentHealth = localPlayer.combattant.MaxHealth;

        Debug.Log($"Slept... Player health restored to {localPlayer.combattant.CurrentHealth}/{localPlayer.combattant.MaxHealth}.");
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


        float timeToSleep = 2;

        GameManager.instance.TogglePanelOn(timeToSleep + 1f);

        DialogueManager.instance.DisplayMessage("zZzZzZz...", $"{player.combattant.Name}", timeToSleep);
    }

    public void No ()
    {
        DialogueManager.option_A -= Yes;

        DialogueManager.option_B -= No;

        DialogueManager.instance.ClearDialogueBox();

        DialogueManager.instance.SetButtonsActive(false);
    }
}