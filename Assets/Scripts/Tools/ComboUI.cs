using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    private float offset = 0.3f; //initial offset from parent transform (6 ui hexagons)
    private float spacing = 1.6f; //space between each hexagon
    private float vertical = 0.5f; //vertical offset
    
    [SerializeField]
    protected GameObject hardware;

    [SerializeField]
    protected GameObject software;

    private GameObject[] combo = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void drawCombo(int count, string type) {
        if (count == 0)
        {
            for (int i = 0; i < combo.Length; i++)
            {
                if (combo != null)
                {
                    Object.Destroy(combo[i]);
                }
            }
        }

        else {
            if (type == "s")
            {
                combo[count - 1] = Instantiate(software, gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                combo[count - 1] = Instantiate(hardware, gameObject.transform.position, Quaternion.identity);
            }

            combo[count - 1].transform.position += new Vector3(offset + spacing * (count - 1), vertical - 2 * vertical * ((count - 1) % 2), -1);
            combo[count - 1].transform.parent = gameObject.transform;

        }

    }   
}
