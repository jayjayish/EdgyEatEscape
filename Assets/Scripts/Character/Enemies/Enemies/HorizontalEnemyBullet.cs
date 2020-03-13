using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyBullet : ProjectileController, IPooledObject
{


    PhysicsObject target;
    //protected string[] listOfObstacleTags = {Tags.SOLID_OBSTACLE };

    protected override void OnMovementTimeToLiveStopped()
    {
        gameObject.SetActive(false);
        GetComponent<CircleCollider2D>().enabled = false;
    }


    public void OnObjectSpawn()
    {
        
        target = GameObject.FindObjectOfType<PlayerController>();
        angle = Mathf.Sign(target.transform.position.x - transform.position.x) * 90 + 90;
        currentTimeToLive = 0;
        GetComponent<CircleCollider2D>().enabled = true;
        isMoving = true;

        //Debug.Log(angle);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == Tags.PLAYER)
        {
                //Debug.Log("Hit");
                hitTarget.GetComponent<PlayerController>().DecrementHealth(1);
        }

        base.OnTriggerEnter2D(col);
    }

    protected override void OnHitObject()
    {
        isMoving = false;
        GetComponent<CircleCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }




}

