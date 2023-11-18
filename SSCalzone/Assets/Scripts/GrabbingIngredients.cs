using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GrabbingIngredients : MonoBehaviour
{
    private bool grabbingOn = false;
    private GameObject ingTouching = null;
    private GameObject ovnTouching = null;
    private GameObject pzaTouching = null;
    private GameObject inbTouching = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) == true)
        {
            if (grabbingOn == false)
            {
                GetComponent<SpriteRenderer>().color = Color.green;
            }
            grabbingOn = true;
        }
        else
        {   
            if (grabbingOn == true)
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            grabbingOn = false;
        }
        if (Input.GetMouseButton(0) == true)
        {
            if (ingTouching != null)
            {
                if (GetComponentInParent<PizzaScript>().cooked == false)
                { 
                    Debug.Log("THERES BEEN AN IMPACT");
                    int ingredientType = ingTouching.GetComponent<MyIngredient>().myIngredient;
                    if (GetComponentInParent<PizzaScript>().ingredients[ingredientType] == false)
                    {
                        GetComponentInParent<PizzaScript>().ingredientTotal += 1;
                        GetComponentInParent<PizzaScript>().ingredients[ingredientType] = true;

                        Destroy(ingTouching.gameObject);
                        ingTouching = null;
                        GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(ingredientType);
                    }
                }
            }
            
        }
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (pzaTouching != null)
            {
                if (GetComponentInParent<PizzaScript>().ingredientTotal != 0)
                {
                    GetComponentInParent<PizzaBuildingScript>().ThrowPizza();
                }

                GetComponentInParent<PizzaScript>().inheritPizza = pzaTouching;
                GetComponentInParent<PizzaScript>().inherited = false;
                GetComponentInParent<PizzaScript>().PizzaAdded();
                //ovnTouching.GetComponent<CookTime>().Invoke("ClearPizza", 0.02f);
                //Debug.Log("starting up!");
                for (int i = 0; i < 10; i++)
                {
                    if (pzaTouching.GetComponent<PizzaScript>().ingredients[i] == true)
                    {
                        GetComponentInParent<PizzaScript>().ingredientTotal += 1;
                        GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(i);
                        //Debug.Log("yeah we at " + i.ToString());
                    }
                }
                Destroy(pzaTouching.gameObject);
                pzaTouching = null;
            }
            else
            {
                if (ovnTouching != null)
                {
                    if (GetComponentInParent<PizzaScript>().ingredientTotal != 0)
                    {
                        GetComponentInParent<PizzaBuildingScript>().ThrowPizza();
                    }
                    GetComponentInParent<PizzaScript>().inheritPizza = ovnTouching;
                    GetComponentInParent<PizzaScript>().inherited = false;
                    GetComponentInParent<PizzaScript>().PizzaAdded();
                    ovnTouching.GetComponent<CookTime>().Invoke("ClearPizza", 0.02f);
                    //Debug.Log("starting up!");
                    for (int i = 0; i < 9; i++)
                    {
                        if (ovnTouching.GetComponent<PizzaScript>().ingredients[i] == true)
                        {
                            GetComponentInParent<PizzaScript>().ingredientTotal += 1;
                            GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(i);
                        }

                    }
                    if (ovnTouching.GetComponent<PizzaScript>().cooked == true)
                    {
                        GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>COOKED!";
                    }
                }
                else
                {
                    if (inbTouching != null)
                    {
                        int ingredientType = inbTouching.GetComponent<MyIngredient>().myIngredient;
                        if (GetComponentInParent<PizzaScript>().ingredients[ingredientType] == false)
                        {
                            GetComponentInParent<PizzaScript>().ingredientTotal += 1;
                            GetComponentInParent<PizzaScript>().ingredients[ingredientType] = true;
                            GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(ingredientType);
                        }
                    }
                }
            }
            
        }

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PizzaLoose")
        {
            pzaTouching = collision.gameObject;
        }
        else
        { 
            if (collision.tag == "Ingredient")
            {
                ingTouching = collision.gameObject;
            }
            else
            {
                if (collision.tag == "Oven")
                {
                    ovnTouching = collision.gameObject;
                }
                else
                {
                    if (collision.tag == "IngredientBox")
                    {
                        inbTouching = collision.gameObject;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ingredient")
        {
            ingTouching = null;
        }
        if (collision.tag == "Oven")
        {
            ovnTouching = null;
        }
        if (collision.tag == "PizzaLoose")
        {
            pzaTouching = null;
        }
        if (collision.tag == "IngredientBox")
        {
            inbTouching = null;
        }
    }

    
}
