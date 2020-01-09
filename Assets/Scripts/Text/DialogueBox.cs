using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public Dialogue dialogue;
    private Dialogue dialogueInst;
    private bool active;

    public bool dialogueFin;

    public void setDialogue(string txt) {
        if (!active)
        {
            dialogueInst = Instantiate(dialogue, gameObject.transform.position, Quaternion.identity);
            dialogueInst.transform.parent = gameObject.transform;
            active = true;
        }

        Debug.Log(txt);
        dialogueInst.setText(txt);
    }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        dialogueFin = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueInst.finished && !dialogueFin) {
            dialogueFin = true;
        }
    }
}
