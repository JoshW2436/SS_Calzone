using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndRotate : MonoBehaviour
{
    //read the position
    public Transform targetTransform;

    //access position from the game object
    public GameObject targetObject;

    //store point in the world
    public Vector3 targetPosition;

    public Vector3 directionVector;

    public float speedPerSecond = 2;

    public bool shouldMove = true;

    public bool useActivationRange = false;
    public float activationRange = 2.0f;

    public bool shouldRotate = true;
    public float compensationAngle = 0;


    public bool targetMouse = false;
    // Start is called before the first frame update
    void Start()
    {
        if (targetTransform == null)
        {
            targetTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetMouse)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            targetPosition = mouseWorldPosition;
        }
        else if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }
        else if (targetObject != null)
        {
            targetPosition = targetObject.transform.position;
        }

        directionVector = targetPosition - transform.position;
        //transform.position += directionVector*speedPerSecond*Time.deltaTime;
        float distanceToTarget = Vector3.Distance(targetTransform.position, transform.position);
        float distanceToTarget2 = directionVector.magnitude;
        directionVector.Normalize();
        if (useActivationRange && distanceToTarget2 > activationRange)
        {
            return;
        }
        if (shouldMove)
        { 
        transform.position += directionVector * speedPerSecond * Time.deltaTime;
        }

        if (!shouldRotate)
        {
            return;
        }

        // float angle = Mathf.Atan2(directionVector.y, directionVector.x)*Mathf.Rad2Deg;
        // transform.rotation=Quaternion.Euler(0,0,angle+compensationAngle);
        transform.right = directionVector;
        transform.Rotate(new Vector3(0,0,compensationAngle));
    }


}
