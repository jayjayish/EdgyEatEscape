using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cpManager : MonoBehaviour
{
    private GameObject player;
    private GameObject CheckPoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCheckPoint(GameObject cp) {
        Debug.Log("CheckPoint Get");
        CheckPoint = cp;
    }

    public GameObject getCheckPoint() {
        return CheckPoint;
    }

    public void callCP() {
        player.transform.position = new Vector3(CheckPoint.transform.position.x, CheckPoint.transform.position.y, player.transform.position.z);
    }
}
