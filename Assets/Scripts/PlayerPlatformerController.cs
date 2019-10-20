using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        
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
