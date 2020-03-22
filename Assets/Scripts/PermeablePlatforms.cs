using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermeablePlatforms : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("Check");
        player = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y >= gameObject.transform.position.y)
        {
            //Debug.Log("Solid");
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            //Debug.Log("Permeable");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
