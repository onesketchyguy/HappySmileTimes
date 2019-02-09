using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopInventory : MonoBehaviour
{
    public GameObject INVContent;
    public static Inventory INV;

    void Update()
    {
       
    }
    public void Start()
    {
        OpenBag();
    }

    public  void OpenBag()
    {
        //OpenPlayers bag
        Debug.Log("Attempting to open bag.");
    
        foreach (var item in INVContent.GetComponentsInChildren<Transform>())
        {
            if (item == INVContent.transform)
                continue;

            Destroy(item.gameObject);
        }

     
        foreach (var item in INV.items.ToArray())
        { 
            GameObject invItem = Instantiate(new GameObject(), INVContent.transform) as GameObject;
            invItem.name = item.name;
           // print(item.name);
            invItem.AddComponent<Image>().sprite = item.image;
            invItem.AddComponent<Button>().onClick.AddListener(delegate { Bought(item); });
        }
        
    }

    public void Bought(ItemDefinition itemToAdd)
    {
        Player player = FindObjectOfType<Player>();
        if (player.inventory.Money >= itemToAdd.value) {

            print("this works");
            foreach (var item in INVContent.GetComponentsInChildren<Transform>())
            {
                if (item == INVContent.transform)
                    continue;

                if (item.name == itemToAdd.name)
                {

                    player.inventory.items.Add(itemToAdd);

                }
            }
            }
            else if (player.inventory.Money < itemToAdd.value)
            {
            print("Poor");
                DialogueManager.instance.DisplayMessage("You are to poor!", 1);
            }
        }
     
    }
