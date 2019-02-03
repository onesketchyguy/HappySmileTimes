using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAfterTime : MonoBehaviour
{
    [SerializeField] private float timeBeforeLoading = 3;

    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        Invoke("Load", timeBeforeLoading);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Load();
        }
    }

    private void Load()
    {
        if (sceneToLoad == "")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}