using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Moves : MonoBehaviour
{
 

    [Serializable]
    public class Move 
    {
        public string MoveName;
        public int MoveAttack;
        public int StaminaCost;
        public Sprite AttackImage;
    }

    public Move[] MoveList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
