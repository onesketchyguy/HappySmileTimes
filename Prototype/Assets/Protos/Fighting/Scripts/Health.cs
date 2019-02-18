using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health=25;
    public int Maxhealth = 25;
    // Start is called before the first frame update
    void Start()
    {
        health = Maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0) {
            Die();
        }
    }
    public void Dam(int damage) {
     health -= damage;
    }

    public void Die() {
        print("Dead");
        Destroy(gameObject);
        health = Maxhealth;
    }

}
