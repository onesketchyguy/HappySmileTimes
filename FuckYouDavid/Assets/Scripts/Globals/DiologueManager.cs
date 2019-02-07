﻿using UnityEngine;
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
    public Text Dtext, NameDisplay;

    void Start()
    {
        ClearDialogueBox();
    }

    public void ClearDialogueBox()
    {
        if (DBOX != null)
        {
            GameManager.gameState = GameManager.GameState.Playing;
            DBOX.gameObject.SetActive(false);

            NameDisplay.text = " ";
            Dtext.text = " ";
        }
    }

    public void OpenDialogueBox()
    {
        if (DBOX != null)
        {
            GameManager.gameState = GameManager.GameState.InChat;
            DBOX.gameObject.SetActive(true);

            Dtext.text = " ";
            NameDisplay.text = " ";
        }
    }

    public void DisplayMessage(string message, float timeToDisplay)
    {
        OpenDialogueBox();

        Dtext.text = message;
        Invoke("ClearDialogueBox", timeToDisplay);
    }

    public void DisplayName(string Name)
    {
        OpenDialogueBox();
        NameDisplay.text = Name;
   
    }
    public void DisplayMessage(string message)
    {
        OpenDialogueBox();

        Dtext.text = message;
    }
}
