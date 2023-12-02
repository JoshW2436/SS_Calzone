using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PizzaBuildingScript : MonoBehaviour
{

    public GameObject pizzaPrefab;
    public TMP_Text pizzaContents;
    public PizzaScript myPizza;
    public float pizzaLaunchSpeed = 50;

    public AudioSource throwSFX = null;
    

    public GameObject thrownPizza = null;
    public Vector2 thrownPizzaStart = Vector2.zero;
    Vector2 thrownPizzaCurrent = Vector2.zero;

    public GameObject spriteBoy = null;
    // Start is called before the first frame update
    void Start()
    {
        myPizza = GetComponent<PizzaScript>();
        
        //ingredients[0] = sauce
        //ingredients[1] = cheese
        //ingredients[2] = pepperoni
        //ingredients[3] = sausage
        //ingredients[4] = onion
        //ingredients[5] = bell pepper
        //ingredients[6] = olives
        //ingredients[7] = pineapple
        var texts = FindObjectsOfType<TMP_Text>();
        foreach (var text in texts)
        {
            if (text.name == "myPizzaText")
            {
                pizzaContents = text;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            { ThrowPizza(); }
            
        }

        if (thrownPizza != null)
        {
            if (thrownPizza.GetComponent<PizzaScript>().thrownPizza != false)
            {
                thrownPizzaCurrent = thrownPizza.transform.position;
                thrownPizza.GetComponent<PizzaScript>().distanceThrown = Vector2.Distance(thrownPizzaStart,thrownPizzaCurrent);
            }
        }

        pizzaContents.text = "My Pizza:";
        myPizza.ingredientTotal = 0;
        for (int i = 0; i < 9; i++)
        {
            if (myPizza.ingredients[i] == true)
            {
                myPizza.ingredientTotal += 1;
                pizzaContents.text = pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(i);
            }

        }
    }

    private void FixedUpdate()
    {
        
    }


    public string GetIngredientName(int arrPosition)
    {
        switch(arrPosition)
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

    public void ThrowPizza()
    {
        if (throwSFX.enabled)
        {
            throwSFX.Play();
        }
        //spriteBoy.GetComponent<SquashStretch>().squashing = true;
        GameObject newPizza = Instantiate(pizzaPrefab,transform.position+ 0.5f * GetComponentInChildren<MoveAndRotate>().directionVector, transform.rotation);
        newPizza.GetComponent<PizzaScript>().inheritPizza = gameObject;
        newPizza.GetComponent<Rigidbody2D>().velocity = pizzaLaunchSpeed*GetComponentInChildren<MoveAndRotate>().directionVector;
        if (PlayerPrefs.GetInt("level", 0) >= 15)
        {
            newPizza.GetComponent<PizzaScript>().blastPizza = true;
        }
        newPizza.GetComponent<PizzaScript>().thrownPizza = true;
        Invoke("ClearPizza", 0.02f);
        thrownPizza = newPizza;
        thrownPizzaStart = thrownPizza.transform.position;
    }

    public void ClearPizza()
    {
        
        GetComponent<PizzaScript>().ingredientTotal  = 0;
        GetComponent<PizzaScript>().cooked = false;
        for (int i = 0; i < GetComponent<PizzaScript>().ingredients.Length; i++)
        {
            GetComponent<PizzaScript>().ingredients[i] = false;
        }
        
        pizzaContents.text = "My Pizza:";
    }
}
