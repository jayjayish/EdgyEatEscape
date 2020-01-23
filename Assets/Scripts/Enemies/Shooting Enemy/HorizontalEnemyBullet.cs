using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemyBullet : PhysicsObject
{
    [SerializeField]
    float moveSpeed = 7f;

    PhysicsObject target;

    private float moveX;

    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerController>();
        moveX = Mathf.Sign(target.transform.position.x - transform.position.x) * moveSpeed;
        velocity = new Vector2(moveX, 0f);
        Destroy(gameObject, 3f);

    }

    protected override void ComputeVelocity()
    {

    }

    void onTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Debug.Log("Hit");
            Destroy(gameObject);
        }
    }

}

