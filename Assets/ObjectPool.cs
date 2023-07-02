using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int poolSize = 10;

    public ObjectPool()
    {
    }

    public ObjectPool(List<GameObject> prefabs)
    {
        foreach (GameObject prefab in prefabs)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObj = GameObject.Instantiate(prefab);
                newObj.SetActive(false);
                pooledObjects.Add(newObj);
            }
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
