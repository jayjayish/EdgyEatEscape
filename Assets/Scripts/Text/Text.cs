using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour
{
    private Letters letters;

    private float x;
    private float y;
    //each pixel is approximately 0.015f;
    private float deltaX2 = 0.030f;
    private float deltaX3 = 0.045f;
    private float deltaX = 0.060f;
    private float deltaX5 = 0.075f;
    private float deltaX6 = 0.090f;
    private float space = 0.030f;
    
    public void printer(string _text, float initX, float initY, float z)
    {
        letters = GameObject.FindObjectOfType<Letters>();
        x = initX;
        y = initY;
        string text = (string) _text.Clone();
        text = text.ToUpper();
        Debug.Log(text);
        
        for (int i = 0; i < text.Length; i++) {
            if (text[i] != ' ')
            {
                //printing letter
                Debug.Log("Letter = " + text[i]);
                GameObject letter = Instantiate((GameObject)letters.letters[(char)text[i]], new Vector3(x, y, z), Quaternion.identity);
                letter.transform.parent = gameObject.transform;

                //spacing for next letter
                if (text[i] == 'G' || text[i] == 'N' || text[i] == 'Q')
                {
                    x = x + deltaX5;
                }

                else if (text[i] == 'M' || text[i] == 'W' || text[i] == 'X')
                {
                    x = x + deltaX6;
                }

                else if (text[i] == '(' || text[i] == ')' || text[i] == ',' || text[i] == '.') {
                    x = x + deltaX3;
                }

                else if (text[i] == '!') {
                    x = x + deltaX2;
                }

                else
                {
                    x = x + deltaX;
                }
            }

            else {
                x = x + space;
            }
        }
    }

    //when Object is destroyed
    private void OnDestroy()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
