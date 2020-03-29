using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPatrolController : EnemyController
{

    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float distance = 1f;

    [SerializeField]
    private float jumpSpeed = 1f;
    [SerializeField]
    private float jumpRate = 1f;

    [SerializeField] private Transform frontGroundDetection = null;
    [SerializeField] private Transform centerGroundDetection = null;
    [SerializeField] private Transform forwardDetection = null;

    private bool isJumping = false;

    private bool canJump = true;


    private bool movingRight = true;


    private float timer = 0f;

    private float jumpTimer = 0f;
    
    protected int groundLayer = 0;


    protected override void Start(){
        base.Start();
        timer = Time.time;
        jumpTimer = Time.time;
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

        if (Time.time - jumpTimer > jumpRate && canJump)
        {
            velocity.y = jumpSpeed;
            
            isJumping = true;
            jumpTimer = Time.time;
        }


    }
    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
       
        RaycastHit2D frontGroundInfo = Physics2D.Raycast(frontGroundDetection.position, Vector2.down, distance);

        RaycastHit2D centerGroundInfo = Physics2D.Raycast(centerGroundDetection.position, Vector2.down, distance);

        RaycastHit2D forwardInfo = Physics2D.Raycast(forwardDetection.position, new Vector2(1f, 0f), 0.1f);
    
        int forwardLayer = 0;

        if (forwardInfo.collider){
            forwardLayer = forwardInfo.collider.gameObject.layer;
        }

        if (isJumping && centerGroundInfo.collider && Time.time - jumpTimer > 0.2){
            isJumping = false;
        }
        
        if((!centerGroundInfo.collider && !isJumping  && Time.time - timer > 0.5f) || forwardLayer == groundLayer)
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
            jumpTimer = Time.time - jumpRate / 2f;
        }


        if (!frontGroundInfo.collider){
            canJump = false;
        }
        else {
            canJump = true;
        }
            
        //Debug.Log(canJump);




    }
}
