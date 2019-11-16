using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is for all player movement including jumps, dashes, ducking etc.
// If there are attacks that move the character, it will be in a seperate script





public class PlayerMovementController : CharacterController
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    // animation variables
    Animator animator;
    private bool playerMoving;
    private float lastMoveX;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        
        //check if player is moving to set idle or moving animations
        playerMoving = (targetVelocity.x != 0);


        animator.SetFloat("Move X", targetVelocity.x);
        animator.SetBool("PlayerMoving", playerMoving);

        if (targetVelocity.x > 0)
        {
            animator.SetFloat("LastMoveX", 1f);

        }
        else if (targetVelocity.x < 0)
        {
            animator.SetFloat("LastMoveX", -1f);
        }
        else
        {
            animator.SetFloat("LastMoveX", animator.GetFloat("LastMoveX"));
        }


       // animator.SetFloat("LastMoveX", targetVelocity.x); 
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) //checks if jump button is pressed while grounded
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump")) // reduces velocity when user lets go of jump button
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        targetVelocity = move * maxSpeed;

    }
}
