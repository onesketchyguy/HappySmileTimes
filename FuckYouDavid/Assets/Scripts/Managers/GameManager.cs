using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static string PlayerName = "";

    internal static Player player = null;

    public Sprite playerIcon;

    public System.Collections.Generic.List<Move> attacks = new System.Collections.Generic.List<Move>() { };

    internal static CombatUniversals playerCombat = new CombatUniversals()
    {
        Strength = new CombatUniversals.Stat { level = -1, experience = -1 },
        Agility = new CombatUniversals.Stat { level = -1, experience = -1 },
        Chin = new CombatUniversals.Stat { level = -1, experience = -1 },
        StaminaStat = new CombatUniversals.Stat { level = -1, experience = -1 }
    };

    public GameObject namePanel;

    public GameObject Panel;
 
    public enum States { Normal, GrossOut, Burn, Freeze, Paralysis, Poison, Confusion, Heal, Taunt, Protection }

    public enum GameState { Playing, InCombat, InBag, InChat, OnConveyor, InShop, Paused }

    public static GameState gameState = GameState.Playing;

    [SerializeField] private GameObject[] ChildrenToSpawnOnStart;

    public static GameManager instance;

    public static bool UpdateNameText = false;

    private GameObject Shop;

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
        print("psdads");
     //   Music.audioSource.clip = Music.Songs[0];
    //    Music.audioSource.Play();
        if (GetComponent<Canvas>())
            GetComponent<Canvas>().worldCamera = Camera.main;

        SceneManager.activeSceneChanged += sceneChanged;

        foreach (var obj in ChildrenToSpawnOnStart)
        {
            bool skipItem = false;

            foreach (var child in GetComponentsInChildren<Transform>())
            {
                if (child.name == obj.name) skipItem = true;

                if (obj.name.Contains("ShopPanel"))
                {
                    Shop = child.gameObject;
                }
            }

            if (skipItem)
                continue;

            GameObject thing = Instantiate(obj, transform.position, transform.rotation, transform) as GameObject;

            if (thing.name.Contains("ShopPanel"))
            {
                Shop = thing;
            }
        }

        if (Shop != gameObject)
        {
            ToggleShop(false);
        }

        if (DialogueManager.instance != null)
            DialogueManager.instance.SetButtonsActive(false);

        UpdateNameText = false;
    }

    public void TogglePanelOff()
    {
        Panel.SetActive(false);
    }

    public void TogglePanelOn(float timeToStayActive)
    {
        Panel.SetActive(true);

        Invoke("TogglePanelOff", timeToStayActive);
    }

    private void Update()
    {
        if (UpdateNameText == true)
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

            if (Input.GetKeyDown(KeyCode.Return) && PlayerName != "")
            {
                namePanel.SetActive(false);

                gameState = GameState.Playing;

                UpdateNameText = false;
            }
        }
    }

    public void LoadScene(string SceneToLoad)
    {
        if (SceneToLoad == "Main") PlayerName = "";
        SceneManager.LoadScene(SceneToLoad);
    }

    private void sceneChanged(Scene arg0, Scene arg1)
    {
        gameState = GameState.Playing;

        DialogueManager.instance.SetButtonsActive(false);

        DialogueManager.instance.ClearDialogueBox();

        if (GetComponent<Canvas>())
            GetComponent<Canvas>().worldCamera = Camera.main;
    }



    public void ToggleShop(bool active)
    {
        Shop.SetActive(active);

        if (active)
        {
            gameState = GameState.InShop;
        }
        else
        {
            gameState = GameState.Playing;
        }
    }
}
