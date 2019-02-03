using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiologueManager : MonoBehaviour
{
    public GameObject DBOX;
    public Text Dtext;

    void Start()
    {
        Dtext.text = " ";
        if (DBOX!=null) {
            DBOX.gameObject.SetActive(false);
            print("Not null");
        }
    }

    void Update()
    {

        
    }
}
