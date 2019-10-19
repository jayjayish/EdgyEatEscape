using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    GameObject player;

    [Range(0, 1)]
    [SerializeField]
    public float speed;

    private float offsets = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (((int)gameObject.transform.position.x > (int)player.transform.position.x + offsets) ||
            ((int)gameObject.transform.position.x < (int)player.transform.position.x - offsets) ||
            ((int)gameObject.transform.position.y > (int)player.transform.position.y + offsets) ||
            ((int)gameObject.transform.position.y < (int)player.transform.position.y - offsets)) {

            float x = player.transform.position.x;
            float y = player.transform.position.y;
            float hor = 0;
            float ver = 0;

            if (gameObject.transform.position.x < x)
            {
                hor = speed;
            }
            else if (gameObject.transform.position.x > x) {
                hor = -speed;
            }

            if (gameObject.transform.position.y < y)
            {
                ver = speed;
            }
            else if (gameObject.transform.position.y > y)
            {
                ver = -speed;
            }

            gameObject.transform.position += new Vector3(hor, ver, 0);

        }
    }
}
