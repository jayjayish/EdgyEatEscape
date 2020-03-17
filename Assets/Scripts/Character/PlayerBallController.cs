using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallController : ProjectileController, IPooledObject
{

    private float direction = 1f;

    // Update is called once per frame

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        
        if(col.tag == Tags.ENEMY){
            col.gameObject.GetComponent<EnemyController>().DecrementHealthMagic(damage);
        }
        

        base.OnTriggerEnter2D(col);
    }

    protected override void OnHitObject(){
        gameObject.SetActive(false);
    }

    protected override void OnMovementTimeToLiveStopped(){
        gameObject.SetActive(false);
    }


    public void SetAngle(float ang)
    {
        angle = ang;
    }

}
