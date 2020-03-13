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
    GameObject hitbox;
    float width;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        width = GetComponent<SpriteRenderer>().bounds.size.x / 7f;
        // find hitbox??
        // hitbox = ???
    }

    public void OnObjectSpawn()
    {
        startTime = Time.time;
        playerInitiateExplode = false;
        //hitbox.SetActive(false); 
    }

    public void PassPlayerObject(GameObject pla){
        player = pla.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInitiateExplode)
        {
            ExplodeLaser();
        }
        else if (Time.time - startTime < timeToLive){
            float x = Input.GetAxis("Horizontal");
            transform.position += new Vector3(x * Time.deltaTime * speed, 0f, 0f);
        }
        else
        {
            ExplodeLaser();
        }

    }


    public void ExplodeLaser()
    {
        // TODO Spawn Laser box
        animator.SetTrigger("TIME_UP");
        // time for active store in json or in this file?
        // yield return new WaitForSeconds(20f * (1f / 60f));
        


        player.StopLaserControl();
    }


    

    public void StartHitbox()
    {
        //hitbox.SetActive(true);
        hitbox = ObjectPooler.Instance.SpawnFromPool(Pool.LASER_GEYSER_BOX, new Vector3(transform.position.x-width, transform.position.y, 0f), Quaternion.identity);
    }

    public void StopHitbox()
    {        
        hitbox.SetActive(false);
    }

    public void StopGeyser()
    {
        gameObject.SetActive(false);
        player.StopLaserControl();
    }
}
