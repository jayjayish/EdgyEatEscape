using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : CharacterController
{
    public float speed;

    public float distance;

    public bool moveingRight = true;

    public Transform groundDetection;

    // Update is called once per frame
    protected override void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
    }
}
