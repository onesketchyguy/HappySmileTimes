using UnityEngine;

public class Talker : MonoBehaviour
{
    public string[] dialogue;

   public int currentSentence = 0;

    public string Name;

    public bool Sales,Tourial;
       
    public Inventory inventory;

    public MainBehaviour MM => GetComponent<MainBehaviour>() ?? null;

    private bool Main => MM != null;

    private bool HasItem => inventory.items.Count > 0 || inventory.keys.Count > 0 || inventory.Money > 0;

    private bool InChat;

    /// <summary>
    /// Amount of time for the dialogue box to show.
    /// </summary>
    public float TextTime = 1f;

    public string NewDialogue()
    {
        string text = "";

        for (int i = 0; i < dialogue.Length; i++)
        {
            if (i == currentSentence)
            {
                text = dialogue[i];
            }
        }

        if (dialogue.Length <= currentSentence +1)
        {
            Invoke("OnExitChat", TextTime);
            currentSentence = 0;
           

        }
        else
        {
            currentSentence++;
        }

        return text;
    }
    public void Update()
    {
        if (InChat==true) {
            if (Input.GetKeyDown(KeyCode.E))
            {

                OnChat();
            }
        }
    }
    public void OnChat()
    {
        CheckName();
        if (Tourial)
        {
            GameManager.UpdateNameText = true;
            Tourial = false;
            OnExitChat();
            return;
        }

        if (HasItem == true&& Sales==false)
        {
            //gives Item
            string itemsRetrieved = CalculateItemsGot();

            FindObjectOfType<Player>().inventory += inventory;

            float timeToDisplayNotification = itemsRetrieved.ToCharArray().Length / 5;

            FindObjectOfType<DialogueManager>().DisplayMessage(itemsRetrieved, timeToDisplayNotification);

            Invoke("OnExitChat", timeToDisplayNotification);

            return;
        }
        else 
        if (Main == true)
        {
            MM.ON();
            DialogueManager.instance.DisplayMessage(NewDialogue(), Name );
            return;

        }
        DialogueManager.instance.DisplayMessage(NewDialogue(), Name, TextTime);

        Invoke("OnExitChat", TextTime);
        if (Sales == true)
        {
            Invoke("Shop", TextTime);
            return;
        }

    }

    private string CalculateItemsGot()
    {
        string itemsRetrieved = "Got";

        if (inventory.Money > 0)
        {
            itemsRetrieved += $"${inventory.Money}";
        }

        foreach (var item in inventory.items.ToArray())
        {
            itemsRetrieved += $" {item.name}";
        }

        foreach (var item in inventory.keys.ToArray())
        {
            itemsRetrieved += $" key to {item}";
        }

        itemsRetrieved += ".";


        return itemsRetrieved;
    }

    public void OnExitChat()
    {
        InChat = false;

        Debug.Log("Leaving chat...");

        DialogueManager.instance.ClearDialogueBox();

        Fighter fightComponent = GetComponent<Fighter>();

        if (fightComponent)
        {
            fightComponent.StartFight();
        }
    }

    public void Shop()
    {
        Debug.Log("Opening shop...");
        
        //DialogueManager.instance.ClearDialogueBox();
        ShopInventory.INV = inventory;
       // ShopInventory.OpenBag();
        GameManager.instance.ToggleShop(true);
        
    }

    private void CheckName()
    {
        Fighter fighter = GetComponent<Fighter>();

        if (fighter)
        {
            Name = fighter.combattant.Name;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("in");
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

            if (rigidBody)
            {
                rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }

            InChat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

            if (rigidBody)
            {
                rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            }

            OnExitChat();
        }
    }
}
