using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeScript : MonoBehaviour
{
    public float timeToLive = 5;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillObject",timeToLive);
    }

    // Update is called once per frame
    void KillObject()
    {
        Destroy(gameObject);
    }
}
