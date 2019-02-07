using UnityEngine;

public class Talker : MonoBehaviour
{
    public DiologueManager DM;
    public string DIALOGUE,DIALOGUE2,Name;
    public bool Sales;
    public Inventory INV;
    public GameObject SP;
    public MainBehaviour MM => GetComponent<MainBehaviour>() ?? gameObject.AddComponent<MainBehaviour>();
    public bool Main=false;
    public bool HasItem;
    public bool InChat;
    public float TextTime = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                GameManager.gameState = GameManager.GameState.InChat;
                Onchat();
            }
        }
    }

    public void NewD()
    {
        DIALOGUE = DIALOGUE2;
    }

    public void Onchat()
    {
        if (DM == null)
        {
            DM = FindObjectOfType<DiologueManager>();
        }
        DM.NameDisplay.text = Name;
        DM.Dtext.text = DIALOGUE;
        DM.DBOX.gameObject.SetActive(true);

        if (HasItem==true) {
            //gives Item
          
            Player player = FindObjectOfType<Player>();
            player.inventory.keys.Add(INV.keys.ToArray()[0]);
            INV.Money += player.inventory.Money;
            HasItem = false;
            Invoke("NewD",1);
        }
        if (Main == false && Sales==false)
        {
            Invoke("Leave", TextTime);
        }
        else if (Main == true)
        {
            MM.ON();
        }
       else if (Sales==true)
        {
            Invoke("Shop", .5f);
        }
    }

    public void Leave()
    {
        InChat = false;

        Debug.Log("Leaving chat...");

        GameManager.gameState = GameManager.GameState.Playing;

        DiologueManager.instance.ClearDialogueBox();

        Fighter fightComponent = GetComponent<Fighter>();

        if (fightComponent)
        {
            fightComponent.StartFight();
        }
    }

    public void Shop()
    {
        print("in shop");
        GameManager.gameState = GameManager.GameState.InChat;

        DiologueManager.instance.ClearDialogueBox();

        if (SP != null) {
       
            SP.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Leave();
    }
}
