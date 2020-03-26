﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrolController : EnemyController
{

    private bool movingTowardsB = true;

    private Vector2 pointA = new Vector2(0f,0f);
    public Vector2 pointB = new Vector2(0f,0f);
    public float speed = 3f;
    private Vector2 direction;
    private Vector2 dirVector;
    private float length;
    private float currentDist;
    private Vector2 tempVector;


    // Start is called before the first frame update
    protected override void Start()
    {
        initialGravityModifier = 0f;
        pointA = transform.position;
        dirVector = pointB - pointA;
        length = dirVector.magnitude;
        direction = dirVector.normalized;
        Debug.Log(direction);
        Debug.Log(pointA);
        base.Start();
        groundNormal = new Vector2 (0f, 1f);
        isGrounded = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {    
        isGrounded = false; //switches to true when collision is found

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x); //calculates vector perpendicular to ground normal

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false); //horizonatal movement

        move = Vector2.up * deltaPosition.y;

        Movement(move, true); //vertical movement
    }

    protected override void ComputeVelocity()
    {
        tempVector = transform.position;
        if (movingTowardsB){
            tempVector = pointA - tempVector;
        }
        else {
            tempVector = pointB - tempVector;
        }
        
        currentDist = tempVector.magnitude;

        if (currentDist >= length){
            movingTowardsB = !movingTowardsB;
        }
        
        if (movingTowardsB){
            velocity.x = direction.x * speed;
            velocity.y = direction.y * speed;
        }
        else{
            velocity.x = -direction.x * speed;
            velocity.y = -direction.y * speed;
        }

        


        base.ComputeVelocity();
    }
}