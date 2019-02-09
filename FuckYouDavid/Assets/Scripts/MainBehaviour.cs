using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBehaviour : MonoBehaviour
{
    public string LevelToGo = "Main";
    public Talker TalkerComponent => GetComponent<Talker>() ?? gameObject.AddComponent<Talker>();

    public void ON()
    {
        Debug.Log("On pressed");

        DialogueManager.instance.SetButtonsActive(true);

        DialogueManager.option_A += Yes_Selected;

        DialogueManager.option_B += No_Selected;
    }

    public void OFF()
    {
        DialogueManager.option_A -= Yes_Selected;

        DialogueManager.option_B -= No_Selected;

        Debug.Log("Off pressed");

        TalkerComponent.OnExitChat();

        DialogueManager.instance.SetButtonsActive(false);
    }


    public void Yes_Selected()
    {
        print("Yes");

        if (LevelToGo == "Quit" || LevelToGo == "quit")
        {
            Debug.Log("Quiting application...");
            Application.Quit();

            return;
        }

        if (LevelToGo == "Main")
        {
            Debug.LogError("Level to load = Main.");

            Debug.Log("Quiting application...");
            Application.Quit();

            return;
        }
        OFF();
        GameManager.gameState = GameManager.GameState.Playing;
        SceneManager.LoadScene(LevelToGo);
    }

    public void No_Selected()
    {
        OFF();
    }
}
