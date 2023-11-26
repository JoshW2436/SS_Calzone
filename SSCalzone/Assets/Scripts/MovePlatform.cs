using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MovePlatform : MonoBehaviour
{
    public Transform[] movePoints;
    public int listPosition;
    public float moveSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movePoints != null)
        {
            if (Vector2.Distance(transform.position,movePoints[listPosition].position) < 0.1*moveSpeed)
            {
                transform.position = movePoints[listPosition].position;
                if (listPosition+1 != movePoints.Length)
                {
                    listPosition += 1;
                    //Debug.Log("next point");
                }
                else
                {
                    listPosition = 0;
                    //Debug.Log("back to the beginning");
                }
                
            }
        }
    }

    private void FixedUpdate()
    {
        if (movePoints != null)
        {
            Vector3 dir = (movePoints[listPosition].position - transform.position).normalized;
            transform.position += dir * moveSpeed *Time.deltaTime;
            //Quaternion dir = Vector2.Angle();
            //Debug.Log("we r moving towards "+ movePoints[listPosition].position.ToString()+" from "+transform.position.ToString()+"and the dir is "+dir.ToString());
            //transform.position = Vector2.Lerp(transform.position,movePoints[listPosition].position,moveSpeed*Time.deltaTime);
        }
    }
}
