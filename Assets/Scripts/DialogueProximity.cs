using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProximity : MonoBehaviour
{
    //unique items
    public string dialogue;
    public float offX, offY;

    //exclamation point above head
    public GameObject exclam;
    private GameObject exclamInst;

    //control
    private bool talking;
    private bool done;
    private float rangeSquare;
    private bool inRange;
    private GameObject player;

    //set for all objects that use this script
    private float range = 5.0f;
    private float offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        //set private variables
        rangeSquare = Mathf.Pow(range, 2);
        player = GameObject.FindGameObjectWithTag("Player");
        inRange = false;
        done = false;
        talking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //check if the object is currently set to out of range, but the player object is now in the circular radius
        if (!inRange && ((Mathf.Pow(player.transform.position.x - gameObject.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - gameObject.transform.position.y, 2)) - offset <= rangeSquare))
        {
            Debug.Log("In range");
            exclamInst = Instantiate(exclam, new Vector3(gameObject.transform.position.x + offX, gameObject.transform.position.y + offY, gameObject.transform.position.z), Quaternion.identity);
            exclamInst.transform.parent = gameObject.transform;
            inRange = true;
        }
        //else if the object is set to in range, but the player has now exited the circular radius
        else if(inRange && ((Mathf.Pow(player.transform.position.x - gameObject.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - gameObject.transform.position.y, 2)) - offset > rangeSquare))
        {
            Debug.Log("Out of range");
            Object.Destroy(exclamInst);
            inRange = false;
        }

        if (inRange) {
            if (Input.GetButtonDown("Submit") && !talking)
            {
                Debug.Log(dialogue);
                //done = dialogue play function
            }
        }

        if (done) {
            talking = false;
        }
        
    }
}
