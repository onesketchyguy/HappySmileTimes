using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public GameManager GM;
    public DiologueManager DM;
    public MovementManager Player;
    public string DIALOGUE;
    public bool AttackOnSight;
    public bool Chalengable;
    public bool Talk;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<MovementManager>();
        DM = FindObjectOfType<DiologueManager>();
        GM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (AttackOnSight) {
         
            if (collision.gameObject.tag == "Player")
            {
                if (Input.anyKey) {
                    DM.Dtext.text = DIALOGUE;
                    DM.DBOX.gameObject.SetActive(true);
                    Player.fighting = true;
                    GM.Invoke("Battle", 1);
                }
                
            }

        }

       else if (Chalengable) {
          
            if (collision.gameObject.tag == "Player")
            {
           
                if (Input.GetKeyDown(KeyCode.E))
                {     
                    DM.Dtext.text = DIALOGUE;
                    DM.DBOX.gameObject.SetActive(true);
                    Player.fighting = true;
                    GM.Invoke("Battle", 1);

                }
         
            }
        }
        else if (Talk)
        {

            if (collision.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {

                    DM.Dtext.text = DIALOGUE;
                    DM.DBOX.gameObject.SetActive(true);
                    Player.fighting = true;
                    Player.Invoke("Win", 1);
                }
            }
        }
    }
}
