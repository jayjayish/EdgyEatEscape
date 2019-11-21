using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    [SerializeField] protected float damage;


    [SerializeField] protected float startupFrames; //beginning of combo
    [SerializeField] protected float activeFrames; //current duration
    [SerializeField] protected float endFrames; //ending of combo

    protected float frames;
    protected bool isActive; //hitbox is active or not

    protected string[] listofObstacleTags = {Tags.ENEMY};


    protected virtual void Awake()
    {
        frames = 0;
        isActive = false;        
    }

    protected virtual void Update()
    {
        frames++;

        frameData();
    }

    protected virtual void frameData()
    {
        if (frames < activeFrames)
        {            
            //start up animation
        }
        else if (frames < activeFrames + startupFrames)
        {
            //check if hitbox interact and damage is dealt
        }
        else if (frames < activeFrames +startupFrames + endFrames)
        {
            //have endlag
            isActive = false;
        }
        else
        {
            //attack is finished
            //set character to inactive
        }


    }



    protected virtual void OnTriggerEnter2D(Collider2D col) //check for collisions, aka dmg
    {
        checkIfHitObject(col.tag)
    }



    protected virtual void checkIfHitObject(string tag) //check if object hit it part of the list of objects to be damaged
    {
        if (isActive)
        {
            for (int i = 0; i < listofObstacleTags.Length; i++)
            {
                if  (tag == listofObstacleTags[i])
                {
                    onHitObject();
                }

            }

           
        }

        protected abstract void onHitObject(); //deal damage and other effects



    }






}
