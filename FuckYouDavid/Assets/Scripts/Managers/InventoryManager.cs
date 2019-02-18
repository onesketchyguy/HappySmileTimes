using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject parent;

    public GameObject Menu, Moves, Bag, INVPanel, INVContent, OptionsPanel;

    [Space]

    [SerializeField] private Text experienceText;
    [SerializeField] private Text Money;

    [SerializeField] private Text StrengthText, StaminaText, AgilityText, ChinText;

    [SerializeField] private Button StrengthButton, StaminaButton, AgilityButton, ChinButton;
    private void Update()
    {
        parent.SetActive(GameManager.gameState == GameManager.GameState.InBag);

        if (Menu.activeSelf == true)
        {
            Player player = FindObjectOfType<Player>();

            StrengthText.text = $"STR {player.combattant.Strength.level} : {player.combattant.Strength.experience}/{player.combattant.Strength.maxExperience}";

            StaminaText.text = $"STA {player.combattant.StaminaStat.level}: {player.combattant.StaminaStat.experience}/{player.combattant.StaminaStat.maxExperience}";

            AgilityText.text = $"AGL {player.combattant.Agility.level}: {player.combattant.Agility.experience}/{player.combattant.Agility.maxExperience}";

            ChinText.text = $"CHIN {player.combattant.Chin.level}: {player.combattant.Chin.experience}/{player.combattant.Chin.maxExperience}";

            StrengthButton.gameObject.SetActive(0 < player.combattant.experience);

            StaminaButton.gameObject.SetActive(0 < player.combattant.experience);

            AgilityButton.gameObject.SetActive(0 < player.combattant.experience);

            ChinButton.gameObject.SetActive(0 < player.combattant.experience);

            experienceText.text = $"XP: {player.combattant.experience}";
            Money.text = $"Money: {player.inventory.Money}";
        }
    }

    public void PanelController(int Factor)
    {
        Menu.SetActive(Factor==1);
        Bag.SetActive(Factor == 2);
        Moves.SetActive(Factor == 3);
        OptionsPanel.SetActive(Factor == 4);

        if (Bag.activeSelf)
        {
            OpenBag();
        }
    }

    public void OpenBag()
    {
        //OpenPlayers bag

        Debug.Log("Attempting to open bag.");

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

    public void IncStrengthSkill()
    {
        Player player = FindObjectOfType<Player>();

        player.combattant.experience -= 1;

        player.combattant.Strength.experience += 1;

        if (player.combattant.Strength.experience > player.combattant.Strength.maxExperience)
        {
            player.combattant.Strength.experience = 0;

            player.combattant.Strength.level += 1;
        }
    }

    public void IncStaminaSkill()
    {
        Player player = FindObjectOfType<Player>();

        player.combattant.experience -= 1;

        player.combattant.StaminaStat.experience += 1;

        if (player.combattant.StaminaStat.experience > player.combattant.StaminaStat.maxExperience)
        {
            player.combattant.StaminaStat.experience = 0;

            player.combattant.StaminaStat.level += 1;
        }
    }

    public void IncAgilitySkill()
    {
        Player player = FindObjectOfType<Player>();

        player.combattant.experience -= 1;

        player.combattant.Agility.experience += 1;

        if (player.combattant.Agility.experience > player.combattant.Agility.maxExperience)
        {
            player.combattant.Agility.experience = 0;

            player.combattant.Agility.level += 1;
        }
    }

    public void IncChinSkill()
    {
        Player player = FindObjectOfType<Player>();

        player.combattant.experience -= 1;

        player.combattant.Chin.experience += 1;

        if (player.combattant.Chin.experience > player.combattant.Chin.maxExperience)
        {
            player.combattant.Chin.experience = 0;

            player.combattant.Chin.level += 1;
        }
    }
}
