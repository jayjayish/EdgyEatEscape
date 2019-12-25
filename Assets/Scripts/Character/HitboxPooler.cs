using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxPooler : MonoBehaviour {
    [SerializeField]
    private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;


    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
    }

   // #region Singleton
    public static HitboxPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
  // #endregion Singleton

    


	// Use this for initialization
	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            GameObject obj = Instantiate(pool.prefab);
            obj.transform.parent = this.transform.parent;
            obj.SetActive(false);
            objectPool.Enqueue(obj);
            poolDictionary.Add(pool.tag,  objectPool);
        }
	}

    public GameObject SpawnFromPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + " does not exist");
            return null;
        }
       
        GameObject newObj =   poolDictionary[tag].Dequeue();
        newObj.SetActive(true);
        newObj.transform.localPosition = position;
       // newObj.transform.rotation = Quaternion.identity;

        poolDictionary[tag].Enqueue(newObj);

        return newObj;
    }


}
