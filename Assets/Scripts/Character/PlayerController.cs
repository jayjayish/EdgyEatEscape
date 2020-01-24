﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is for all player movement including jumps, dashes, ducking etc.
// If there are attacks that move the character, it will be in a seperate script




public class PlayerController : CharacterController
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private bool isAttacking = false;
    private float attackFrames = 0;

    // animation variables
    Animator animator;
    private bool isPlayerMoving;
    private bool facingLeft = true;
    private float lastMoveX;

    // constants for dash detection
    public const float DOUBLE_PRESS_TIME = .20f;
    private float lastLeftTime = 0f;
    private float lastRightTime = 0f;

    //dash time constants
    private int dashDirection;
    private const float dashMultiplier = 10f;
    private int dashingRight;
    private float dashTime;
    public float dashSpeed;
    public const float startDashTime = .1f;

    //combo array
    // 'h' for hardware and 's' for software
    private PlayerComboJSON comboJSON;
    private List<string> comboExecuted = new List<string>();
    private float lastTriggerTime = 0f;
    private float COMBO_TIME = 0.3f;
    private float TriggeredTime;
    public const float startTriggerTime = 0f;
    private int comboCount = 0;


    private Rigidbody2D rb2d;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        comboJSON = GetComponent<PlayerComboJSON>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        UpdateAnimator();
        DetectAttack();
        //DetectCombo();

    }



    #region Attacks

    //Hitbox Creation is HERE
    protected void DetectAttack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            //Animator and Data Table stuff goes here


            StartCoroutine(DoAttack("TESTBOX"));
        }
    }


    IEnumerator DoAttack(string hitboxName)
    {
        //Startup
        
        yield return new WaitForSeconds( comboJSON.getStartup(hitboxName.ToUpper()) * (1f/60f));


        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool(hitboxName.ToUpper(), comboJSON.getPosition(hitboxName.ToUpper()));


       // hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage(hitboxName.ToUpper()));
        //Vector3 temp = hitbox.transform.localScale;
        //temp.z = animator.GetFloat("LastMoveX");
        //hitbox.transform.localScale = temp;

        yield return new WaitForSeconds(comboJSON.getActive(hitboxName.ToUpper()) * (1f / 60f));

        hitbox.SetActive(false);
        isAttacking = false;

    }


    #endregion







    #region Animations
    protected void UpdateAnimator()
    {
        //check if player is moving to set idle or moving animations
        layerTransitions();
        isPlayerMoving = (targetVelocity.x != 0);


        animator.SetFloat("speed", Mathf.Abs(targetVelocity.x));

        BasicAttackAnimation();
        Flip(targetVelocity.x);

        jumpAnimation();
    }

    private void Flip(float xVelocity)
    {
        if (xVelocity > 0 && facingLeft || xVelocity < 0 && !facingLeft)
        {
            facingLeft = !facingLeft;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private void BasicAttackAnimation()
    {
        if (isAttacking)
            animator.SetTrigger("attack");
        else
            animator.ResetTrigger("attack");

    }

    private void layerTransitions()
    {
        if (!isGrounded)
        {
            animator.SetLayerWeight(1, 1);
            animator.SetLayerWeight(0, 0);
        }

        else
        {
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(0, 1);
        }

    }

    private void jumpAnimation()
    {
        Debug.Log("total velocity" + (rb2d.velocity.y + velocity.y));
        if (Input.GetButtonDown("Jump"))// && isGrounded)
        {
            animator.SetTrigger("jump");
            Debug.Log("jumped");
        }

        else if ((rb2d.velocity.y + velocity.y) < 0 && !isGrounded)
        {
            animator.SetBool("isfalling", true);
        }

        else if (isGrounded)
        {
            animator.SetBool("isfalling", false);
            animator.ResetTrigger("jump");
        }


    }

    #endregion





    #region Movement

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded) //checks if jump button is pressed while grounded
        {
            velocity.y = jumpTakeOffSpeed;
        }

        else if (Input.GetButtonUp("Jump")) // reduces velocity when user lets go of jump button
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        // detect dash

        if (Input.GetButtonDown("DashLeft")) //checks if "a" or left arrow button was pressed
        {
            //Debug.Log("DashLeft");//checks for input

            float timesinceLastLeft = Time.time - lastLeftTime;

            if (timesinceLastLeft <= DOUBLE_PRESS_TIME)
            {
                dashDirection = -1;
                dashTime = startDashTime;//timer for dash
                Debug.Log("dashTime:" + dashTime);
                Debug.Log("double left");//delta tiime
            }
            //Double click
            else
            {
                //Normal Click
                lastLeftTime = Time.time;
                //Debug.Log("left clicked" + lastLeftTime);
            }


        }

        if (Input.GetButtonDown("DashRight")) //checks if "d" or right arrow button was pressed
        {
            //Debug.Log("DashRight");// checks for input

            float timesinceLastRight = Time.time - lastRightTime;

            if (timesinceLastRight <= DOUBLE_PRESS_TIME)
            {
                dashDirection = 1;
                dashTime = startDashTime;//timer for dash
                Debug.Log("dashTime:" + dashTime);

                Debug.Log("double right");//debug delta time
            }
            //double click
            else
            {
                //Normal Click
                lastRightTime = Time.time;
                //Debug.Log("right clicked" + lastRightTime);
            }
        }

        if (dashTime > 0)
        {
            move.x = dashDirection * dashMultiplier;
            dashTime -= Time.deltaTime; //Decrease time counter
            //Debug.Log("dashTime:" + dashTime);
        }




        targetVelocity = move * maxSpeed;

    }





    #endregion










}
