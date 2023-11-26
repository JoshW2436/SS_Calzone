using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLerp : MonoBehaviour
{
    public float lerpSpeed = 2f;
    public Transform targetPosition = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position,targetPosition.position,lerpSpeed);
    }
}
