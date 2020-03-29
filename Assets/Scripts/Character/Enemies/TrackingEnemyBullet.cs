using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemyBullet : ProjectileController, IPooledObject
{


    PhysicsObject target;


    protected override void OnMovementTimeToLiveStopped()
    {
        gameObject.SetActive(false);
    }


    public override void OnObjectSpawn()
    {
        target = GameObject.FindObjectOfType<PlayerController>();
        angle = Vector2.Angle(target.transform.position, transform.position);
        GetComponent<CircleCollider2D>().enabled = true;
        base.OnObjectSpawn();
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == Tags.PLAYER)
        {
            Debug.Log("Hit");
        }

        base.OnTriggerEnter2D(col);
    }

    protected override void OnHitObject()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }



}

