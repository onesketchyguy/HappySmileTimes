using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public Player player;
    public Text Costs;

    void Start()
    {     
        player = FindObjectOfType<Player>();
    }

    public void Leave()
    {
        GameManager.instance.ToggleShop(false);
    }

    public void Burger()
    {
        if (player.inventory.Money >= 10)
        {
            print("pressed yum yum");
        }
    }
}
