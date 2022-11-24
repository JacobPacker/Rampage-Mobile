using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSegments : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;
    public Animator anim;

    [HideInInspector]
    public bool destroyed = false;
    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        //print("damage is " + damageAmount);


        if (health == 2)
        {
            anim.Play("TwoHealth");
        }
        if (health == 1)
        {
            anim.Play("OneHealth");
        }
        if (health <= 0)
        {
            anim.Play("ZeroHealth");
            destroyed = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
