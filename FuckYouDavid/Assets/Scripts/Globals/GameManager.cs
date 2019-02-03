using System.Collections.Generic;
using UnityEngine;

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
        foreach (var obj in ChildrenToSpawnOnStart)
        {
            Instantiate(obj, transform.position, transform.rotation, transform);
            if (obj.name.Contains("OptionsBOX")) {
                obj.SetActive(false);
                MainManager.OptionsBOX=obj;
            }

        }

        if (GetComponent<Canvas>())
            GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
