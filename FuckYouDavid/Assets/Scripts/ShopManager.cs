using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{

    public Player player;
    public Text Costs;
    public GameObject SP;




    // Start is called before the first frame update
    void Start()
    {     
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Leave() {
        SP.SetActive(false);
        GameManager.gameState = GameManager.GameState.Playing;
    }

    public void Burger()
    {
        // if (Player.Money>=10) {

        //   }
        print("pressed yum yum");

    }



}
