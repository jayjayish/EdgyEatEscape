using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public Dialogue dialogue;
    private Dialogue dialogueInst;

    public bool dialogueFin;

    public void setDialogue(string txt) {
        Debug.Log(txt);
        dialogue.setText(txt);
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueFin = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
