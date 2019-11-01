using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//A Character is any entity that has health, physics and can be interacted with.



public class CharacterController : PhysicsObject
{
    // Add stuff here as needed asssssssssss
    protected override void OnEnable()
    {
        base.OnEnable();
    }


    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void Movement(Vector2 move, bool yMovement)
    //Function for movement
    {
        base.Movement(move, yMovement);
    }

    protected override void ComputeVelocity() // no implentation in this class, but called in update
    {
        base.ComputeVelocity();
    }


}
