using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is for all player movement including jumps, dashes, ducking etc.
// If there are attacks that move the character, it will be in a seperate script




public class PlayerController : CharacterController
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;

    // animation variables
    Animator animator;
    private bool playerMoving;
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
    private string[] comboExecuted;
    private float TriggerTime = 0f;

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

        // detect dash

        if (Input.GetButtonDown("DashLeft")) //checks if "a" or left arrow button was pressed
        {
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
            Debug.Log("DashRight");// checks for input

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

    // detect combo input
    protected virtual void detectCombo()
    {
        // just use Debug.Log to indicate what was pressed and what "combo" was execcuted
        comboExecuted[0] = "yes";
        //just detect combo sequence input, check key clicks in timing

        // define key inputs
        // learn how arrays work, ask Amy lol
        if (Input.GetButtonDown("TriggerR") || Input.GetButtonDown("TriggerL"))
        {
            float timesinceLastTrigger = Time.time - TriggerTime;



            Debug.Log("Triggered"); //checks for which attack key is pressed

        }
        // check initial attack key
        // set timing
        // check next key
        // repeat for up to 6 keys

        // for each input, set array element to inputted key
        // on end of combo, reset array
    }






    /*
     * 
     * THE STYLE OF HOW TO SPAWN A HITBOX
 private void SOME TYPE OF HITBOX()
{
    GameObject hitbox = HitboxPooler.Instance.SpawnFromPool(Pool.HITBOXNAME, transform.position); Transform.position may have offsets for staring position of hitbox
    hitbox.GetComponent<Filename of Hitbox>().OnObjectSpawn();
}

 */
}

