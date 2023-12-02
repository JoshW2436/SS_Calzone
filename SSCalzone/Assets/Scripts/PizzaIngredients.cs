using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PizzaIngredients : MonoBehaviour
{
    public GameObject[] pizzaLayers;
    public PizzaScript playerPizza = null;
    public Sprite uncookedSprite = null;
    public Sprite cookedSprite = null;
    public bool parentManager = false;
    public OrderingThing orderPizza = null;

    // Start is called before the first frame update
    void Start()
    {
        //pizzaLayers = new GameObject[8];
        if (tag == "PizzaLoose")
        {
            playerPizza = GetComponentInParent<PizzaScript>();
        }
        else
        {
            if (tag == "Oven")
            {
                playerPizza = GetComponentInParent<PizzaScript>();
            }
            else
            {
                playerPizza = GameObject.Find("PlayerObj").gameObject.GetComponent<PizzaScript>();
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (orderPizza != null && parentManager == true)
        {
                GetComponent<SpriteRenderer>().sprite = cookedSprite;
            for (int i = 0; i < pizzaLayers.Length; i++)
            {
                if (orderPizza.myOrder[i] == true)
                {
                    pizzaLayers[i].GetComponent<SpriteRenderer>().enabled = true;

                    if (pizzaLayers[i].GetComponent<PizzaIngredients>().cookedSprite != null)
                    {
                            pizzaLayers[i].GetComponent<SpriteRenderer>().sprite = pizzaLayers[i].GetComponent<PizzaIngredients>().cookedSprite;
                        
                    }

                }
                else
                {
                    pizzaLayers[i].GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
        else
        {
            if (playerPizza != null && parentManager == true)
            {
                if (tag == "Oven")
                {
                    if (playerPizza.ingredientTotal > 0)
                    {
                        GetComponent<SpriteRenderer>().enabled = true;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
                if (playerPizza.cooked)
                {
                    GetComponent<SpriteRenderer>().sprite = cookedSprite;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = uncookedSprite;
                }
                for (int i = 0; i < pizzaLayers.Length; i++)
                {
                    if (playerPizza.ingredients[i] == true)
                    {
                        pizzaLayers[i].GetComponent<SpriteRenderer>().enabled = true;

                        if (pizzaLayers[i].GetComponent<PizzaIngredients>().uncookedSprite != null && pizzaLayers[i].GetComponent<PizzaIngredients>().cookedSprite != null)
                        {
                            if (playerPizza.cooked)
                            {
                                pizzaLayers[i].GetComponent<SpriteRenderer>().sprite = pizzaLayers[i].GetComponent<PizzaIngredients>().cookedSprite;
                            }
                            else
                            {
                                pizzaLayers[i].GetComponent<SpriteRenderer>().sprite = pizzaLayers[i].GetComponent<PizzaIngredients>().uncookedSprite;
                            }
                        }


                    }
                    else
                    {
                        pizzaLayers[i].GetComponent<SpriteRenderer>().enabled = false;
                    }
                }

            }
        }
        

    }
}
