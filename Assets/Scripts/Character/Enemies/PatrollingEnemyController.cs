using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyController : EnemyController
{
    public float speed;

    public float distance;

    private bool movingRight = true;
    
    public Transform groundDetection;
    public Transform forwardDetection;


    private float timer = 0f;

    protected int groundLayer = 0;

   

    protected override void Start(){
        base.Start();
        timer = Time.time;
        groundLayer = LayerMask.NameToLayer("Ground");
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
        RaycastHit2D forwardInfo = Physics2D.Raycast(forwardDetection.position, new Vector2(1f, 0f), 0.1f);
    
        int forwardLayer = 0;

        if (forwardInfo.collider){
            forwardLayer = forwardInfo.collider.gameObject.layer;
        }
        


        if((groundInfo.collider == false && Time.time - timer > 0.5f) || (forwardLayer == groundLayer))
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
