using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;
    [SerializeField] private int initialQuantity = 1;
    private List<GameObject> objPool = new List<GameObject>();

    private void Awake()
    {
        while(initialQuantity>0)
        {
            PopulatePool();
            initialQuantity--;
        }
    }

    private void PopulatePool()
    {
        GameObject newObj = Instantiate(objPrefab);
        newObj.SetActive(false);
        objPool.Add(newObj);
        newObj.transform.parent = transform;
    }

    public virtual void SpawnObject()
    {
        GameObject obj;

        for (int x = 0; x < objPool.Count; x++)
        {
            obj = objPool[x];
            if (!obj.activeSelf)
            {
                goto activate;
            }
        }
        PopulatePool();
        obj = objPool[objPool.Count - 1];

    activate:
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);
        obj.GetComponent<SpawnedObject>().InitializeObject();
    }
}

public interface SpawnedObject
{
    void InitializeObject();
}