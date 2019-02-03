using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public GameObject CombatScreen;

    [SerializeField] private Image[] icons;

    [SerializeField] private GameObject[] combatant_regions;

    [SerializeField] private Button[] attacks;

    public static CombatUniversals combatant_0, combatant_1;

    [SerializeField] private GameObject fightOptionsRegion;

    private void Update()
    {
        CombatScreen.SetActive(GameManager.gameState == GameManager.GameState.InCombat);


        if (CombatScreen.activeSelf)
        {
            if (combatant_regions[0] != null && combatant_regions[1] != null)
            {
                if (combatant_0 != null)
                {
                    icons[0].sprite = combatant_0.Image;

                    SetUiElements(combatant_regions[0], combatant_0);
                }

                if (combatant_1 != null)
                {
                    icons[1].sprite = combatant_1.Image;

                    SetUiElements(combatant_regions[1], combatant_1);
                }
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

    public void Attack(int attackNo)
    {
        bool hit = false;

        int power = 0;

        switch (combatant_0.attacks.ToArray()[attackNo].powerType)
        {
            case CombatUniversals.CombatOptions.reliance.Strength:
                hit = CalculateChances(combatant_0.Stength, combatant_1.Agility);

                power = combatant_0.attacks.ToArray()[attackNo].power * combatant_0.Stength;
                break;
            case CombatUniversals.CombatOptions.reliance.Agility:
                hit = CalculateChances(combatant_0.Agility, combatant_1.Agility);

                power = combatant_0.attacks.ToArray()[attackNo].power * combatant_0.Agility;
                break;
            case CombatUniversals.CombatOptions.reliance.Chin:
                hit = CalculateChances(combatant_0.Chin, combatant_1.Agility);

                power = combatant_0.attacks.ToArray()[attackNo].power * combatant_0.Chin;
                break;
            case CombatUniversals.CombatOptions.reliance.Stamina:
                hit = CalculateChances(combatant_0.StaminaStat, combatant_1.Agility);

                power = combatant_0.attacks.ToArray()[attackNo].power * combatant_0.StaminaStat;
                break;
            default:
                break;
        }

        if (hit)
        {
            Debug.Log("Hit!");
            //Hit or 
            combatant_1.CurrentHealth -= power;
        }
        else
        {
            Debug.Log("Miss!");
            //miss
        }
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