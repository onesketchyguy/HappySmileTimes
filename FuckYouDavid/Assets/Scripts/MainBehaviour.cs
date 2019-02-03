using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBehaviour : MonoBehaviour
{

    public DiologueManager DM;
    public string LevelToGo = "Main";
    public GameObject Options;
    public Talker T;

    // Start is called before the first frame update
    void Start()
    {
        T = FindObjectOfType<Talker>();

        if (Options == null)
        {
            Options = GameObject.Find("OptionsBOX");

            if (Options == null)
            {
                Debug.LogError("Unable to identify OptionsBox!");
            }
        }
    }

    public void ON()
    {
        Debug.Log("On pressed");

        Options.SetActive(true);

        MainManager.option_A += Yes_Selected;

        MainManager.option_B += No_Selected;
    }

    public void OFF()
    {
        MainManager.option_A -= Yes_Selected;

        MainManager.option_B -= No_Selected;

        Debug.Log("Off pressed");

        T.Leave();
        Options.SetActive(false);
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
