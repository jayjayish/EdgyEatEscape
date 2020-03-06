using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserController : MonoBehaviour, IPooledObject
{
    // need timer for
    [SerializeField] protected float timeToLive;
    private float startTime;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnObjectSpawn()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < timeToLive){
            float x = Input.GetAxis("Horizontal");
            transform.position += new Vector3(x * Time.deltaTime, 0f, 0f);
        } else {
            animator.SetTrigger("TIME_UP");
        }

    }
}
