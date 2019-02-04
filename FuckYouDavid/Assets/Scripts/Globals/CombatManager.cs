using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    private bool playersTurn = false;

    public GameObject CombatScreen;

    [SerializeField] private Image[] icons;

    [SerializeField] private GameObject optionsRegion;

    [SerializeField] private GameObject[] combatant_regions;

    [SerializeField] private Button[] attacks;

    public static CombatUniversals combatant_0, combatant_1;

    [SerializeField] private GameObject fightOptionsRegion;

    [SerializeField] private Text Logger;
    private bool logging = true;
    private Queue<string> loggerQueue = new Queue<string> { };

    private void SetCombatScreen(bool active)
    {
        if (active == true && !CombatScreen.activeSelf)
        {
            if (combatant_regions[0] == null || combatant_regions[1] == null)
            {
                Debug.LogError("Combatant regions not settup properly! Unable to complete combat task.");

                return;
            }

            loggerQueue.Enqueue($"{combatant_1.Name} encountered!");

            InvokeRepeating("Logger_DisplayNext", 0, 1);
        }

        CombatScreen.SetActive(active);
    }

    private void Update()
    {
        if (CombatScreen.activeSelf)
        {
            optionsRegion.SetActive(playersTurn == true && logging == false);

            SetupUI();

            if (logging == false)
            {
                Logger.text = "";

                if (combatant_0 != null && combatant_1 != null)
                {
                    //Combatant attack
                    if (playersTurn == false)
                    {
                        SetFightPanel(false);

                        // Implement AI that makes strategy instead of being retarded

                        AI_Attack(Random.Range(0, combatant_1.attacks.Count - 1));
                    }
                }
            }
            else
            {
                SetFightPanel(false);
            }
        }
        else
        {
            SetFightPanel(false);
        }

        SetCombatScreen(GameManager.gameState == GameManager.GameState.InCombat);
    }

    public void Logger_DisplayNext()
    {
        //Display next text

        if (loggerQueue.Count > 0)
        {
            Logger.text = $"{loggerQueue.Dequeue()}";

            if (Logger.text == "<i>Got away!</i>")
            {
                //Replace with proper escape!
                combatant_1.CurrentHealth = 0;

                GameManager.gameState = GameManager.GameState.Playing;
            }
        }
        else
        {
            CancelInvoke("Logger_DisplayNext");

            if (combatant_1.CurrentHealth <= 0 || combatant_0.CurrentHealth <= 0)
            {
                GameManager.gameState = GameManager.GameState.Playing;

                if (combatant_0.CurrentHealth <= 0)
                {
                    GameManager.instance.LoadScene("Main");
                }

                return;
            }

            playersTurn = !playersTurn;
            logging = false;
        }

    }

    private void SetupUI()
    {
        if (combatant_0 != null && combatant_1 != null)
        {
            //Player
            icons[0].sprite = combatant_0.Image;

            //Combatant
            icons[1].sprite = combatant_1.Image;

            SetUiElements(combatant_regions[1], combatant_1);

            SetUiElements(combatant_regions[0], combatant_0);
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

        int special = 0;

        int power = combatant_A.attacks.ToArray()[attackNo].power;

        switch (combatant_A.attacks.ToArray()[attackNo].powerType)
        {
            case CombatUniversals.Move.reliance.Strength:
                hit = CalculateChances(combatant_A.Stength.level, combatant_B.Agility.level);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Stength.level;
                break;
            case CombatUniversals.Move.reliance.Agility:
                hit = CalculateChances(combatant_A.Agility.level, combatant_B.Agility.level);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Agility.level;
                break;
            case CombatUniversals.Move.reliance.Chin:
                hit = CalculateChances(combatant_A.Chin.level, combatant_B.Agility.level);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Chin.level;
                break;
            case CombatUniversals.Move.reliance.Stamina:
                hit = CalculateChances(combatant_A.StaminaStat.level, combatant_B.Agility.level);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.StaminaStat.level;
                break;
            default:
                break;
        }

        loggerQueue.Enqueue($"{combatant_A.Name} used {combatant_A.attacks.ToArray()[attackNo].name}...");

        if (hit == true)
        {
            //Hit

            int damageToDeal = Random.Range(power, power + special + 1);

            loggerQueue.Enqueue($"<b>{combatant_A.Name}</b> hit <b>{combatant_B.Name}</b> for <b>{damageToDeal}!</b>");

            if (damageToDeal >= power + special)
            {
                loggerQueue.Enqueue("<b>Critical hit!</b>");
            }

            combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

            combatant_B.CurrentHealth -= damageToDeal;

            if (combatant_A.attacks.ToArray()[attackNo].experience >= combatant_A.attacks.ToArray()[attackNo].maxExperience)
            {
                if (1 <= combatant_A.attacks.ToArray()[attackNo].Upgrade.Length)
                {
                    loggerQueue.Enqueue($"{combatant_A.attacks.ToArray()[attackNo].name} leveled up to {combatant_A.attacks.ToArray()[attackNo].Upgrade[0].name} for {combatant_A.Name}!");

                    combatant_A.attacks.Add(combatant_A.attacks.ToArray()[attackNo].Upgrade[0]);

                    combatant_A.attacks.Remove(combatant_A.attacks.ToArray()[attackNo]);

                    combatant_A.attacks.ToArray()[attackNo].experience = 0;

                }
                else
                {
                    loggerQueue.Enqueue($"{combatant_A.attacks.ToArray()[attackNo].name} leveled up! But there are no upgrades available...");
                }
            }
        }
        else
        {
            //miss

            loggerQueue.Enqueue($"<i>It missed...</i>");
        }

        logging = true;

        InvokeRepeating("Logger_DisplayNext", 0, 2);

        if (combatant_1.CurrentHealth <= 0 && !loggerQueue.Contains($"{combatant_1.Name} knocked out."))
        {
            loggerQueue.Enqueue($"{combatant_1.Name} got knocked out.");

            int experienceAdded = Random.Range(combatant_1.PowerLevel / 2, combatant_1.PowerLevel * 2);

            combatant_0.experience += (experienceAdded);

            loggerQueue.Enqueue($"Gained {experienceAdded} XP.");
        }

        if (combatant_0.CurrentHealth <= 0 && !loggerQueue.Contains($"{combatant_0.Name} knocked out."))
        {
            loggerQueue.Enqueue($"{combatant_0.Name} got knocked out.");
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

    //Pressed by button
    public void SetFightPanel(bool active)
    {
        if (fightOptionsRegion != null)
        {
            fightOptionsRegion.SetActive(active);

            if (active == true)
            {
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

        GameManager.gameState = GameManager.GameState.InBag;
    }

    public void Run()
    {
        if (CalculateChances(combatant_0.Agility.level, 0.6f))
        {
            loggerQueue.Enqueue("Got away!");
        }
        else
        {
            loggerQueue.Enqueue("Failed to get away!");
        }

        playersTurn = !playersTurn;
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
                if (text.name.Contains("Health"))
                {
                    text.text = $"{combatUniversal.CurrentHealth}/{combatUniversal.MaxHealth}";
                }
                else
                {
                    text.text = combatUniversal.Name;
                }
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