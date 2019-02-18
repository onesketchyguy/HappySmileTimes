using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public Collider2D[] Cols;
    public GameObject[] parts;
    public float AttackSpeed = 1f;
    public Health health;
    public Rigidbody2D RD;
    // Start is called before the first frame update
    void Start()
    {
        Clear();
        RD = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            Attack(Cols[0]);
            PartsController(parts[0]);
            print("pressed");
            // Cols[0].SetActive(true);
            Invoke("Clear", AttackSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            Attack(Cols[1]);
            PartsController(parts[1]);
            print("pressed");
            Invoke("Clear", AttackSpeed);
            // Cols[0].SetActive(true);
        }

        print(gameObject.name + health.health);

    }
    public void PartsController(GameObject Part)
    {
        foreach (GameObject part in parts)
        {

            part.SetActive(false);
        }
        Part.SetActive(true);

    }

    public void Clear()
    {
        foreach (GameObject part in parts)
        {
            part.SetActive(false);
        }
    }

    public void Attack(Collider2D col)
    {
        print(col.name);
   


    }

}
