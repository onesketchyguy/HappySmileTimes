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
    public Text Dtext;

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
}
