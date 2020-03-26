using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All Enemies that can damage the player from contact.
// Interactions with the prefab, spawning, and prefab cleanup will be here


public class EnemyController : CharacterController, IPooledObject
{
    [SerializeField] protected float touchDamage = 1f;
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

        currentHealth = maxHealth;
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

    protected override void OnDeath()
    {
        //onCharacterDeath();
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) //check for collisions, aka dmg
    {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == Tags.PLAYER)
        {
            hitTarget.GetComponent<PlayerController>().DecrementHealth(touchDamage);
        }

    }

    public virtual void OnObjectSpawn(){

    }

}
