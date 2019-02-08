using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

    public GameObject Panel, Moves, Bag, INVPanel, INVContent;

    void Update()
    {
        Panel.SetActive(GameManager.gameState == GameManager.GameState.InBag);   
    }

    public void PanelController(int Factor)
    {
        Panel.SetActive(Factor==1);
        Bag.SetActive(Factor == 2);
        Moves.SetActive(Factor == 3);

        if (Bag.activeSelf)
        {
            OpenBag();
        }
    }

    public void OpenBag()
    {
        //OpenPlayers bag

        Debug.Log("Attempting to open bag.");

        Panel.SetActive(!INVPanel.activeSelf);

        foreach (var item in INVContent.GetComponentsInChildren<Transform>())
        {
            if (item == INVContent.transform)
                continue;

            Destroy(item.gameObject);
        }

        Player player = FindObjectOfType<Player>();

        if (player)
        {
            foreach (var item in player.inventory.items.ToArray())
            {
                GameObject invItem = Instantiate(new GameObject(), INVContent.transform) as GameObject;

                invItem.name = item.name;

                invItem.AddComponent<Image>().sprite = item.image;

                invItem.AddComponent<Button>().onClick.AddListener(delegate { RemoveItemFromBag(item); });
            }
        }
        else
        {
            INVPanel.SetActive(false);
        }
    }

    public void RemoveItemFromBag(ItemDefinition itemToRemove)
    {
        Player player = FindObjectOfType<Player>();

        if (player)
        {
            player.inventory.items.Remove(itemToRemove);

            foreach (var item in INVContent.GetComponentsInChildren<Transform>())
            {
                if (item == INVContent.transform)
                    continue;

                if (item.name == itemToRemove.name)
                    Destroy(item.gameObject);
            }
        }
        else
        {
            INVPanel.SetActive(false);
        }
    }
}