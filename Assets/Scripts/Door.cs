using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject player;
    private bool inRange;
    private float rangeSquare;
    private float range = 2.0f;
    private float offset = 0.1f;
    [SerializeField]
    private string door = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rangeSquare = Mathf.Pow(range, 2);
        inRange = false;
    }

    private void changeScene()
    {
        SceneManager.LoadScene(door); // CHANGE FROM HARD CODE TO VARIABLE LATER
    }

    void Update()
    {
        //check if the object is currently set to out of range, but the player object is now in the circular radius
        if (!inRange && ((Mathf.Pow(player.transform.position.x - gameObject.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - gameObject.transform.position.y, 2)) - offset <= rangeSquare))
        {
            Debug.Log("Open Door?");
            inRange = true;
        }
        //else if the object is set to in range, but the player has now exited the circular radius
        else if (inRange && ((Mathf.Pow(player.transform.position.x - gameObject.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - gameObject.transform.position.y, 2)) - offset > rangeSquare))
        {
            Debug.Log("Leaving Door?");
            inRange = false;
        }
        if (inRange && Input.GetKeyDown(KeyCode.DownArrow)) {
            changeScene();
        }

    }
}
