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
        checkIfHitObject(col.tag);
    }



    protected virtual void checkIfHitObject(string tag) //check if object hit it part of the list of objects to be damaged
    {
        for (int i = 0; i < listofObstacleTags.Length; i++)
        {
            if (tag == listofObstacleTags[i])
            {
                onHitObject();
            }

        }
       
    }


    protected virtual void onHitObject() {

        Debug.Log("Touchy Touch");

        return;
    } //deal damage and other effects










}
