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

        if (parent.activeSelf == true && Menu.activeSelf == true)
        {
            Player player = FindObjectOfType<Player>();

            StrengthText.text = $"STR {GameManager.playerCombat.Strength.level} : {GameManager.playerCombat.Strength.experience}/{GameManager.playerCombat.Strength.maxExperience}";

            StaminaText.text = $"STA {GameManager.playerCombat.StaminaStat.level}: {GameManager.playerCombat.StaminaStat.experience}/{GameManager.playerCombat.StaminaStat.maxExperience}";

            AgilityText.text = $"AGL {GameManager.playerCombat.Agility.level}: {GameManager.playerCombat.Agility.experience}/{GameManager.playerCombat.Agility.maxExperience}";

            ChinText.text = $"CHIN {GameManager.playerCombat.Chin.level}: {GameManager.playerCombat.Chin.experience}/{GameManager.playerCombat.Chin.maxExperience}";

            StrengthButton.gameObject.SetActive(0 < GameManager.playerCombat.experience);

            StaminaButton.gameObject.SetActive(0 < GameManager.playerCombat.experience);

            AgilityButton.gameObject.SetActive(0 < GameManager.playerCombat.experience);

            ChinButton.gameObject.SetActive(0 < GameManager.playerCombat.experience);

            experienceText.text = $"XP: {GameManager.playerCombat.experience}";
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
                        GameManager.playerCombat.CurrentHealth += (int) (10 * itemToRemove.ChanceOfEffect);

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

        GameManager.playerCombat.experience -= 1;

        GameManager.playerCombat.Strength.experience += 1;

        if (GameManager.playerCombat.Strength.experience > GameManager.playerCombat.Strength.maxExperience)
        {
            GameManager.playerCombat.Strength.experience = 0;

            GameManager.playerCombat.Strength.level += 1;
        }
    }

    public void IncStaminaSkill()
    {
        Player player = FindObjectOfType<Player>();

        GameManager.playerCombat.experience -= 1;

        GameManager.playerCombat.StaminaStat.experience += 1;

        if (GameManager.playerCombat.StaminaStat.experience > GameManager.playerCombat.StaminaStat.maxExperience)
        {
            GameManager.playerCombat.StaminaStat.experience = 0;

            GameManager.playerCombat.StaminaStat.level += 1;
        }
    }

    public void IncAgilitySkill()
    {
        Player player = FindObjectOfType<Player>();

        GameManager.playerCombat.experience -= 1;

        GameManager.playerCombat.Agility.experience += 1;

        if (GameManager.playerCombat.Agility.experience > GameManager.playerCombat.Agility.maxExperience)
        {
            GameManager.playerCombat.Agility.experience = 0;

            GameManager.playerCombat.Agility.level += 1;
        }
    }

    public void IncChinSkill()
    {
        Player player = FindObjectOfType<Player>();

        GameManager.playerCombat.experience -= 1;

        GameManager.playerCombat.Chin.experience += 1;

        if (GameManager.playerCombat.Chin.experience > GameManager.playerCombat.Chin.maxExperience)
        {
            GameManager.playerCombat.Chin.experience = 0;

            GameManager.playerCombat.Chin.level += 1;
        }
    }
}
