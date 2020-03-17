using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanHorseController : MonoBehaviour, IPooledObject
{

    private float direction = 0;
    [SerializeField] private float speed = 20f;
    private float spawnTime = 0f;
    [SerializeField] private float liveTime = 3f;
    [SerializeField] private int damage = 20;

    protected string[] listOfObstacleTags = {Tags.SOLID_OBSTACLE };
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTime > liveTime)
        {
            gameObject.SetActive(false);
        }

        transform.position = new Vector3(transform.position.x + speed * direction * Time.deltaTime, transform.position.y, 0f);

    }

    private void FixedUpdate()
    {
       
    }


    public void OnObjectSpawn()
    {
        spawnTime = Time.time;
    }

    public void ChangeDirection(float dir)
    {
        direction = dir;
        transform.localScale = new Vector2(-dir, 1f);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == Tags.ENEMY){
            col.gameObject.GetComponent<EnemyController>().DecrementHealthMagic(damage);
        }
    }


}
