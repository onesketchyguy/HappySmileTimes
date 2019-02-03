using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, InCombat, InBag, InChat, OnConveyor, Paused }

    public static GameState gameState = GameState.Playing;

    [SerializeField] private GameObject[] ChildrenToSpawnOnStart;

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
        }
    }
}
