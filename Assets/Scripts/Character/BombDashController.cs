using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDashController : MonoBehaviour, IPooledObject
{
    // need timer for
    [SerializeField] protected float timeToLive = 5f;
    private float startTime;

    Animator animator;
    GameObject hitbox;
    // float offset;
    
    public void OnObjectSpawn()
    {
        startTime = Time.time;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
        hitbox = ObjectPooler.Instance.SpawnFromPool(Pool.BOMB_BOX, new Vector3(transform.position.x - 1.8f, transform.position.y, 0f), Quaternion.identity);
        // hitbox.setDamage();
    }

    public void EndAttack()
    {
        hitbox.SetActive(false);
        gameObject.SetActive(false);
    }
}
