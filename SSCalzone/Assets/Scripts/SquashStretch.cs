using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashStretch : MonoBehaviour
{
    public Vector3 squashedScale = Vector3.one;
    public Vector3 stretchedScale = Vector3.one;
    public float squashspeed = 1f;
    public bool repeat = false;

    private int phase = 0;
    public bool squashing = true;
    private Vector3 oldScale = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        oldScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (squashing)
        {
            if (phase == 0)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, squashedScale, squashspeed * Time.deltaTime);
                if ((transform.localScale - squashedScale).magnitude < new Vector3(0.05f, 0.05f, 0.05f).magnitude)
                {
                    phase = 1;
                }
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, stretchedScale, squashspeed * Time.deltaTime);
                if ((transform.localScale - stretchedScale).magnitude < new Vector3(0.05f, 0.05f, 0.05f).magnitude)
                {
                    if (repeat)
                    {
                        phase = 0;
                    }
                    else
                    {
                        phase = 0;
                        squashing = false;
                    }
                }
            }
        }
        else
        {
            if ((transform.localScale - oldScale).magnitude > new Vector3(0.05f, 0.05f, 0.05f).magnitude)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, oldScale, squashspeed * Time.deltaTime);
            }
            else
            {
                transform.localScale = oldScale;
            }
        }    
        
    }
}
