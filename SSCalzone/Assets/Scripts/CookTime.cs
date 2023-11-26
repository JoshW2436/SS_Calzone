using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CookTime : MonoBehaviour
{
    private GameObject destroyObject;
    private bool cooking = false;
    private float cookTime = 7;
    public float finishTime;
    public float startTime;
    public Slider cookBar;
    public TMP_Text cookingNotifs;
    private bool cookingDone = false;
    [HideInInspector]
    public PlayerScript scoreScript = null;

    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("PlayerObj").gameObject.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooking)
        {
            cookBar.gameObject.SetActive(true);
            cookBar.value = (Time.time - startTime )/ cookTime;
            if (Time.time > finishTime)
            {
                cooking = false;
                cookingDone = true;
                GetComponent<PizzaScript>().cooked = true;
            }
        }
        else
        {
            cookBar.gameObject.SetActive(false);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cooking == false && cookingDone == false)
        {
            if (collision.gameObject.tag == "PizzaLoose")
            {
                if (collision.gameObject.GetComponent<PizzaScript>().cooked == false)
                {
                    if (collision.gameObject.GetComponent<PizzaScript>().thrownPizza == true)
                    {
                        scoreScript.DistanceBonus(collision.gameObject.GetComponent<PizzaScript>().distanceThrown);
                    }
                    cooking = true;
                    GetComponent<PizzaScript>().inherited = false;
                    GetComponent<PizzaScript>().inheritPizza = collision.gameObject;
                    GetComponent<PizzaScript>().destroyPizza = collision.gameObject;
                    GetComponent<PizzaScript>().PizzaAdded();

                    for (int i = 0; i < 10; i++)
                    {
                        if (GetComponent<PizzaScript>().ingredients[i] == true)
                        {
                            cookingNotifs.text = cookingNotifs.text + "<br>" + GetIngredientName(i);
                            //Debug.Log("yeah we at " + i.ToString());
                        }
                    }

                    finishTime = Time.time + cookTime;
                    startTime = Time.time;
                }
            }
        }
    }

    private void DestroyPizza()
    {
        Destroy(destroyObject);
    }

    public void ClearPizza()
    {
        cookingNotifs.text = "Oven";
        cookingDone = false;
        cooking = false;
        GetComponent<PizzaScript>().cooked = false;
        for (int i = 0; i < GetComponent<PizzaScript>().ingredients.Length; i++)
        {
            GetComponent<PizzaScript>().ingredients[i] = false;
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

    // add thing that checks for hit pizza thrown and if its distance is big add to exp
}
