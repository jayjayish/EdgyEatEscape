using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyController : EnemyController
{
    public float speed;
    public float distance;

    public int counter = 0;

    private bool movingRight = true;

    public Transform RightGroundRay;
    public Transform LeftGroundRay;

    void Update()
    {
        if (movingRight){
            targetVelocity = new Vector2(speed, 0f);
        }
        else {
             targetVelocity = new Vector2(-speed, 0f);
        }

        Debug.Log(targetVelocity);
        counter ++;


        RaycastHit2D RightGroundInfo = Physics2D.Raycast(RightGroundRay.position, Vector2.down, distance); //POSSIBLY BUG CAUSING
        RaycastHit2D LeftGroundInfo = Physics2D.Raycast(LeftGroundRay.position, Vector2.down, distance);
        if(!RightGroundInfo.collider && counter > 50)
        {
            movingRight = false;
            counter = 0;
        }
        else if(!LeftGroundInfo.collider && counter > 50){
            movingRight = true;
            counter = 0;
        }
    }
    
   
}
