using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    private bool playersTurn = false;

    public GameObject CombatScreen;
    public Player P;
    [SerializeField] private Image[] icons;

    [SerializeField] private GameObject optionsRegion;

    [SerializeField] private GameObject[] combatant_regions;

    [SerializeField] private Button[] attacks;
    public MusicManager Music;
    public static CombatUniversals combatant_0, combatant_1;

    [SerializeField] private GameObject fightOptionsRegion, inventoryPanel, inventoryContent;

    [SerializeField] private Text LoggerObject;
    private bool logging = true;
    private Queue<string> logger = new Queue<string> { };


    public void Start()
    {
        P = FindObjectOfType<Player>();
        Music = FindObjectOfType<MusicManager>();

    }

    private void SetCombatScreen(bool active)
    {
        if (active == true && !CombatScreen.activeSelf)
        {
            if (combatant_regions[0] == null || combatant_regions[1] == null)
            {
                Debug.LogError("Combatant regions not settup properly! Unable to complete combat task.");

                return;
            }

            playersTurn = Random.Range(0, 1) == 1;

            inventoryPanel.SetActive(false);

            logger.Enqueue($"{combatant_1.Name} encountered!");

            SetupUI();

            Invoke("Logger_DisplayNext", 0);
        }

        CombatScreen.SetActive(active);
    }

    private void Update()
    {
        if (CombatScreen.activeSelf)
        {
            optionsRegion.SetActive(playersTurn == true && logging == false);

            if (optionsRegion.activeSelf == false || fightOptionsRegion.activeSelf == true)
            {
                inventoryPanel.SetActive(false);
            }

            if (logging == false)
            {
                SetupUI();

                LoggerObject.text = "";

                if (combatant_0 != null && combatant_1 != null)
                {
                    //Combatant attack
                    if (playersTurn == false)
                    {
                        SetFightPanel(false);

                        // Implement AI that makes strategy instead of being retarded

                        Attack(Random.Range(0, combatant_1.attacks.Count - 1), combatant_1, combatant_0);
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

        if (logger.Count > 0)
        {
            LoggerObject.text = $"{logger.Dequeue()}";

            if (LoggerObject.text == "Got away!")
            {
                GameManager.gameState = GameManager.GameState.Playing;
            }

            if (LoggerObject.text.Contains("hit") || LoggerObject.text.Contains("encountered"))
            {
                SetupUI();

                SoundManager.Instance.PlayDoorSound(1);
            }
            else
            if (LoggerObject.text.Contains("leveled up"))
            {
                SoundManager.Instance.PlayLevelUpEffect();
            }

            float time = LoggerObject.text.ToCharArray().Length / 10;

            Invoke("Logger_DisplayNext", time > 1 && time < 3 ? time : 2);
        }
        else
        {
            CancelInvoke("Logger_DisplayNext");

            if (combatant_1.CurrentHealth <= 0 || combatant_0.CurrentHealth <= 0)
            {
                GameManager.gameState = GameManager.GameState.Playing;

                if (combatant_0.CurrentHealth <= 0)
                {
                    P.Respawn();
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
            Music.audioSource.clip = Music.Songs[1];
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
        Attack(attackNo, combatant_0, combatant_1);
    }

    public void Attack(int attackNo, CombatUniversals combatA, CombatUniversals combatb)
    {
        bool freed = false;

        string state = "";

        int damage = 0;

        switch (combatA.CurrentState)
        {
            case GameManager.States.Normal:
                CommitAttack(attackNo, combatA, combatb);
                break;
            case GameManager.States.GrossOut:
                freed = CalculateChances(combatA.Chin.level, 0.6f);

                state = "gross out";
                break;
            case GameManager.States.Burn:
                freed = CalculateChances(combatA.Chin.level, 0.6f);

                state = "burn";
                break;
            case GameManager.States.Freeze:
                freed = CalculateChances(combatA.Chin.level, 0.6f);

                state = "frozen";
                break;
            case GameManager.States.Paralysis:
                freed = CalculateChances(combatA.Chin.level, 0.6f);

                state = "paralysis";
                break;
            case GameManager.States.Poison:
                freed = CalculateChances(combatA.Chin.level, 0.6f);

                state = "poison";
                break;
            case GameManager.States.Confusion:
                freed = CalculateChances(combatA.Chin.level, 0.6f);

                logger.Enqueue($"{combatA.Name} is confused...");

                CommitAttack(attackNo, combatA, combatA);

                state = "confusion";
                break;
            case GameManager.States.Heal:
                freed = CalculateChances(combatA.Chin.level, 0f);

                state = "healing";
                break;
            case GameManager.States.Taunt:
                freed = CalculateChances(combatA.Chin.level, 0.6f);

                state = "taunting";
                break;
            case GameManager.States.Protection:
                freed = CalculateChances(combatA.Chin.level, 0f);

                state = "blocking";
                break;
            default:
                break;
        }

        if (freed)
        {
            logger.Enqueue($"{combatA.Name} recovered from {state}...");

            combatA.CurrentState = GameManager.States.Normal;

            Invoke("Logger_DisplayNext", 0);
        }
        else if (state != "")
        {
            logger.Enqueue($"{combatA.Name} is {state}...");

            if (damage > 0)
            {
                logger.Enqueue($"{combatA.Name} hurt for {damage}hp!");
            }

            Invoke("Logger_DisplayNext", 0);
        }
    }

    private void CommitAttack(int attackNo, CombatUniversals combatant_A, CombatUniversals combatant_B)
    {
        bool hit = false;

        int special = 0;

        int power = combatant_A.attacks.ToArray()[attackNo].power;

        logger.Enqueue($"{combatant_A.Name} used {combatant_A.attacks.ToArray()[attackNo].name}...");

        switch (combatant_A.attacks.ToArray()[attackNo].Effect)
        {
            case GameManager.States.Normal:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level - combatant_B.Chin.level;

                    hit = CalculateChances(power, combatant_B.Agility.level);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Mathf.Abs(Random.Range(power, power + special));

                        if (damageToDeal <= 0)
                        {
                            logger.Enqueue("It does nothing...");
                        }
                        else
                        {
                            logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                            if (damageToDeal >= power + special)
                            {
                                logger.Enqueue("Critical!");
                            }

                            combatant_A.attacks.ToArray()[attackNo].experience += Random.Range(0, damageToDeal);

                            combatant_B.CurrentHealth -= damageToDeal;

                            CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);
                        }
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.GrossOut:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level;

                    hit = CalculateChances(power, combatant_B.Agility.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("Critical hit!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        combatant_B.CurrentHealth -= damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_B.CurrentState = GameManager.States.GrossOut;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.Burn:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level;

                    hit = CalculateChances(power, combatant_B.Agility.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("Critical hit!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        combatant_B.CurrentHealth -= damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_B.CurrentState = GameManager.States.Burn;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.Freeze:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level;

                    hit = CalculateChances(power, combatant_B.Agility.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("Critical hit!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        combatant_B.CurrentHealth -= damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_B.CurrentState = GameManager.States.Freeze;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.Paralysis:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level;

                    hit = CalculateChances(power, combatant_B.Agility.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("Critical hit!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        combatant_B.CurrentHealth -= damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_B.CurrentState = GameManager.States.Paralysis;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.Poison:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level;

                    hit = CalculateChances(power, combatant_B.Agility.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("Critical hit!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        combatant_B.CurrentHealth -= damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_B.CurrentState = GameManager.States.Poison;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.Confusion:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level;

                    hit = CalculateChances(power, combatant_B.Agility.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("Critical hit!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        combatant_B.CurrentHealth -= damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_B.CurrentState = GameManager.States.Confusion;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.Heal:
                {
                    power += combatant_A.Strength.level + combatant_A.Chin.level;

                    hit = CalculateChances(power, combatant_B.Strength.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Chin.level));

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} successfully healing.");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("It's extremely effective!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_A.CurrentState = GameManager.States.Heal;

                        combatant_A.CurrentHealth += damageToDeal;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It failed...</i>");
                    }
                }
                break;
            case GameManager.States.Taunt:
                {
                    power += combatant_A.Strength.level + combatant_A.Agility.level;

                    hit = CalculateChances(power, combatant_B.Agility.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Agility.level)) - combatant_B.Chin.level;

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} hit {combatant_B.Name} for {damageToDeal}hp!");

                        if (damageToDeal >= power + special)
                        {
                            logger.Enqueue("Critical hit!");
                        }

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        combatant_B.CurrentHealth -= damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_B.CurrentState = GameManager.States.Taunt;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It missed...</i>");
                    }
                }
                break;
            case GameManager.States.Protection:
                {
                    power += combatant_A.Strength.level + combatant_A.Chin.level;

                    hit = CalculateChances(power, combatant_B.Strength.level * 2);

                    special = (combatant_A.attacks.ToArray()[attackNo].power * (combatant_A.Strength.level + combatant_A.Chin.level));

                    if (hit == true)
                    {
                        //Hit
                        int damageToDeal = Random.Range(power, power + special + 1);

                        logger.Enqueue($"{combatant_A.Name} is blocking.");

                        combatant_A.attacks.ToArray()[attackNo].experience += damageToDeal;

                        CheckToSeeIfAttackLeveledUp(attackNo, combatant_A);

                        combatant_A.CurrentState = GameManager.States.Protection;
                    }
                    else
                    {
                        //miss

                        logger.Enqueue($"<i>It failed...</i>");
                    }
                }
                break;
            default:
                break;
        }

        logging = true;

        Invoke("Logger_DisplayNext", 0);

        if (combatant_1.CurrentHealth <= 0 && (!logger.Contains($"{combatant_1.Name}")&& !logger.Contains("knocked out")))
        {
            if (combatant_1.CurrentHealth <= -2 && (!logger.Contains($"{combatant_1.Name}") && !logger.Contains("knocked out")))
                logger.Enqueue($"{combatant_1.Name} got knocked the fuck out!");

            int experienceAdded = Random.Range(combatant_1.PowerLevel, combatant_1.PowerLevel + 10);

            combatant_0.experience += (experienceAdded);

            logger.Enqueue($"Gained {experienceAdded} XP.");
        }

        if (combatant_0.CurrentHealth <= 0 && (!logger.Contains($"{combatant_0.Name}") && !logger.Contains("knocked out")))
        {
            if (combatant_0.CurrentHealth <= -2 && (!logger.Contains($"{combatant_0.Name}") && !logger.Contains("knocked out")))
                logger.Enqueue($"{combatant_0.Name} got knocked the fuck out!");
            logger.Enqueue($"{combatant_0.Name} got knocked out.");
        }
    }

    private void CheckToSeeIfAttackLeveledUp(int attackNo, CombatUniversals combatant_A)
    {
        if (combatant_A.attacks.ToArray()[attackNo].experience >= combatant_A.attacks.ToArray()[attackNo].maxExperience)
        {
            if (combatant_A.attacks.ToArray()[attackNo].Upgrade.Count >= 1)
            {
                logger.Enqueue($"{combatant_A.attacks.ToArray()[attackNo].name} leveled up to {combatant_A.attacks.ToArray()[attackNo].Upgrade[0].name} for {combatant_A.Name}!");

                combatant_A.attacks.Add(combatant_A.attacks.ToArray()[attackNo].Upgrade.ToArray()[0]);

                combatant_A.attacks.Remove(combatant_A.attacks.ToArray()[attackNo]);

                combatant_A.attacks.ToArray()[attackNo].experience = 0;

            }
            else
            {
                logger.Enqueue($"{combatant_A.attacks.ToArray()[attackNo].name} leveled up! But there are no upgrades available...");
            }
        }
    }

    bool CalculateChances(float successChance)
    {
        float chance = 1 - Mathf.Abs(1 - (Random.Range(0.0f, 1)) / 1);

        return (chance > successChance);
    }

    bool CalculateChances(int stat, float successChance)
    {
        float chance = 1 - Mathf.Abs((1 * stat) - (Random.Range(0.0f, 1 * stat)) / 1 * stat);

        return (chance > successChance);
    }

    bool CalculateChances(int stat, int stat2)
    {
        int chance1Stat = 1 * stat;

        float chance = 1 - Mathf.Abs(chance1Stat - (Random.Range(0.0f, chance1Stat)) / chance1Stat);

        int chance2Stat = 1 * stat2;

        float chance2 = 1 - Mathf.Abs(chance1Stat - (Random.Range(0.0f, chance1Stat)) / chance1Stat);

        return (chance >= chance2);
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

        inventoryPanel.SetActive(!inventoryPanel.activeSelf);

        foreach (var item in inventoryContent.GetComponentsInChildren<Transform>())
        {
            if (item == inventoryContent.transform)
                continue;

            Destroy(item.gameObject);
        }

        Player player = FindObjectOfType<Player>();

        if (player)
        {
            foreach (var item in player.inventory.items.ToArray())
            {
                GameObject invItem = Instantiate(new GameObject(), inventoryContent.transform) as GameObject;

                invItem.name = item.name;

                invItem.AddComponent<Image>().sprite = item.image;

                invItem.AddComponent<Button>().onClick.AddListener(delegate { RemoveItemFromBag(item); });

                GameObject textObject = new GameObject(item.name + "Text");

                textObject.transform.SetParent(invItem.transform);

                textObject.transform.position = invItem.transform.position;

                textObject.transform.localScale = new Vector3(1, 1, 1);

                Text t = textObject.AddComponent<Text>();

                t.font = GetComponentInChildren<Text>().font;

                t.color = Color.black;

                t.text = $"{item.name} x {item.count}";

                Outline ol = textObject.AddComponent<Outline>();

                ol.effectDistance = new Vector2(2, 2);

                ol.effectColor = Color.white;
            }
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void RemoveItemFromBag(ItemDefinition itemToRemove)
    {
        Player player = FindObjectOfType<Player>();

        if (player)
        {
            player.inventory.items.Remove(itemToRemove);

            foreach (var item in inventoryContent.GetComponentsInChildren<Transform>())
            {
                if (item == inventoryContent.transform)
                    continue;

                if (item.name == itemToRemove.name)
                {
                    if (CalculateChances(itemToRemove.ChanceOfEffect))
                    {
                        logger.Enqueue($"{combatant_0.Name} used {itemToRemove.name} successfully!");

                        switch (itemToRemove.Effect)
                        {
                            case GameManager.States.Normal:
                                combatant_0.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.GrossOut:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Burn:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Freeze:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Paralysis:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Poison:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Confusion:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Heal:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Taunt:
                                combatant_1.CurrentState = itemToRemove.Effect;
                                break;
                            case GameManager.States.Protection:
                                combatant_0.CurrentState = itemToRemove.Effect;
                                break;
                            default:
                                break;
                        }

                        Invoke("Logger_DisplayNext", 0);
                    }
                    else
                    {
                        logger.Enqueue($"{combatant_0.Name} failed to use {itemToRemove.name}...");
                    }

                    if (itemToRemove.count > 1)
                    {
                        itemToRemove.count -= 1;
                    }
                    else
                    {
                        Destroy(item.gameObject);
                    }
                }
                    
            }
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void Run()
    {
        if (CalculateChances(combatant_0.Agility.level, 0.6f))
        {
            logger.Enqueue("Got away!");
        }
        else
        {
            logger.Enqueue("Failed to get away!");
        }

        playersTurn = !playersTurn;
    }

    private void SetUiElements(GameObject uiParent, CombatUniversals combatant)
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
                    string state = "";

                    switch (combatant.CurrentState)
                    {
                        case GameManager.States.Normal:
                            state = "Fine";
                            break;
                        case GameManager.States.GrossOut:
                            state = "Grossed out";
                            break;
                        case GameManager.States.Burn:
                            state = "Burned";
                            break;
                        case GameManager.States.Freeze:
                            state = "Frozen";
                            break;
                        case GameManager.States.Paralysis:
                            state = "Paralized";
                            break;
                        case GameManager.States.Poison:
                            state = "Poisoned";
                            break;
                        case GameManager.States.Confusion:
                            state = "Confused";
                            break;
                        case GameManager.States.Heal:
                            state = "Healing";
                            break;
                        case GameManager.States.Taunt:
                            state = "Enraged";
                            break;
                        case GameManager.States.Protection:
                            state = "Blocking";
                            break;
                        default:
                            break;
                    }

                    text.text = $"{combatant.CurrentHealth}/{combatant.MaxHealth} : {state}";
                }
                else
                {
                    text.text = combatant.Name;
                }
            }

            Slider slider = item.GetComponent<Slider>();

            if (slider)
            {
                slider.value = combatant.CurrentHealth;
                slider.maxValue = combatant.MaxHealth;
            }
        }
    }
}