using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUniversals : MonoBehaviour
{
  
    public int Stength; //attack
    public int Agility; //attackspeed
    public int Chin; //defence
    public int StaminaStat;


    //vals
    public int MaxHealth;
    public int MaxStamina;
    public int CurrentStamina;
    public int CurrentHealth;
    public int PL;

    public Sprite CombatSprite;
    public Sprite BackSprite;


    // Start is called before the first frame update
    void Start()
    {
        CalculatePowerlevel();
        CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag != "Player")
        {
            if (CurrentHealth<= 0) {
                //WinCombat();
            }
        }
    }

    public void CalculatePowerlevel() {
        PL = StaminaStat + Stength + Chin + Agility;

    }

    public void CalculateHealth()
    {
        MaxHealth = (StaminaStat + Chin) *2;
        CurrentHealth = MaxHealth;
    }



}
