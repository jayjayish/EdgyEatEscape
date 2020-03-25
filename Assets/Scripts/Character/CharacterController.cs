using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//A Character is any entity that has health, physics and can be interacted with.



public class CharacterController : PhysicsObject
{

    [SerializeField]
    protected float maxHealth = 1;

    protected float currentHealth;

    // Add stuff here as needed asssssssssss
    protected override void OnEnable()
    {
        base.OnEnable();
    }


    protected override void Start()
    {
        base.Start();
        
        currentHealth = maxHealth;
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

    #region Health
    public virtual void DecrementHealth(float damage)
    {
        Debug.Log(damage);
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        if (IsHealthZero())
        {
            OnDeath();
        }
       // updateHealthBar();
    }

	
	
	
	public virtual void DecrementHealthMagic(float damage)
    {

        DecrementHealth(damage);
       // updateHealthBar();
    }
	
	
	
	public virtual void DecrementHealthPhysical(float damage)
    {

        DecrementHealth(damage);
        // updateHealthBar();
    }
	
	
	
	
    /*
    private void updateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(healthPoints / maxHealth, 1, 1);
    }
    */

    protected bool IsHealthZero()
    {
        return currentHealth <= 0;
    }

    protected virtual void OnDeath()
    {
        //onCharacterDeath();
    }

    public virtual void IncrementHealth(float heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        //updateHealthBar();
    }

    /*
    public void damagedByAttacker(float damage)
    {
        if (!isInvulnerable)
        {
            isInvulnerable = true;
            decrementHealth(damage);
            Invoke("resetInvulnerable", invulnerableTimer);
        }
    }
    */
    #endregion


}
