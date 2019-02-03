using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    public DiologueManager DM;
    public string LevelToGo;
    public GameObject Options;
    public Talker T;

    // Start is called before the first frame update
    void Start()
    {
     
        T = FindObjectOfType<Talker>();
    }
    public void ON() {
 
        Options.SetActive(true);

    }
    public void OFF()
    {
        Debug.Log("Play");
        T.Leave();
        Options.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes() {
        print("Yes");
        CombatManager.gameState = CombatManager.GameState.Playing;
        SceneManager.LoadScene(LevelToGo);
        

    }

    public void No() {
        OFF();
        //T.Leave();
    }

}
