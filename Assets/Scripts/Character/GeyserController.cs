using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserController : MonoBehaviour, IPooledObject
{
    // need timer for
    [SerializeField] protected float timeToLive;
    private float startTime;
    Animator animator;
    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        animator.GetComponent<Animator>();

    }

    public void OnObjectSpawn()
    {
        startTime = Time.time;

    }

    public void PassPlayerObject(GameObject pla){
        player = pla.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < timeToLive){
            float x = Input.GetAxis("Horizontal");
            transform.position += new Vector3(x * Time.deltaTime, 0f, 0f);
        } else {
            animator.SetTrigger("TIME_UP");
            player.StopLaserControl();
        }

    }
}
