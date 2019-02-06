using UnityEngine;
using UnityEngine.UI;

public class DiologueManager : MonoBehaviour
{
    public static DiologueManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject DBOX;
    public Text Dtext, Dtext2;

    void Start()
    {
        ClearDialogueBox();
    }

    public void ClearDialogueBox()
    {
        if (DBOX != null)
        {
            DBOX.gameObject.SetActive(false);

            Dtext.text = " ";
        }
    }

    public void OpenDialogueBox()
    {
        if (DBOX != null)
        {
            DBOX.gameObject.SetActive(true);

            Dtext.text = " ";
        }
    }

    public void DisplayMessage(string message, float timeToDisplay)
    {
        OpenDialogueBox();

        Dtext.text = message;

        Invoke("ClearDialogueBox", timeToDisplay);
    }

    public void DisplayMessage(string message)
    {
        OpenDialogueBox();

        Dtext.text = message;
    }
}
