using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    private float x, y, newX, newY;
    private float offsetX = 22.5f;
    private float offsetY = 9.0f;
    private float displacement = 22.5f;
    public GameObject background;
    private GameObject[] backgroundInst = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        newX = x;
        newY = y;
        backgroundInst[0] = Instantiate(background, gameObject.transform.position - new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
        backgroundInst[1] = Instantiate(background, gameObject.transform.position, Quaternion.identity);
        backgroundInst[2] = Instantiate(background, gameObject.transform.position + new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);

    }

    void checkX() {
        if (newX >= x + offsetX)
        {
            if (backgroundInst[0].transform.position.x < backgroundInst[1].transform.position.x && backgroundInst[0].transform.position.x < backgroundInst[2].transform.position.x)
            {
                Object.Destroy(backgroundInst[0]);
                if (backgroundInst[1].transform.position.x > backgroundInst[2].transform.position.x)
                {
                    backgroundInst[0] = Instantiate(background, backgroundInst[1].transform.position + new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    backgroundInst[0] = Instantiate(background, backgroundInst[2].transform.position + new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
            }
            else if (backgroundInst[1].transform.position.x < backgroundInst[2].transform.position.x && backgroundInst[1].transform.position.x < backgroundInst[0].transform.position.x)
            {
                Object.Destroy(backgroundInst[1]);
                if (backgroundInst[0].transform.position.x > backgroundInst[2].transform.position.x)
                {
                    backgroundInst[1] = Instantiate(background, backgroundInst[0].transform.position + new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    backgroundInst[1] = Instantiate(background, backgroundInst[2].transform.position + new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
            }
            else
            {
                Object.Destroy(backgroundInst[2]);
                if (backgroundInst[1].transform.position.x > backgroundInst[0].transform.position.x)
                {
                    backgroundInst[2] = Instantiate(background, backgroundInst[1].transform.position + new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    backgroundInst[2] = Instantiate(background, backgroundInst[0].transform.position + new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
            }
            x = newX;
        }

        else if (newX <= x - offsetX)
        {
            if (backgroundInst[0].transform.position.x > backgroundInst[1].transform.position.x && backgroundInst[0].transform.position.x > backgroundInst[2].transform.position.x)
            {
                Object.Destroy(backgroundInst[0]);
                if (backgroundInst[1].transform.position.x < backgroundInst[2].transform.position.x)
                {
                    backgroundInst[0] = Instantiate(background, backgroundInst[1].transform.position - new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    backgroundInst[0] = Instantiate(background, backgroundInst[2].transform.position - new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
            }
            else if (backgroundInst[1].transform.position.x > backgroundInst[2].transform.position.x && backgroundInst[1].transform.position.x > backgroundInst[0].transform.position.x)
            {
                Object.Destroy(backgroundInst[1]);
                if (backgroundInst[0].transform.position.x < backgroundInst[2].transform.position.x)
                {
                    backgroundInst[1] = Instantiate(background, backgroundInst[0].transform.position - new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    backgroundInst[1] = Instantiate(background, backgroundInst[2].transform.position - new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
            }
            else
            {
                Object.Destroy(backgroundInst[2]);
                if (backgroundInst[1].transform.position.x < backgroundInst[0].transform.position.x)
                {
                    backgroundInst[2] = Instantiate(background, backgroundInst[1].transform.position - new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
                else
                {
                    backgroundInst[2] = Instantiate(background, backgroundInst[0].transform.position - new Vector3(displacement, 0.0f, 0.0f), Quaternion.identity);
                }
            }
            x = newX;
        }
    }

    void checkY() {
        if (newY <= y + offsetY && newY >= y - offsetY)
        {
            if (backgroundInst[0] != null && backgroundInst[1] != null && backgroundInst[2] != null)
            {
                float deltaY = newY - y;

                backgroundInst[0].transform.position = new Vector3(backgroundInst[0].transform.position.x, newY - deltaY, backgroundInst[0].transform.position.z);
                backgroundInst[1].transform.position = new Vector3(backgroundInst[1].transform.position.x, newY - deltaY, backgroundInst[1].transform.position.z);
                backgroundInst[2].transform.position = new Vector3(backgroundInst[2].transform.position.x, newY - deltaY, backgroundInst[2].transform.position.z);
            }
        }
        else if (newY <= y - offsetY)
        {
            backgroundInst[0].transform.position = new Vector3(backgroundInst[0].transform.position.x, newY + offsetY, backgroundInst[0].transform.position.z);
            backgroundInst[1].transform.position = new Vector3(backgroundInst[1].transform.position.x, newY + offsetY, backgroundInst[1].transform.position.z);
            backgroundInst[2].transform.position = new Vector3(backgroundInst[2].transform.position.x, newY + offsetY, backgroundInst[2].transform.position.z);
        }
        else if (newY >= y + offsetY)
        {
            backgroundInst[0].transform.position = new Vector3(backgroundInst[0].transform.position.x, newY - offsetY, backgroundInst[0].transform.position.z);
            backgroundInst[1].transform.position = new Vector3(backgroundInst[1].transform.position.x, newY - offsetY, backgroundInst[1].transform.position.z);
            backgroundInst[2].transform.position = new Vector3(backgroundInst[2].transform.position.x, newY - offsetY, backgroundInst[2].transform.position.z);
        }
    }


    // Update is called once per frame
    void Update()
    {
        newX = gameObject.transform.position.x;
        newY = gameObject.transform.position.y;

        checkX();
        checkY();
    }
}
