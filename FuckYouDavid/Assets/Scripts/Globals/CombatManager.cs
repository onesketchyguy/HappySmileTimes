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

            if (Logger.text == "Got away!")
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

    public void SetupFightPanel()
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

        int special = 0;

        int power = combatant_A.attacks.ToArray()[attackNo].power;

        switch (combatant_A.attacks.ToArray()[attackNo].powerType)
        {
            case CombatUniversals.Move.reliance.Strength:
                hit = CalculateChances(combatant_A.Stength, combatant_B.Agility);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Stength;
                break;
            case CombatUniversals.Move.reliance.Agility:
                hit = CalculateChances(combatant_A.Agility, combatant_B.Agility);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Agility;
                break;
            case CombatUniversals.Move.reliance.Chin:
                hit = CalculateChances(combatant_A.Chin, combatant_B.Agility);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.Chin;
                break;
            case CombatUniversals.Move.reliance.Stamina:
                hit = CalculateChances(combatant_A.StaminaStat, combatant_B.Agility);

                special = combatant_A.attacks.ToArray()[attackNo].power * combatant_A.StaminaStat;
                break;
            default:
                break;
        }

        if (hit == true)
        {
            //Hit

            int damageToDeal = Random.Range(power, power + special + 1);

            loggerQueue.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}!");

            if (damageToDeal >= power + special)
            {
                loggerQueue.Enqueue($"It was a critical hit!");
            }

            combatant_B.CurrentHealth -= damageToDeal;
        }
        else
        {
            //miss

            loggerQueue.Enqueue($"{combatant_A.Name} missed attack against {combatant_B.Name}!");
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