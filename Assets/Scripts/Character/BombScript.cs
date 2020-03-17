using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    // need timer for
    [SerializeField] protected float timeToLive = 5f;
    private float startTime;

    Animator animator;
    
    public void OnObjectSpawn()
    {
        startTime = Time.time;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime > timeToLive)
        {
            Explode();
        }
    }

    void Explode()
    {
        animator.SetTrigger("TIME_UP");
    }

    public void StartHitBox()
    {

    }

    public void EndAttack()
    {
        
    }
}
