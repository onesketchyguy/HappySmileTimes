using UnityEngine;

public class Talker : MonoBehaviour
{
    public string[] dialogue;

    int currentSentence = 0;

    public string Name;
    public bool Sales;
    public Inventory inventory;
    public GameObject SP;
    public MainBehaviour MM => GetComponent<MainBehaviour>() ?? gameObject.AddComponent<MainBehaviour>();
    public bool Main=false;
    public bool HasItem;
    public bool InChat;
    public float TextTime = 1f;


    private void Start()
    {
        if (GetComponent<Fighter>()) {

            Name = GetComponent<Fighter>().combattant.Name;

        }


    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnChat();
            }
        }
    }

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

        if (dialogue.Length <= currentSentence + 1)
        {
            currentSentence = 0;
        }
        else
        {
            currentSentence++;
        }

        return text;
    }

    public void OnChat()
    {
        if (HasItem == true)
        {
            //gives Item
            FindObjectOfType<Player>().inventory += inventory;

            string itemsRetrieved = $"Got {inventory.Money}";

            foreach (var item in inventory.items.ToArray())
            {
                itemsRetrieved += $", {item.name}";
            }

            itemsRetrieved += ".";

            float timeToDisplayNotification = itemsRetrieved.ToCharArray().Length / 5;

            FindObjectOfType<DialogueManager>().DisplayMessage(itemsRetrieved, timeToDisplayNotification);

            inventory = new Inventory { };

            HasItem = false;

            Invoke("Leave", timeToDisplayNotification);

            return;
        }
        else 
        if (Main == true)
        {
            MM.ON();
        }
        else
        if (Sales == true)
        {
            Invoke("Shop", TextTime);
        }

        DialogueManager.instance.DisplayMessage(NewDialogue(), Name, TextTime);
    }

    public void Leave()
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
        print("in shop");

        DialogueManager.instance.ClearDialogueBox();

        if (SP != null)
        {
            GameManager.gameState = GameManager.GameState.InChat;

            SP.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Leave();
    }
}
