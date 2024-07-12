using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    public GameObject[] objectts;
    public List<GameObject> SpawnedObject;
    public Vector3 spawnValues;
    int randomObjects;
    public float spawnObjectCollisionCheckRadius;
    public static Obj instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        StartCoroutine(Spawners());
    }

    public IEnumerator Spawners()
    {
        while (true)
        {
            //selection of objects from cube, cone, cylinder
            randomObjects = Random.Range(0, objectts.Length);

            // selection of position
            Vector3 spawnPosition =  new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 1, Random.Range(-spawnValues.z, spawnValues.z));

            //Checking pos to avoid collosion
            if (!Physics.CheckSphere(spawnPosition, spawnObjectCollisionCheckRadius))
            {
                SpawnedObject.Add(Instantiate(objectts[randomObjects], spawnPosition, Quaternion.identity));
            }
            
            yield return new WaitForSeconds(1);
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        if (SpawnedObject.Count > 4)
        {
            Destroy(SpawnedObject[0].gameObject);
            SpawnedObject.RemoveAt(0);
        }
    }
}
