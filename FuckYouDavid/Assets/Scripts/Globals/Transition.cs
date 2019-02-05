using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Transition : MonoBehaviour
{
    public string LevelToLoad = "";

    private string levelToLoad => LevelToLoad == "" ? "Main" : LevelToLoad;

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Bring up a travel dialogue
            GameManager.instance.LoadScene(levelToLoad);
        }
    }
}
