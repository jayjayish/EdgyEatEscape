using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField]
    private string door = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            SceneManager.LoadScene(door);
        }
    }
}
