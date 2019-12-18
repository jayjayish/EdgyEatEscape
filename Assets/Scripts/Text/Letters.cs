using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letters : MonoBehaviour
{
    private char A = 'A';
    public GameObject[] letterObjects;
    public Hashtable letters = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; A + i <= 'Z'; i++) {
            Debug.Log("Storing Letter" + (char)(A + i));
            letters.Add((char) (A + i), (GameObject) letterObjects[i]);
        }

        for (int i = 0; i <= 9; i++) {
            Debug.Log("Storing Number" + (char)i.ToString()[0]);
            letters.Add( (char) i.ToString()[0], (GameObject) letterObjects[26 + i] );
        }
        
        letters.Add('(', (GameObject)letterObjects[36]);
        letters.Add(')', (GameObject)letterObjects[37]);
        letters.Add('+', (GameObject)letterObjects[38]);
        letters.Add('-', (GameObject)letterObjects[39]);
        letters.Add(',', (GameObject)letterObjects[40]);
        letters.Add('=', (GameObject)letterObjects[41]);
        letters.Add('!', (GameObject)letterObjects[42]);
        letters.Add('.', (GameObject)letterObjects[43]);
        letters.Add('?', (GameObject)letterObjects[44]);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
