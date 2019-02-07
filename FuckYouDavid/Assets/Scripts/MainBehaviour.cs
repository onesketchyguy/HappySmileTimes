using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBehaviour : MonoBehaviour
{
    public DialogueManager DM;
    public string LevelToGo = "Main";
    public Talker T;

    // Start is called before the first frame update
    void Start()
    {
        T = FindObjectOfType<Talker>();
    }


    public void ON()
    {
        Debug.Log("On pressed");

        MainManager.instance.ON();

        MainManager.option_A += Yes_Selected;

        MainManager.option_B += No_Selected;
    }

    public void OFF()
    {
        MainManager.option_A -= Yes_Selected;

        MainManager.option_B -= No_Selected;

        Debug.Log("Off pressed");

        T.Leave();

        MainManager.instance.OFF();
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

        GameManager.gameState = GameManager.GameState.Playing;
        SceneManager.LoadScene(LevelToGo);
    }

    public void No_Selected()
    {
        OFF();
        //T.Leave();
    }

}
