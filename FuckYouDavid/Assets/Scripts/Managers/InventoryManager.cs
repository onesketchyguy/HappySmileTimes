using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public GameObject Panel,Moves,Bag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
            Panel.SetActive(GameManager.gameState==GameManager.GameState.InBag);      
    }

    public void PanelController(int Factor) {
        Panel.SetActive(Factor==1);
        Moves.SetActive(Factor == 3);
        Bag.SetActive(Factor == 2);


    }

}
