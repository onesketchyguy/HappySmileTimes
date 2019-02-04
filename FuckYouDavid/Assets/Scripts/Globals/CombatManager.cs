using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    private bool playersTurn;

    public GameObject CombatScreen;

    [SerializeField] private Image[] icons;

    [SerializeField] private GameObject optionsRegion;

    [SerializeField] private GameObject[] combatant_regions;

    [SerializeField] private Button[] attacks;

    public static CombatUniversals combatant_0, combatant_1;

    [SerializeField] private GameObject fightOptionsRegion;

    private void Update()
    {
        CombatScreen.SetActive(GameManager.gameState == GameManager.GameState.InCombat);

        if (CombatScreen.activeSelf)
        {
            optionsRegion.SetActive(playersTurn);

            if (combatant_regions[0] != null && combatant_regions[1] != null)
            {
                if (combatant_0 != null)
                {
                    icons[0].sprite = combatant_0.Image;

                    SetUiElements(combatant_regions[0], combatant_0);

                    if (combatant_0.CurrentHealth <= 0)
                    {
                        GameManager.gameState = GameManager.GameState.Playing;
                    }
                }

                if (combatant_1 != null)
                {
                    icons[1].sprite = combatant_1.Image;

                    SetUiElements(combatant_regions[1], combatant_1);

                    if (combatant_1.CurrentHealth <= 0)
                    {
                        GameManager.gameState = GameManager.GameState.Playing;
                    }
                }
            }

            if (!playersTurn)
            {
                SetFightPanel(false);

                // Implement AI that makes strategy instead of being retarded

                AI_Attack(Random.Range(0, combatant_1.attacks.Count - 1));
            }
        }
        else
        {
            SetFightPanel(false);
        }
    }

    public void Fight()
    {
        //Open fight menu

        SetFightPanel(true);

        for (int i = 0; i < attacks.Length; i++)
        {
            var item = attacks[i];

            if (i < combatant_0.attacks.Count)
            {
                item.GetComponentInChildren<Text>().text = combatant_0.attacks.ToArray()[i].name;
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    public void Player_Attack(int attackNo)
    {
        CommitAttack(attackNo, combatant_0, combatant_1);
    }

    public void AI_Attack(int attackNo)
    {
        CommitAttack(attackNo, combatant_1, combatant_0);
    }

    private void CommitAttack(int attackNo, CombatUniversals combatant_A, CombatUniversals combatant_B)
    {
        bool hit = false;

        int power = 0;

        switch (combatant_A.attacks.ToArray()[attackNo].powerType)
        {
            case CombatUniversals.Move.reliance.Strength:
                hit = CalculateChances(combatant_A.Stength, combatant_B.Agility);

                power = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Stength;
                break;
            case CombatUniversals.Move.reliance.Agility:
                hit = CalculateChances(combatant_A.Agility, combatant_B.Agility);

                power = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Agility;
                break;
            case CombatUniversals.Move.reliance.Chin:
                hit = CalculateChances(combatant_A.Chin, combatant_B.Agility);

                power = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Chin;
                break;
            case CombatUniversals.Move.reliance.Stamina:
                hit = CalculateChances(combatant_A.StaminaStat, combatant_B.Agility);

                power = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.StaminaStat;
                break;
            default:
                break;
        }

        if (hit)
        {
            Debug.Log("Hit!");
            //Hit or 
            combatant_B.CurrentHealth -= Random.Range(combatant_A.attacks.ToArray()[attackNo].power, power);
        }
        else
        {
            Debug.Log("Miss!");
            //miss
        }

        playersTurn = !playersTurn;
    }

    bool CalculateChances(int stat, float successChance)
    {
        float chance = 1 - Mathf.Abs((Random.Range(0.0f, 1 * stat) - 1 * stat) / 1 * stat);

        //Debug.Log(chance);

        return (chance > successChance);
    }

    bool CalculateChances(int stat, int stat2)
    {
        float chance = 1 - Mathf.Abs((Random.Range(0.0f, 1 * stat) - 1 * stat) / 1 * stat);

        //Debug.Log(chance);

        float chance2 = 1 - Mathf.Abs((Random.Range(0.0f, 1 * stat2) - 1 * stat2) / 1 * stat2);

        //Debug.Log(chance2);

        return (chance > chance2);
    }

    private void SetFightPanel(bool active)
    {
        if (fightOptionsRegion != null)
        {
            fightOptionsRegion.SetActive(active);
        }
        else
        {
            Debug.LogWarning("No fightOptionsRegion setup!");
        }
    }

    public void OpenBag()
    {
        //OpenPlayers bag

        Debug.Log("Attempting to open bag.");
    }

    public void Run()
    {
        if (CalculateChances(combatant_0.Agility, 0.6f))
        {
            Debug.Log("Got away!");

            //Replace with proper escape!
            combatant_1.CurrentHealth = 0;

            GameManager.gameState = GameManager.GameState.Playing;
        }
        else
        {
            Debug.Log("Failed to get away!");
        }
    }

    private void SetUiElements(GameObject uiParent, CombatUniversals combatUniversal)
    {
        foreach (var item in uiParent.GetComponentsInChildren<Transform>())
        {
            if (item == uiParent.transform)
                continue;

            Text text = item.GetComponent<Text>();

            if (text)
            {
                text.text = combatUniversal.Name;
            }

            Slider slider = item.GetComponent<Slider>();

            if (slider)
            {
                slider.value = combatUniversal.CurrentHealth;
                slider.maxValue = combatUniversal.MaxHealth;
            }
        }
    }
}