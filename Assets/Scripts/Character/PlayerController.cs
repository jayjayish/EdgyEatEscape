using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is for all player movement including jumps, dashes, ducking etc.
// If there are attacks that move the character, it will be in a seperate script




public class PlayerController : CharacterController
{


    [SerializeField] private float maxSpeed = 7;

    private Vector2 move;

    // animation variables
    Animator animator;
    //private bool isPlayerMoving;
    private bool facingLeft = true;

    private int playerLayer;
    private int enemyLayer;
    private int platformLayer;



    #region JumpVariables
    [SerializeField] private float initialJumpTimer = 1f;
    [SerializeField] private float jumpFloatMultiplier = 0.8f;
    [SerializeField] private float jumpFallMultiplier = 0.8f;

    private float jumpTimer = 0f;
    private bool isJumping = false;

    [SerializeField] private float jumpTakeOffSpeed = 7;

    [SerializeField] private float jumpBufferTime = 0.10f;
    private float jumpBufferTimer = 0f;
    private bool jumpBufferBool = false;

    #endregion






    #region DashVariables
    // constants for dash detection
    [SerializeField] private readonly float DOUBLE_PRESS_TIME = .20f;
    [SerializeField] private readonly float totalDashTime = .2f;
    [SerializeField] private readonly float initialDashMultiplier = 5f;
    private float lastLeftTime = 0f;
    private float lastRightTime = 0f;
    private int dashDirection;
    private float dashMultiplier;
    private float dashTime;
    private GameObject laserHolder;

    #endregion

    #region ComboVariables
    private bool canMoveWhileAttacking = false;
    private bool isControllingLaser = false;
    private float headDrillStart = 0f;
    private bool isAttacking = false;
    private string lastButtonPressed = "";


    // 'h' for hardware and 's' for software
    private PlayerComboJSON comboJSON;
    private readonly float COMBO_TIME = 1f;
    private float timeOfLastAttack = 0;
    private string currentCombo = "";     //combo string
    private int comboCount = 0;
    Queue<IEnumerator> comboQueue;
    private bool comboQueueAlive = false;

    //Delegate
    private delegate void attackDelegate();
    private attackDelegate attackMovementDelegate;
    #endregion


    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        comboJSON = GetComponent<PlayerComboJSON>();
        rb2d = GetComponent<Rigidbody2D>();
        comboQueue = new Queue<IEnumerator>();
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        platformLayer = LayerMask.NameToLayer("Platform");
    }

    protected override void Update()
    {
        base.Update();
        UpdateAnimator();
        DetectCombo();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();


    }


    #region Attacks

    protected void AttackQueueManager()
    {
        if (comboQueue.Count != 0)
        {
            StartCoroutine(comboQueue.Dequeue());
        }
    }

    IEnumerator TestRoutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);

        EndAttack();
    }

    protected void AttackQueuer()
    {
        currentCombo = string.Concat(currentCombo, lastButtonPressed);
        Debug.Log(comboCount + "  " + currentCombo);
        //comboQueue.Enqueue(DoAttack("HEAD_DRILL"));

        if (comboCount == 1 && lastButtonPressed == "s")
        {
            comboQueue.Enqueue(TestRoutine());
        }
        else if (comboCount == 2 && lastButtonPressed == "s")
        {
            comboQueue.Enqueue(TestRoutine());
        }
        else if (comboCount == 1 && lastButtonPressed == "h")
        {
            comboQueue.Enqueue(DoAttack("PUNCH"));
        }
        else if (comboCount == 2 && lastButtonPressed == "h")
        {
            comboQueue.Enqueue(DoAttack("BIND"));

        }
        else if (comboCount == 3)
        {
            if (string.Equals(currentCombo, "sss"))
            {
                //TROJAN_HORSE asdf
                comboQueue.Enqueue(DoTrojanHorse());
            }
            else if (string.Equals(currentCombo, "ssh"))
            {
                //SHOCKWAVE asdf
                comboQueue.Enqueue(DoShockwave());
            }
            else if (string.Equals(currentCombo, "shs"))
            {
                //FORK_BOMB
                comboQueue.Enqueue(DoAttack("FORK_BOMB"));
            }
            else if (string.Equals(currentCombo, "shh"))
            {
                //BOMB_DASH 
                comboQueue.Enqueue(DoBombDash());
            }
            else if (string.Equals(currentCombo, "hss"))
            {
                //LASER_GEYSER
                comboQueue.Enqueue(DoLaserGeyser());
            }
            else if (string.Equals(currentCombo, "hsh"))
            {
                //RAIN_DROP asdf
                comboQueue.Enqueue(DoRainDrop());
            }
            else if (string.Equals(currentCombo, "hhs"))
            {
                //SLIDE_DASH
                comboQueue.Enqueue(DoDynamicRam());
            }
            else if (string.Equals(currentCombo, "hhh"))
            {
                //HEAD_DRILL asdf
                //TODO Split the animation into two
                comboQueue.Enqueue(DoHeadDrill());

            }
        }
    }

    // detect combo input
    protected void DetectCombo()
    {
        //If an attack button is pressed
        if (AttackPressed())
        {
            //Do nothing if reached max combo
            if (comboCount == 3)
            {

            }
            //If fresh combo
            else if (!comboQueueAlive)
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

        //If combo is ongoinging
        if (comboQueueAlive && isAttacking)
        {
            timeOfLastAttack = Time.time;
        }
        //If combo is ongoing and the follow up window is closed
        else if (comboQueueAlive && !isAttacking && Time.time - timeOfLastAttack > COMBO_TIME)
        {
            comboQueueAlive = false;
            currentCombo = "";
            comboCount = 0;
        }
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


    #region AttackEnemerators

    //Create a Hitbox
    IEnumerator DoAttack(string hitboxName)
    {
        //Startup
        isAttacking = true;
        animator.SetTrigger(hitboxName);

        yield return new WaitForSeconds( comboJSON.getStartup(hitboxName.ToUpper()) * (1f/60f));

        //Active
        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool(hitboxName.ToUpper(), comboJSON.getPosition(hitboxName.ToUpper()));
        hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage(hitboxName.ToUpper()));

        yield return new WaitForSeconds(comboJSON.getActive(hitboxName.ToUpper()) * (1f / 60f));

        //Endlag
        hitbox.SetActive(false);

        yield return new WaitForSeconds(comboJSON.getEndlag(hitboxName.ToUpper()) * (1f / 60f));

        EndAttack();

    }

    IEnumerator DoLaserGeyser()
    {
        isAttacking = true;
        isControllingLaser = true;
        animator.SetTrigger("LASER_GEYSER");
        yield return new WaitForSeconds(comboJSON.getStartup("LASER_GEYSER") * (1f / 60f));
        Vector3 geyserOffset;
        if (facingLeft){
            geyserOffset = new Vector3(-3f, 0f, 0f);
        } else {
            geyserOffset = new Vector3(3f, 0f, 0f);
        }
        GameObject laser = ObjectPooler.Instance.SpawnFromPool(Pool.LASER_GEYSER, transform.position + geyserOffset, Quaternion.identity);
        GeyserController geyser = laser.GetComponent<GeyserController>();
        geyser.PassPlayerObject(gameObject);
        geyser.OnObjectSpawn();



        //Spawn stuff asofijaseofijaesofj

        while ((Input.GetButton("TriggerL") || Input.GetButton("TriggerR")))
        {
            yield return new WaitForSeconds(1f / 60f);
        }

        geyser.playerInitiateExplode = true;


        //Explode laser if it hasnt been
        isControllingLaser = false;
        EndAttack();
        
    }

    IEnumerator DoTrojanHorse()
    {
        isAttacking = true;
        animator.SetTrigger("TROJAN_HORSE");
        GameObject horse = ObjectPooler.Instance.SpawnFromPool(Pool.TROJAN_HORSE, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        TrojanHorseController horseController = horse.GetComponent<TrojanHorseController>();
        horseController.OnObjectSpawn();
        if (facingLeft)
        {
            horseController.ChangeDirection(-1);
        }
        else
        {
            horseController.ChangeDirection(1);
        }
        //Spawn stuff asofijaseofijaesofj


        yield return new WaitForSeconds(1f);


        //

        EndAttack();

    }

    IEnumerator DoShockwave()
    {
        animator.SetTrigger("SHOCKWAVE");
        isAttacking = true;

        yield return new WaitForSeconds(60f / 60f);

        velocity.y = jumpTakeOffSpeed;
        gravityModifier = 0f;
        yield return new WaitForSeconds(30f / 60f);
        velocity.y = 0;

        yield return new WaitForSeconds((comboJSON.getStartup("SHOCKWAVE") - 90f) * (1f / 60f));

        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool("SHOCKWAVE", comboJSON.getPosition("SHOCKWAVE"));


        yield return new WaitForSeconds(comboJSON.getActive("SHOCKWAVE") * (1f / 60f));

        gravityModifier = initialGravityModifier;

        hitbox.SetActive(false);

        EndAttack();
    }

    IEnumerator DoHeadDrill()
    {
        //Startup
        canMoveWhileAttacking = true;
        isAttacking = true;
        animator.SetTrigger("HEAD_DRILL");
        headDrillStart = Time.time;

        yield return new WaitForSeconds(comboJSON.getStartup("HEAD_DRILL") * (1f / 60f));

        //Active
        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool("HEAD_DRILL", comboJSON.getPosition("HEAD_DRILL"));
        hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage("HEAD_DRILL"));

        float maxDrillTime = comboJSON.getActive("HEAD_DRILL");


        while (((Input.GetButton("TriggerL") || Input.GetButton("TriggerR"))) && ((Time.time - headDrillStart) < (maxDrillTime * 1f / 60f))) 
        {
            yield return new WaitForSeconds(1f / 60f);
        }


        //Endlag
        hitbox.SetActive(false);
        animator.SetTrigger("HEAD_DRILL_END");
        canMoveWhileAttacking = false;
        yield return new WaitForSeconds(comboJSON.getEndlag("HEAD_DRILL") * (1f / 60f));

        EndAttack();
    }

    IEnumerator DoRainDrop()
    {
        //Startup
        canMoveWhileAttacking = true;
        isAttacking = true;
        animator.SetTrigger("RAIN_DROP");
        velocity.y = jumpTakeOffSpeed * 1.2f;

        yield return new WaitForSeconds(comboJSON.getStartup("RAIN_DROP") * (1f / 60f));

        //Active
        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool("RAIN_DROP", comboJSON.getPosition("RAIN_DROP"));
        hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage("RAIN_DROP"));

        yield return new WaitForSeconds(comboJSON.getActive("RAIN_DROP") * (1f / 60f));


        //Endlag
        hitbox.SetActive(false);
        canMoveWhileAttacking = false;
        yield return new WaitForSeconds(comboJSON.getEndlag("HEAD_DRILL") * (1f / 60f));

        EndAttack();
    }

    IEnumerator DoDynamicRam()
    {
        //Startup

        isAttacking = true;
        animator.SetTrigger("DYNAMIC_RAM");
        yield return new WaitForSeconds(15 * (1f / 60f));

        attackMovementDelegate += MoveForward;
        Debug.Log("Ram2");
        yield return new WaitForSeconds((comboJSON.getStartup("DYNAMIC_RAM") - 15) * (1f / 60f));

        //Active
        GameObject hitbox = HitboxPooler.Instance.SpawnFromPool("DYNAMIC_RAM", comboJSON.getPosition("DYNAMIC_RAM"));
        hitbox.GetComponent<PlayerHitboxController>().setDamage(comboJSON.getDamage("DYNAMIC_RAM"));

        yield return new WaitForSeconds(comboJSON.getActive("DYNAMIC_RAM") * (1f / 60f));


        //Endlag
        hitbox.SetActive(false);
        yield return new WaitForSeconds(comboJSON.getEndlag("DYNAMIC_RAM") * (1f / 60f));

        EndAttack();
    }

    IEnumerator DoBombDash()
    {
        float runningTime = 40f;
        isAttacking = true;
        animator.SetTrigger("BOMB_DASH");
        
        for (int i = 0; i < 3; i++)
        {
            attackMovementDelegate += MoveForward;     
            yield return new WaitForSeconds(runningTime * (1f / 60f));  
            attackMovementDelegate = null;
            yield return new WaitForSeconds(runningTime * (1f / 60f));  
            // drop bomb  
        }     

        EndAttack();
    }
    

    //Example Delegate to add to delegate?
    private void JumpUp()
    {
        velocity.y = jumpTakeOffSpeed;
    }

    private void MoveForward()
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

    private void EndAttack()
    {       
        isAttacking = false;
        canMoveWhileAttacking = false;
        attackMovementDelegate = null;
        timeOfLastAttack = Time.time;
        AttackQueueManager();
    }

    public void StopLaserControl(){
        isControllingLaser = false;
    }

    #endregion

    #endregion


    #region Movement

    protected override void ComputeVelocity()
    {
        move = Vector2.zero;

        if (isControllingLaser)
        {
            //Special stuff for lasering
            // player control switches to geyser
           // do nothing?
            
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

    private void DetectJump() //Add a maximum timer to this and make the multiplier a variable TODO
    {
        if (Input.GetButtonDown("Jump") && isGrounded) //checks if jump button is pressed while grounded
        {
            velocity.y = jumpTakeOffSpeed;
            isJumping = true;
            jumpTimer = Time.time;
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded){
            jumpBufferBool = true;
            jumpBufferTimer = Time.time;
        }
        else if (jumpBufferBool && Time.time - jumpBufferTimer < jumpBufferTime && Input.GetButton("Jump") && isGrounded){
            velocity.y = jumpTakeOffSpeed;
            isJumping = true;
            jumpTimer = Time.time;
        }
        else if (jumpBufferBool && ((Time.time - jumpBufferTimer < jumpBufferTime && !Input.GetButton("Jump") || Time.time - jumpBufferTimer >= jumpBufferTime))){
            jumpBufferBool = false;
            jumpBufferTimer = 0f;
        }
        else if (Input.GetButton("Jump") && isJumping &&  initialJumpTimer + jumpTimer > Time.time) // reduces velocity when user lets go of jump button
        {
            gravityModifier = initialGravityModifier * jumpFloatMultiplier;
        }
        else if (!Input.GetButton("Jump") && initialJumpTimer + jumpTimer > Time.time && velocity.y > 0)
        {
            gravityModifier = initialGravityModifier * jumpFallMultiplier;
        }
        else if (!Input.GetButton("Jump") || initialJumpTimer + jumpTimer <= Time.time)
        {
            isJumping = false;
            jumpTimer = 0f;
            gravityModifier = initialGravityModifier;
        }
        else
        {
            gravityModifier = initialGravityModifier;
            isJumping = false;
        }
    }

    private void DetectDash()
    {
        // detect dash

        if (Input.GetButtonDown("DashLeft")) //checks if "a" or left arrow button was pressed
        {
            lastRightTime = 0f;
            //Double click
            if (Time.time - lastLeftTime <= DOUBLE_PRESS_TIME)
            {
                dashDirection = -1;
                dashTime = 0;//timer for dash
            }
            //Normal Click
            else
            {
                
                lastLeftTime = Time.time;
            }
        }

        else if (Input.GetButtonDown("DashRight")) //checks if "d" or right arrow button was pressed
        {
            lastLeftTime = 0f;
            //Double click
            if (Time.time - lastRightTime <= DOUBLE_PRESS_TIME)
            {
                dashDirection = 1;
                dashTime = 0;
            }
            //Normal Click
            else
            {                
                lastRightTime = Time.time;
            }
        }

        if (dashTime < totalDashTime)
        {
            dashMultiplier = (1 - initialDashMultiplier) / totalDashTime * dashTime + initialDashMultiplier;
            move.x = dashDirection * dashMultiplier;
            dashTime += Time.deltaTime; //Decrease time counter
            IgnoreEnemyCollision(true);
        }
        else
        {
            IgnoreEnemyCollision(false);
        }
    }

    #endregion


    #region Animations
    protected void UpdateAnimator()
    {
        //check if player is moving to set idle or moving animations
        LayerTransitions();
        //isPlayerMoving = targetVelocity.x != 0;
        animator.SetFloat("speed", Mathf.Abs(targetVelocity.x));
        Flip(targetVelocity.x);
        JumpAnimation();
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


    private void LayerTransitions()
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

    private void JumpAnimation()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("jump");
        }

        else if (velocity.y < 0 && !isGrounded && !isAttacking)
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


    private void IgnoreEnemyCollision(bool value)
    {
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, value);
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }

    /*
    public void IgnorePlatformCollision(bool value)
    {
        Physics2D.IgnoreLayerCollision(playerLayer, platformLayer, value);
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
    }
    */
}
