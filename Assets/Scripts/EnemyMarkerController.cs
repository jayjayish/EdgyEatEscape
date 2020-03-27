using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarkerController : MonoBehaviour
{
    // Start is called before the first frame update
    public string enemyType = "";
    void Start()
    {
        //Debug.Log(enemyType);
        GameObject enemy = ObjectPooler.Instance.SpawnFromPool(enemyType, transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
        enemy.GetComponent<EnemyController>().OnObjectSpawn();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
