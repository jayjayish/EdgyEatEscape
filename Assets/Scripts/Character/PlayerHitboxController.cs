using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxController : MonoBehaviour, IPooledObject
{
    private float damage;

    protected string[] listofObstacleTags = {Tags.ENEMY};


    public void OnObjectSpawn()
    {

    }

    public void setDamage(float number)
    {
        damage = number;
    }


    protected virtual void Awake()
    {
   
    }


    protected virtual void OnTriggerEnter2D(Collider2D col) //check for collisions, aka dmg
    {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == Tags.ENEMY)
        {
            hitTarget.GetComponent<EnemyController>().DecrementHealth(damage);
        }

    }














}
