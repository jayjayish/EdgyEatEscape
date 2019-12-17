using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    //resizing from Free Aspect
    private float refWidth = 898.0f;
    private float ratio;
    //5:4 ref
    private float width5 = 454.0f;
    //4:3 ref
    private float width4 = 484.0f;
    //3:2 ref
    private float width3 = 544.0f;
    //16:10 ref
    private float width10 = 581.0f;
    //16:9 ref
    private float width9 = 645.0f;

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
        Debug.Log("1 "+ _text);
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
        Debug.Log("bruh " + text);
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
                Debug.Log(line);
                Debug.Log("Row = " + row);
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
            Debug.Log(line);
            line = "";
        }

        float aspect = Screen.width / Screen.height;
        //check aspect ratio
        if (aspect >= 16 / 9)
        {
            ratio = width9 / refWidth;
        }
        else if (aspect >= 16 / 10)
        {
            ratio = width10 / refWidth;
        }
        else if (aspect >= 3 / 2)
        {
            ratio = width3 / refWidth;
        }
        else if (aspect >= 4 / 3)
        {
            ratio = width4 / refWidth;
        }
        else if (aspect >= 5 / 4)
        {
            ratio = width5 / refWidth;
        }
        else
        {
            ratio = 1.0f;
        }

        words.transform.localScale = new Vector3(words.transform.localScale.x * ratio, words.transform.localScale.y * ratio, words.transform.localScale.z);
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
