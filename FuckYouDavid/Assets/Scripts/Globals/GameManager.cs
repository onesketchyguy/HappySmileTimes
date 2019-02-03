using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance
    private GameManager Instance => Instance == null ? this : Instance;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    [SerializeField] private GameObject[] childrenToCreate;

    public enum GameState { Playing, InCombat, InBag, InChat, OnConveyor, Paused }

    public static GameState gameState = GameState.Playing;

    private void Start()
    {
        foreach (var obj in childrenToCreate)
        {
            Instantiate(obj, transform.position, transform.rotation, transform);
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
