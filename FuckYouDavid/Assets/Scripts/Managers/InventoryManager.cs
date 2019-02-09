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
            bool use = false;

            switch (itemToRemove.Effect)
            {
                case GameManager.States.Normal:
                    break;
                case GameManager.States.GrossOut:
                    break;
                case GameManager.States.Burn:
                    break;
                case GameManager.States.Freeze:
                    break;
                case GameManager.States.Paralysis:
                    break;
                case GameManager.States.Poison:
                    break;
                case GameManager.States.Confusion:
                    break;
                case GameManager.States.Heal:
                    use = true;
                    break;
                case GameManager.States.Taunt:
                    break;
                case GameManager.States.Protection:
                    break;
                default:
                    break;
            }

            if (use == true)
            {
                player.inventory.items.Remove(itemToRemove);

                foreach (var item in INVContent.GetComponentsInChildren<Transform>())
                {
                    if (item == INVContent.transform)
                        continue;

                    if (item.name == itemToRemove.name)
                    {
                        player.combattant.CurrentHealth += (int) (10 * itemToRemove.ChanceOfEffect);

                        DialogueManager.instance.DisplayMessage($"Used {itemToRemove.name}.", 1);

                        itemToRemove.count -= 1;

                        if (itemToRemove.count <= 0)
                        {
                            Destroy(item.gameObject);
                        }
                    }
                }
            }
            else
            {
                DialogueManager.instance.DisplayMessage("You can't use that here!", 1);
            }
        }
        else
        {
            INVPanel.SetActive(false);
        }
    }
}