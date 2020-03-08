using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserController : MonoBehaviour, IPooledObject
{
    // need timer for
    [SerializeField] protected float timeToLive = 5f;
    [SerializeField] protected float speed = 3f;
    private float startTime;
    public bool playerInitiateExplode = false;

    Animator animator;
    PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        //animator.GetComponent<Animator>();

    }

    public void OnObjectSpawn()
    {
        startTime = Time.time;
        playerInitiateExplode = false;
    }

    public void PassPlayerObject(GameObject pla){
        player = pla.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInitiateExplode)
        {
            //animator.SetTrigger("TIME_UP");
            ExplodeLaser();
        }
        else if (Time.time - startTime < timeToLive){
            float x = Input.GetAxis("Horizontal");
            transform.position += new Vector3(x * Time.deltaTime * speed, 0f, 0f);
        }
        else
        {
            //animator.SetTrigger("TIME_UP");
            ExplodeLaser();
        }

    }


    public void ExplodeLaser()
    {
        //Spawn Laser box
        player.StopLaserControl();
        gameObject.SetActive(false); //Change this later?? TODO
    }
}
