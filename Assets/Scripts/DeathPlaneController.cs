using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{

    [SerializeField] private float touchDamage = 5f;
    [SerializeField] private Vector2 respawnLocation = new Vector2(0f, 0f);


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected void OnTriggerEnter2D(Collider2D col) {
        GameObject hitTarget = col.gameObject;
        if (hitTarget.tag == Tags.PLAYER)
        {
            hitTarget.GetComponent<PlayerController>().FallDown(touchDamage, respawnLocation);
        }

        
    }
}
