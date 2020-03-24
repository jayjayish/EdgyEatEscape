using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoints : MonoBehaviour
{
    private GameObject player;
    private cpManager cpManager;
    [SerializeField]
    private float radius;
    private float rangeSquare;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cpManager = cpManager.FindObjectOfType<cpManager>();
        rangeSquare = Mathf.Pow(radius, 2);
    }

    // Update is called once per frame
    void Update()
    {   // check for if the pointer to the current checkpoint is the same as this checkpoints pointer. Also checks if the player is in range to activate checkpoint
        if (cpManager.getCheckPoint() != gameObject && Mathf.Pow(player.transform.position.x - gameObject.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - gameObject.transform.position.y, 2) <= rangeSquare)
        {
            cpManager.setCheckPoint(gameObject);
        }
    }
}
