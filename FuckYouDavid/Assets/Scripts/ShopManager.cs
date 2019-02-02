using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{

    public Player player;
    public Text Costs;




    // Start is called before the first frame update
    void Start()
    {     
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

  

}
