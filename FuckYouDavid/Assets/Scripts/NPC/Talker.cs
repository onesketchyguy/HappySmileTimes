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
    public bool HasItem;
    public bool InChat;
    public float TextTime = 1f;
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
                GameManager.gameState = GameManager.GameState.InChat;
                Onchat();
            }
          
            
        }
    }
    public void NewD() {
        DIALOGUE = "Jeez man dont be a choosey begger";
    }
    public void Onchat() {
     
        DM.Dtext.text = DIALOGUE;
        DM.DBOX.gameObject.SetActive(true);
        if (HasItem==true) {
            //gives Item
            HasItem = false;
            Invoke("NewD",1);
        }
        if (Main == false)
        {
            Invoke("Leave", TextTime);
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
        GameManager.gameState = GameManager.GameState.Playing;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);
      
    }
    public void Shop()
    {
        GameManager.gameState = GameManager.GameState.InChat;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);
        if (SP != null) {
       
            SP.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.gameState = GameManager.GameState.Playing;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);

    }
}
