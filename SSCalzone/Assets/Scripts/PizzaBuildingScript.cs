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
    }

    private void FixedUpdate()
    {
        
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("THERES BEEN AN IMPACT");
        int ingredientType = collision.GetComponent<MyIngredient>().myIngredient;
        ingredients[ingredientType]=true;
        Destroy(collision.gameObject);
        pizzaContents.text = pizzaContents.text + "<br>"+GetIngredientName(ingredientType);
    }
    */

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
        GameObject newPizza = Instantiate(pizzaPrefab,transform.position+ 0.5f * GetComponentInChildren<MoveAndRotate>().directionVector, transform.rotation);
        newPizza.GetComponent<PizzaScript>().inheritPizza = gameObject;
        newPizza.GetComponent<Rigidbody2D>().velocity = pizzaLaunchSpeed*GetComponentInChildren<MoveAndRotate>().directionVector;
        Invoke("ClearPizza", 0.02f);
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
