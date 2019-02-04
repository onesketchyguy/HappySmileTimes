using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string PlayerName = "";

    public GameObject namePanel;

    public enum GameState { Playing, InCombat, InBag, InChat, OnConveyor, Paused }

    public static GameState gameState = GameState.Playing;

    [SerializeField] private GameObject[] ChildrenToSpawnOnStart;

    public List<CombatUniversals.Move> moves = new List<CombatUniversals.Move>() { };

    public static GameManager instance;

    private void Awake()
    {
        if (instance != this && GameManager.instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        if (GetComponent<Canvas>())
            GetComponent<Canvas>().worldCamera = Camera.main;

        SceneManager.activeSceneChanged += sceneChanged;

        foreach (var obj in ChildrenToSpawnOnStart)
        {
            Instantiate(obj, transform.position, transform.rotation, transform);
        }

        if (MainManager.instance != null)
            MainManager.instance.OFF();

        InvokeRepeating("UpdateNameText", 0, 1f * Time.deltaTime);
    }

    public void LoadScene(string SceneToLoad)
    {
        if (SceneToLoad == "Main") PlayerName = "";

        SceneManager.LoadScene(SceneToLoad);
    }

    private void sceneChanged(Scene arg0, Scene arg1)
    {
        if (PlayerName == "")
        {
            //Open name dialogue
            InvokeRepeating("UpdateNameText", 0, 1f * Time.deltaTime);
        }

        gameState = GameState.Playing;

        if (MainManager.instance != null)
            MainManager.instance.OFF();

        DiologueManager.instance.ClearDialogueBox();

        if (GetComponent<Canvas>())
            GetComponent<Canvas>().worldCamera = Camera.main;
    }

    private void UpdateNameText()
    {
        namePanel.SetActive(true);

        gameState = GameState.Paused;

        foreach (var obj in namePanel.GetComponentsInChildren<Transform>())
        {
            Text text = obj.GetComponent<Text>();

            if (text != null)
            {
                if (text.name == "NameText")
                {
                    string textToUse = text.text;

                    textToUse += Input.inputString;

                    if (Input.GetKeyDown(KeyCode.Backspace))
                    {
                        string NewText = "";

                        char[] array = text.text.ToCharArray();
                        for (int i = 0; i < array.Length - 1; i++)
                        {
                            char character = array[i];

                            NewText += character;
                        }

                        textToUse = NewText;
                    }

                    text.text = textToUse;

                    PlayerName = textToUse;
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            namePanel.SetActive(false);

            gameState = GameState.Playing;

            CancelInvoke("UpdateNameText");
        }
    }
}
