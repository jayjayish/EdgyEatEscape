using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrojanHorseController : MonoBehaviour, IPooledObject
{

    private float direction = 0;
    [SerializeField] private float speed = 4f;
    private float spawnTime = 0f;
    [SerializeField] private float liveTime = 3f;

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
        
    }

    public void ChangeDirection(float dir)
    {
        direction = dir;
        transform.localScale = new Vector2(-dir, 1f);
    }
}
