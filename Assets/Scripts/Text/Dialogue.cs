using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    //resizing based on dialogue box
    private float refWidth = 272.0f;
    private float refHeight = 520.0f;
    private float screenHeight;
    private float screenWidth;

    //real objects from files
    public GameObject empty;
    public Text printer;

    //instantiated objects
    private GameObject hanger;
    private Text words;

    //Lets the programs above know that the dialogue has ended
    public bool finished;

    //variables
    //limit per textbox
    private int limit = 38;

    //locations
    private float[] rowY = new float[4] { 0.4f, -0.3f, -1.0f, -1.7f };
    private float rowX = -9.8f;
    private float rowZ = -1f;

    private bool dialogueExists;

    //for reading letters
    int index = 0;
    string nextWord = "";
    string line = "";

    private string text = "";

    public void setText(string _text) {
        // Debug.Log("1 "+ _text);
        this.text = (string) _text.Clone();
        DialogueChain();
    }

    //bool returns if dialogue is finished
    private void DialogueChain() {
        //set variables and inst objects
        finished = false;
        words = Instantiate(printer, gameObject.transform.position, Quaternion.identity);
        hanger = Instantiate(empty, gameObject.transform.position, Quaternion.identity);
        words.transform.parent = hanger.transform;
        hanger.transform.parent = gameObject.transform;
        // Debug.Log("bruh " + text);
        int row = 0;

        while (index < text.Length && row < rowY.Length)
        {

            while (text[index] != ' ' && index < text.Length)
            {
                nextWord += text[index];
                index++;
                if (index >= text.Length)
                {
                    break;
                }
            }
            index++;

            if (nextWord.Length + line.Length > limit)
            {
                // Debug.Log(line);
                // Debug.Log("Row = " + row);
                words.printer(line, rowX, rowY[row], rowZ);
                line = "" + nextWord;
                if (line.Length > 0)
                {
                    line += ' ';
                }
                nextWord = "";
                row++;
            }

            else if (!nextWord.Equals(" "))
            {
                line += nextWord + ' ';
                nextWord = "";
            }

        }
        if (line.Length > 0 && row < rowY.Length)
        {
            words.printer(line, rowX, rowY[row], rowZ);
            // Debug.Log(line);
            line = "";
        }

        // working on rescaling TODO
        words.transform.localScale = new Vector3(words.transform.localScale.x * (screenWidth / refWidth), words.transform.localScale.y * (screenWidth / refWidth), words.transform.localScale.z);
        
        dialogueExists = true;
        Debug.Log("dialogue Exists = " + dialogueExists);
    }

    // Start is called before the first frame update
    void Start()
    {
        finished = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool touch = Input.GetButtonDown("Submit");
        if ((index < text.Length || !(line.Length == 0))&& !dialogueExists)
        {
            DialogueChain();
        }
        if (touch && dialogueExists)
        {
            Debug.Log("destroy words");
            Object.Destroy(hanger);
            Debug.Log("index = " + index);
            Debug.Log("text length = " + text.Length);
            Debug.Log("line = " + line);
            dialogueExists = false;
            if (index >= text.Length && line.Length == 0) {
                finished = true;
                Debug.Log("thats all for now folks");
                Debug.Log("finished = " + finished);
            }
        }

    }
}
