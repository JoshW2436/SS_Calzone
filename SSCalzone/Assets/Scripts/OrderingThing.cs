using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class OrderingThing : MonoBehaviour
{
    public bool[] myOrder;
    public int myDifficulty = 0;
    public TMP_Text orderTxt;
    public TMP_Text orderNotifs;
    public PlayerScript scoreScript;
    // Start is called before the first frame update
    void Start()
    {
        myOrder = new bool[8];
        CreateOrder(myDifficulty);
        scoreScript = GameObject.Find("PlayerObj").gameObject.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) { CreateOrder(myDifficulty); }
    }

    void CreateOrder(int Difficulty)
    {
        orderTxt.text = "";
        orderTxt.text = orderTxt.text + "<br>Sauce\nCheese";
        for (int i = 0;i<myOrder.Length;i++)
        {
            myOrder[i] = false;
        }
        switch (Difficulty)
        {
            case 0:
                {
                    myOrder[0] = true;
                    myOrder[1] = true;
                    //int extrasCount = 0;
                    //for (int i = 0; i < 1; i++)
                    //{
                       // GetOrderItem();
                        
                    //}
                }
                break;

            case 1:
                {
                    myOrder[0] = true;
                    myOrder[1] = true;
                    //int extrasCount = 0;
                    for (int i = 0; i < 1; i++)
                    {

                        GetOrderItem();

                    }
                }
                break;

            case 2:
                {
                    myOrder[0] = true;
                    myOrder[1] = true;
                    //int extrasCount = 0;
                    for (int i = 0; i < 3; i++)
                    {

                        GetOrderItem();

                    }
                }
                break;

            case 3:
                {
                    myOrder[0] = true;
                    myOrder[1] = true;
                    //int extrasCount = 0;
                    for (int i = 0; i < 5; i++)
                    {

                        GetOrderItem();

                    }
                }
                break;
        }
        
    }

    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PizzaLoose")
        {
            //the way this works: we go through the order array, and find each piece. if ingredients[myOrder[i]] = true, good deal.
            if (CheckOrder(collision.gameObject) == true)
            {
                Debug.Log("successful order!");
            }
            Destroy(collision.gameObject);
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PizzaLoose")
        {            
            if (collision.GetComponent<PizzaScript>().cooked)
            {
                if (collision.GetComponent<PizzaScript>().thrownPizza == true)
                {
                    scoreScript.DistanceBonus(collision.GetComponent<PizzaScript>().distanceThrown);
                }
                    if (CheckOrder(collision.gameObject) == true)
                    {
                        StartCoroutine(PizzaMessage("Correct Pizza! Order Fulfilled!"));

                        if (scoreScript != null)
                        {
                            scoreScript.score += 100 * (myDifficulty + 1);
                            scoreScript.experiencePoints += 200 + 100 * (myDifficulty + 1);
                            scoreScript.StartCoroutine("ShowGameMessage","Pizza Completed! +"+(200 + 100 * (myDifficulty + 1)).ToString()+"XP");
                            PlayerPrefs.SetInt("exP", scoreScript.experiencePoints);
                            PlayerPrefs.SetInt("LRP", PlayerPrefs.GetInt("LRP", 0)+1);
                    }
                        CreateOrder(myDifficulty);
                    }
                    else
                    {
                        StartCoroutine(PizzaMessage("Wrong Pizza!"));
                    }
                    Destroy(collision.gameObject);
                
            }
            else
            {
                StartCoroutine(PizzaMessage("Pizzas have to be cooked!"));
                
            }
        }
    }

    public bool CheckOrder(GameObject collision)
    {
        for (int i = 0; i < myOrder.Length; i++)
        {
            if (collision.GetComponent<PizzaScript>().ingredients[i] != myOrder[i])//myOrder[0])
            {
                return false;
            }
        }
        return true;
    }

    public void GetOrderItem()
    {
        
        int randVal = (UnityEngine.Random.Range(2, 8));
        bool foundVal = false;
        bool availVal = false;
        //Debug.Log("Guh" + randVal.ToString());
       // do
        {
            randVal = (UnityEngine.Random.Range(2, 8));
            //if (GetComponentInParent<OrderKioskScript>().availableIngredients[randVal] == true)
            {
                availVal = true;
               /// break;
            }
        }
        //while (availVal == false);

        if (myOrder[randVal] == false)
        {
            myOrder[randVal] = true;
            orderTxt.text = orderTxt.text + "<br>" + GetIngredientName(randVal);
            //Debug.Log("found one!!");
        }
        else
        {
            //do
            {
                randVal = (UnityEngine.Random.Range(2, 8));
                if (myOrder[randVal] == false)
                {
                    myOrder[randVal] = true;
                    orderTxt.text = orderTxt.text + "<br>" + GetIngredientName(randVal);
                    //Debug.Log("did!");
                    //break;
                }
                else
                {
                    //Debug.Log("didnt!");
                }
            }
           // while (foundVal == false);

        }


    }

    public string GetIngredientName(int arrPosition)
    {
        switch (arrPosition)
        {
            case 0:
                {
                    return "Sauce";
                }
            case 1:
                {
                    return "Cheese";
                }
            case 2:
                {
                    return "Pepperoni";
                }
            case 3:
                {
                    return "Sausage";
                }
            case 4:
                {
                    return "Onion";
                }
            case 5:
                {
                    return "Bell Pepper";
                }
            case 6:
                {
                    return "Olives";
                }
            case 7:
                {
                    return "Pineapple";
                }
        }
        return " ";
    }

    IEnumerator PizzaMessage(string message)
    {
        orderNotifs.text = message;
        yield return new WaitForSeconds(5f);
        orderNotifs.text = "";
    }
}
