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
            print(item.name);
            invItem.AddComponent<Image>().sprite = item.image;
            invItem.AddComponent<Button>().onClick.AddListener(delegate { Bought(); });
        }
        
    }
    public void Bought() {
        Player player = FindObjectOfType<Player>();
       // player.inventory.Money-=;
    
    }




    public void RemoveItemFromBag(ItemDefinition itemToRemove)
    {
        Player player = FindObjectOfType<Player>();

                player.inventory.items.Remove(itemToRemove);

                foreach (var item in INVContent.GetComponentsInChildren<Transform>())
                {
                    if (item == INVContent.transform)
                        continue;

                    if (item.name == itemToRemove.name)
                    {
                        player.combattant.CurrentHealth += (int)(10 * itemToRemove.ChanceOfEffect);

                        DialogueManager.instance.DisplayMessage($"Used {itemToRemove.name}.", 1);

                        itemToRemove.count -= 1;

                        if (itemToRemove.count <= 0)
                        {
                            Destroy(item.gameObject);
                        }
                    }   
            else
            {
                DialogueManager.instance.DisplayMessage("You can't use that here!", 1);
            }
        }
     
    }
}