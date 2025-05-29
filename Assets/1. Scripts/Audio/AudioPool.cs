using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioPool : MonoBehaviour
{
    [SerializeField]
    private List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    public static AudioPool instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            if (pool != null)
            {
                Queue<GameObject> queue = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab, transform);
                    queue.Enqueue(obj);
                    obj.SetActive(false);
                }
                poolDictionary.Add(pool.tag, queue);
            }
        }
    }

    public GameObject GetPoolObj(string stringTag, Transform parent, Vector3 position)
    {
        if (poolDictionary.ContainsKey(stringTag) && poolDictionary[stringTag] != null)
        {
            GameObject obj = poolDictionary[stringTag].Dequeue();
            if (!obj.activeInHierarchy)
            {
                obj.transform.SetParent(parent);
                obj.transform.position = position;
                obj.SetActive(true);

                poolDictionary[stringTag].Enqueue(obj);
                return obj;
            }
            else
            {
                poolDictionary[stringTag].Enqueue(obj);
            }
        }
        return null;
    }
}
[System.Serializable]
public class Pool
{
    public string tag;
    public int size;
    public GameObject prefab;
}
