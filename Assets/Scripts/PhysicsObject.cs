using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField]
    protected float minGroundNormalY = .65f;
    [SerializeField]
    protected float gravityModifier = 1f;

    protected bool isGrounded;
    protected Vector2 groundNormal;

    protected Vector2 targetVelocity;
    protected Rigidbody2D rb2d;
    protected BoxCollider2D box;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    // used to prevent redundant checking when not moving
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    protected virtual void OnEnable()
        //Stores a component reference
    {
        rb2d = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        contactFilter.useTriggers = false; //Won't check collisions against triggers
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); 
        //use physics2D settings to determine what layers we're going to check collisions against
        contactFilter.useLayerMask = true;
    }

    protected virtual void Awake()
    {


    }

    // Update is called once per frame
    protected virtual void Update()
    {
        targetVelocity = Vector2.zero; // zeros out velocity so we don't use velocity from previous frame
        ComputeVelocity ();
    }


    protected virtual void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime; //gravity

        velocity.x = targetVelocity.x;

        isGrounded = false; //switches to true when collision is found

        Vector2 deltaPosition = velocity * Time.deltaTime;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x); //calculates vector perpendicular to ground normal

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false); //horizonatal movement

        move = Vector2.up * deltaPosition.y;

        Movement(move, true); //vertical movement
    }

    protected virtual void Movement(Vector2 move, bool yMovement)
        //Function for movement
    {
        float distance = move.magnitude; // "distance" is the distance we're attempting to move
        if (distance > minMoveDistance)
        /*
         - This if statement checks for collisions and adjusts velocity during collisions
         - Only checks for collisions if distance we're attempting to move is greater than 
           a minimum distance
         - this way our object is constantly checking for collisions if it's standing still 
        */
        {
            int count = box.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            //checks if colliders in our rigidbody2d is going to overlap with anything
            // int count is the number of contacts that were made 
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]); //copies everything in hitBuffer array into hitBufferList
            }
            for (int i = 0; i < hitBufferList.Count; i++) //checks if surface you collide with is something you can stand on or not
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    isGrounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;

            }

        }
        rb2d.position = rb2d.position + move.normalized * distance; 
    }

    protected virtual void ComputeVelocity() // no implentation in this class, but called in update
    {

    }
}
