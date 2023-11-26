using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPages : MonoBehaviour
{
    // Start is called before the first frame update
    public int myPageValue = 1;
    ButtonFunctions manager = null;
    Vector2 startPosition = Vector2.zero;

    private void Start()
    {
        startPosition = transform.position;
        manager = GameObject.Find("ButtonManager").gameObject.GetComponent<ButtonFunctions>();
    }
    // Update is called once per frame
    void Update()
    {
        if (manager.pageValue == myPageValue)
        {
            transform.position = startPosition;
        }
        else
        {
            transform.position = new Vector2(-4000, -4000);
        }
    }
}
