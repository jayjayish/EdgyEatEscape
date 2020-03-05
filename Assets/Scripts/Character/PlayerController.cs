using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is for all player movement including jumps, dashes, ducking etc.
// If there are attacks that move the character, it will be in a seperate script




public class PlayerController : CharacterController
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
    private bool isAttacking = false;
    private bool canMoveWhileAttacking = false;
    private bool isControllingLaser = false;
    private float attackFrames = 0;
    private int playerLayer;
    private int enemyLayer;

    // animation variables
    Animator animator;
    private bool isPlayerMoving;
    private bool facingLeft = true;
    private float lastMoveX;
    Vector2 move;



    //Delegate
    protected delegate void attackDelegate();
    protected attackDelegate attackMovementDelegate;



    #region DashVariables
    // constants for dash detection
    public const float DOUBLE_PRESS_TIME = .20f;
    private float lastLeftTime = 0f;
    private float lastRightTime = 0f;
    private bool ignoreEnemyCollisions = true;
    #endregion
    
    #region DashConstants
    //dash time constants
    private int dashDirection;
    private float dashMultiplier = 5f;
    [SerializeField]
    private const float initialDashMultiplier = 5f;
    private int dashingRight;
    private float dashTime;
    public float dashSpeed;
    public const float startDashTime = .2f;
    #endregion

    #region ComboVariables
    //combo array
    // 'h' for hardware and 's' for software
    private PlayerComboJSON comboJSON;

    // private float lastTriggerTime = 0f;
    private float COMBO_TIME = 1f;
    private float TriggeredTime;
    //public const float startTriggerTime = 0f;

    private float timeOfLastAttack = 0;
    private string currentCombo;
    private int comboCount = 0;
    Queue<IEnumerator> comboQueue;
    protected bool comboQueueAlive = false;
    protected string lastButtonPressed;
    #endregion


    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        comboJSON = GetComponent<PlayerComboJSON>();
        rb2d = GetComponent<Rigidbody2D>();
        currentCombo = "";
        lastButtonPressed = "";
        comboQueue = new Queue<IEnumerator>();
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    protected override void Update()
    {
        base.Update();
        UpdateAnimator();
       // DetectAttack();
        DetectCombo();

    }



    #region Attacks

    //Hitbox Creation is HERE
    protected void DetectAttack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            //Animator and Data Table stuff goes here


            StartCoroutine(DoAttack("BASIC_ATTACK_BOX"));

            //animator.Play("Sonic_Slam");
        }
    }


    IEnumerator DoAttack(string hitboxName)
    {
        //Startup
        isAttacking = true;
        animator.SetTrigger(hitboxName);

        yield return new WaitForSeconds( comboJSON.getStartup(hitboxName.ToUpper()) * (1f/60f));


        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool(hitboxName.ToUpper(), comboJSON.getPosition(hitboxName.ToUpper()));
        hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage(hitboxName.ToUpper()));

       // hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage(hitboxName.ToUpper()));
        //Vector3 temp = hitbox.transform.localScale;
        //temp.z = animator.GetFloat("LastMoveX");
        //hitbox.transform.localScale = temp;

        yield return new WaitForSeconds(comboJSON.getActive(hitboxName.ToUpper()) * (1f / 60f));

        hitbox.SetActive(false);
        yield return new WaitForSeconds(comboJSON.getEndlag(hitboxName.ToUpper()) * (1f / 60f));
        isAttacking = false;

        attackMovementDelegate = null;

        timeOfLastAttack = Time.time;
        AttackQueueManager();

    }

    IEnumerator DoJumpAttack(string hitboxName)
    {
        //Startup
        isAttacking = true;
        attackMovementDelegate += JumpUp;
        attackMovementDelegate += MoveForward;
        yield return new WaitForSeconds(1f / 60f);
        attackMovementDelegate -= JumpUp;
        yield return new WaitForSeconds((comboJSON.getStartup(hitboxName.ToUpper()) - 1) * (1f / 60f));


        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool(hitboxName.ToUpper(), comboJSON.getPosition(hitboxName.ToUpper()));


        // hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage(hitboxName.ToUpper()));
        //Vector3 temp = hitbox.transform.localScale;
        //temp.z = animator.GetFloat("LastMoveX");
        //hitbox.transform.localScale = temp;

        yield return new WaitForSeconds(comboJSON.getActive(hitboxName.ToUpper()) * (1f / 60f));

        hitbox.SetActive(false);
        isAttacking = false;

        attackMovementDelegate = null;

    }
   


    IEnumerator DoLaserGeyser()
    {
        isAttacking = true;
        isControllingLaser = true;


        //Spawn stuff asofijaseofijaesofj


        yield return new WaitForSeconds(1f);


        //Explode laser if it hasnt been

        isAttacking = false;
        isControllingLaser = false;
    }


    IEnumerator DoTrojanHorse()
    {
        isAttacking = true;


        //Spawn stuff asofijaseofijaesofj


        yield return new WaitForSeconds(1f);


        //

        isAttacking = false;

    }

    IEnumerator DoShockwave()
    {
        isAttacking = true;


        //Spawn stuff asofijaseofijaesofj


        yield return new WaitForSeconds(1f);


        //

        isAttacking = false;

    }

    protected void JumpUp()
    {
        velocity.y = jumpTakeOffSpeed;
    }

    protected void MoveForward()
    {
        if (facingLeft)
        {
            move.x = -1;
        }
        else
        {
            move.x = 1;
        }
    }


    #endregion



    #region Animations
    protected void UpdateAnimator()
    {
        //check if player is moving to set idle or moving animations
        layerTransitions();
        isPlayerMoving = (targetVelocity.x != 0);


        animator.SetFloat("speed", Mathf.Abs(targetVelocity.x));

       // BasicAttackAnimation();
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
        if (Input.GetButtonDown("Jump"))
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
        move = Vector2.zero;

        if (isControllingLaser)
        {

        }
        else if (isAttacking && !canMoveWhileAttacking)
        {
            attackMovementDelegate?.Invoke();
        }
        else if (isAttacking && canMoveWhileAttacking)
        {

            DetectBasicHorizontalMovement();
            attackMovementDelegate?.Invoke();
        }
        else
        {

            DetectBasicHorizontalMovement();
            DetectJump();

            DetectDash();
        }


        targetVelocity = move * maxSpeed;

    }

    private void DetectBasicHorizontalMovement()
    {
        move.x = Input.GetAxis("Horizontal");
    }

    private void DetectJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded) //checks if jump button is pressed while grounded
        {
            velocity.y = jumpTakeOffSpeed;
        }

        else if (Input.GetButtonUp("Jump")) // reduces velocity when user lets go of jump button
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }
    }

    private void DetectDash()
    {
        // detect dash

        if (Input.GetButtonDown("DashLeft")) //checks if "a" or left arrow button was pressed
        {

            float timesinceLastLeft = Time.time - lastLeftTime;

            if (timesinceLastLeft <= DOUBLE_PRESS_TIME)
            {
                dashDirection = -1;
                dashTime = 0;//timer for dash
                Debug.Log("dashTime:" + dashTime);
                Debug.Log("double left");//delta tiime
            }
            //Double click
            else
            {
                //Normal Click
                lastLeftTime = Time.time;
            }


        }

        if (Input.GetButtonDown("DashRight")) //checks if "d" or right arrow button was pressed
        {

            float timesinceLastRight = Time.time - lastRightTime;

            if (timesinceLastRight <= DOUBLE_PRESS_TIME)
            {
                dashDirection = 1;
                dashTime = 0;//timer for dash
                Debug.Log("dashTime:" + dashTime);

                Debug.Log("double right");//debug delta time
            }
            //double click
            else
            {
                //Normal Click
                lastRightTime = Time.time;
            }
        }

        if (dashTime < startDashTime)
        {
            dashMultiplier = (1 - initialDashMultiplier) / startDashTime * dashTime + initialDashMultiplier;
            move.x = dashDirection * dashMultiplier;
            dashTime += Time.deltaTime; //Decrease time counter
            IgnoreEnemyCollision(true);
        }
        else
        {
            IgnoreEnemyCollision(false);
        }
    }






    private void IgnoreEnemyCollision(bool value)
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, value);
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }


    #endregion





    protected void AttackQueueManager()
    {
        if (comboQueue.Count != 0){
            
            StartCoroutine(comboQueue.Dequeue());
        }
    }

    IEnumerator TestRoutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        isAttacking = false;
        timeOfLastAttack = Time.time;
        AttackQueueManager();
    }

    protected void AttackQueuer()
    {
        currentCombo = string.Concat(currentCombo, lastButtonPressed);
        Debug.Log(comboCount + "  " + currentCombo);

        //comboQueue.Enqueue(DoAttack("HEAD_DRILL"));
        if (comboCount ==1 && lastButtonPressed == "s")
        {

        }
        else if (comboCount == 2  && lastButtonPressed == "s")
        {

        }
        else if (comboCount == 3 && lastButtonPressed == "s")
        {

        }
        else if (comboCount == 1 && lastButtonPressed == "h")
        {

        }
        else if (comboCount == 2 && lastButtonPressed == "h")
        {

        }
        else if (comboCount == 3 && lastButtonPressed == "h")
        {

        }
        else if (comboCount == 4)
        {
            if(string.Compare(currentCombo.Substring(0,3), "sss") == 0)
            {
                //TROJAN_HORSE asdf
                //comboQueue.Enqueue(DoTrojanHorse());
            }
            else if (string.Compare(currentCombo.Substring(0, 3), "ssh") == 0)
            {
                //SHOCKWAVE asdf
                
            }
            else if (string.Compare(currentCombo.Substring(0, 3), "shs") == 0)
            {
                //FORK_BOMB asdf
            }
            else if (string.Compare(currentCombo.Substring(0, 3), "shh") == 0)
            {
                //BOMB_DASH 
            }
            else if (string.Compare(currentCombo.Substring(0, 3), "hss") == 0)
            {
                //LASER_GEYSER
                //comboQueue.Enqueue(DoLaserGeyser());
            }
            else if (string.Compare(currentCombo.Substring(0, 3), "hsh") == 0)
            {
                //RAIN_DROP asdf
            }
            else if (string.Compare(currentCombo.Substring(0, 3), "hhs") == 0)
            {
                //SLIDE_DASH
            }
            else if (string.Compare(currentCombo.Substring(0, 3), "hhh") == 0)
            {
                //HEAD_DRILL asdf
            }
        }
    }


    //  Queue<IEnumerator> comboQueue;
    //  protected bool comboQueueLock;

    // detect combo input
    protected void DetectCombo()
    {

        if (AttackPressed())
        {
            if (!comboQueueAlive)
            {
                currentCombo = "";
                comboQueueAlive = true;
                comboCount = 1;
                AttackQueuer();
                AttackQueueManager();
            }
            else if (comboQueueAlive)
            {
                comboCount++;
                AttackQueuer();
                if (!isAttacking)
                {
                    AttackQueueManager();
                }
            }

        }

        if (comboQueueAlive && isAttacking)
        {
            timeOfLastAttack = Time.time;
        }
        else if (comboQueueAlive && !isAttacking && Time.time - timeOfLastAttack > COMBO_TIME)
        {
            comboQueueAlive = false;
            currentCombo = "";
        }



        /*
        If Button Pressed{
            If Queue is dead{
                Add first attack
                combo count ++
                call attack queue manager
            }
            
            If Queue is alive{
                If attack is still running{
                    Add it to end of queue
                    comob count ++
                }
                If attack is done{
                    Add it to queue
                    combo count ++
                    Call attack queue manager

                }

                
            }



        }
        If Queue is alive and not attacking {
            tick down combo time
            if combo time is 0{
                kill combo, combo now dead
                clear the combo list
            }
        }



        */

    }







    private bool AttackPressed()
    {
        if (Input.GetButtonDown("TriggerR"))
        {
            lastButtonPressed = "s";
            return true;
        }
        else if (Input.GetButtonDown("TriggerL"))
        {
            lastButtonPressed = "h";
            return true;
        }
        else
        {
            return false;
        }
    }
}
