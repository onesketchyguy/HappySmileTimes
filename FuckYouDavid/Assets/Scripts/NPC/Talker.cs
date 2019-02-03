using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{

    public DiologueManager DM;
    public  Player player;
    public string DIALOGUE;
    public bool Sales;
    public GameObject SP;
  
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        DM = FindObjectOfType<DiologueManager>();
   
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Player")
            {
            if (Input.GetKeyDown(KeyCode.E))
            {
                print("sdasda");
                CombatManager.gameState = CombatManager.GameState.InChat;
                DM.Dtext.text = DIALOGUE;
                DM.DBOX.gameObject.SetActive(true);
                Invoke("Leave", .5f);

                if (Sales) {
                    Invoke("Shop", .5f);
                  
                 
                }
            }
           
            
        }
    }
    public void Leave() {
        CombatManager.gameState = CombatManager.GameState.Playing;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);
    }
    public void Shop()
    {
        if (SP != null) {
            SP.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CombatManager.gameState = CombatManager.GameState.Playing;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);

    }
}
