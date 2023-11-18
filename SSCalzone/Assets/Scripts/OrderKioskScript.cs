using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderKioskScript : MonoBehaviour
{
    public bool sauce = false;
    public bool cheese = false;
    public bool pepperoni = false;
    public bool sausage = false;
    public bool onion = false;
    public bool bellPeppers = false;
    public bool olives = false;
    public bool pineapple = false;
    private bool levelSet = false;
    //[HideInInspector]
    public bool[] availableIngredients = new bool [8] ;
    
    // Start is called before the first frame update
    void Start()
    {
        availableIngredients = new bool[8];
        
            if (sauce == true)
            {
                availableIngredients[0] = true;
            }
            if (cheese == true)
            {
                availableIngredients[1] = true;
            }
            if (pepperoni == true)
            {
                availableIngredients[2] = true;
            }
            if (sausage == true)
            {
                availableIngredients[3] = true;
            }
            if (onion == true)
            {
                availableIngredients[4] = true;
            }
            if (bellPeppers== true)
            {
                availableIngredients[5] = true;
            }
            if (olives == true)
            {
                availableIngredients[6] = true;
            }
        if (pineapple == true)
        {
            availableIngredients[7] = true;
        }
    }


    // Update is called once per frame
    void Update()
    {

       // if (levelSet == false)
        //{
            
            //int listShorten = 0;
           // for (int i = 0; i < availableIngredients.Length; i++)
            //{
            //    if (availableIngredients[i] == -1)
            //    {
            //        i = listShorten;
            //        break;
            //    }
            //}
            //Array.Resize(ref availableIngredients, listShorten+1);
            levelSet = true;
       // }
       
    }

    
}
