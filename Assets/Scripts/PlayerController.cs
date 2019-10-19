using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 velocity;

    public float runSpeed = 3f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    //public float dashDistance = 5f;
    public Vector3 Drag;

    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;


    // Start is called before the first frame update
    void Start()
    {
        // rigidbody = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * runSpeed;


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            verticalMove = jumpHeight + gravity;
        } else 
        {
            verticalMove = gravity;
        }
        
        // not used in move at the
        // if (Input.GetButtonDown("Crouch"))
        // {
        //     crouch = true;
        // } else if (Input.GetButtonUp("Crouch")) 
        // {
        //     crouch = false;
        // }

    }

    // FixedUpdate is called at fixed time intervals
    void FixedUpdate()
    {
    }

}
