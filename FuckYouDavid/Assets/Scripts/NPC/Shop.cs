﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{

    public DialogueManager DM;
    public  Player player;
    public string DIALOGUE;
  
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        DM = FindObjectOfType<DialogueManager>();
   
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
                player.AllowedToMove = false;
                DM.Dtext.text = DIALOGUE;
                DM.DBOX.gameObject.SetActive(true);
                Invoke("Leave", 1);
            }

        }
    }
    public void Leave() {
        player.AllowedToMove = true;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player.AllowedToMove = true;
        DM.Dtext.text = "";
        DM.DBOX.gameObject.SetActive(false);

    }
}
