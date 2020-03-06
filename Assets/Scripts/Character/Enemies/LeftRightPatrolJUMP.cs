using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightPatrolJUMP : CharacterController
{

    [SerializeField]
    private float speed;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float nextJump =0;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float jumpRate = 1f;
    [SerializeField]
    private bool movingRight = true;
    [SerializeField]
    private Transform groundDetection;






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

        if (Time.time > nextJump)
        {
            velocity.y = jumpSpeed;

            Debug.Log("Jump!!!!!");

            nextJump = Time.time + jumpRate;

        }


    }
    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
       
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if(groundInfo.collider == false)
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
        }
    }
}
