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
    public MainManager MM;
    public bool Main=false;
    public bool InChat;
  
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        DM = FindObjectOfType<DiologueManager>();
        MM = FindObjectOfType<MainManager>();

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
                CombatManager.gameState = CombatManager.GameState.InChat;
                Onchat();
            }
          
            
        }
    }

    public void Onchat() {
     
        DM.Dtext.text = DIALOGUE;
        DM.DBOX.gameObject.SetActive(true);

        if (Main == false)
        {
            Invoke("Leave", .5f);
        }
        else if (Main == true)
        {
            MM.ON();
        }
        if (Sales)
        {
            Invoke("Shop", .5f);
        }
    }


    public void Leave() {
        InChat = false;
        print("Leaving");
        CombatManager.gameState = CombatManager.GameState.Playing;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);
      
    }
    public void Shop()
    {
        CombatManager.gameState = CombatManager.GameState.InChat;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);
        if (SP != null) {
       
            SP.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CombatManager.gameState = CombatManager.GameState.Playing;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);

    }
}
