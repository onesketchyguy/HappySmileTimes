using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{

    public DiologueManager DM;
    public string LevelToGo;
    public GameObject Options;

    // Start is called before the first frame update
    void Start()
    {
        Options.SetActive(false);
    }
    public void ON() {
        Options.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes() {
        print("Yes");
        SceneManager.LoadScene(LevelToGo);
        

    }

    public void No() {
        print("No");
        CombatManager.gameState = CombatManager.GameState.Playing;
        Options.SetActive(false);
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);
    }

}
