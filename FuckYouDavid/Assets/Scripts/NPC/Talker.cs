using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talker : MonoBehaviour
{

    public DiologueManager DM;
    public  Player player;
    public string DIALOGUE;
  
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
                    DM.Dtext.text = DIALOGUE;
                    DM.DBOX.gameObject.SetActive(true);

                }
            
        }
    }
}
