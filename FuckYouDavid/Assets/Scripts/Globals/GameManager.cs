using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    }

    public void LoadScene(string SceneToLoad)
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    private void sceneChanged(Scene arg0, Scene arg1)
    {
        gameState = GameState.Playing;

        if (MainManager.instance != null)
            MainManager.instance.OFF();

        DiologueManager.instance.ClearDialogueBox();

        if (GetComponent<Canvas>())
            GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
