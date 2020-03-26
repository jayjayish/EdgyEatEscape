using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightPatrol : EnemyController
{
    public float speed;

    public float distance;

    private bool movingRight = true;
    
    public Transform groundDetection;


    private float timer = 0f;


    protected override void Start(){
        base.Start();
        timer = Time.time;
    }

    protected override void ComputeVelocity()
    {
        if (movingRight)
        {
            targetVelocity.x = speed;
        }
        else
        {
            targetVelocity.x = -speed;
        }

    }
    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
       
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false && Time.time - timer > 0.5f)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            movingRight = !movingRight;
            timer = Time.time;
        }
    }
}
