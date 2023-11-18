using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float delayBetweenSpawns = 2.0f;
    public float asteroidLaunchSpeed = 5;

    bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        if (prefabToSpawn == null)
        {
            Debug.Log("Missing projectile Prefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn) 
        {
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            MoveAndRotate moveAndRotate = GetComponent<MoveAndRotate>();
            
            if (moveAndRotate != null)
            {
                spawnedObject.transform.Rotate(new Vector3(0, 0, -moveAndRotate.compensationAngle));
            }
            canSpawn = false;
           spawnedObject.GetComponent<Rigidbody2D>().velocity = asteroidLaunchSpeed * transform.up;
            Invoke("EnableSpawn", delayBetweenSpawns);
        }
    }

    void EnableSpawn()
    {
        canSpawn = true;
    }
}
