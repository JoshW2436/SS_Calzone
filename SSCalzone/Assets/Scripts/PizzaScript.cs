using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PizzaScript : MonoBehaviour
{
    public GameObject inheritPizza = null;
    public bool inherited = false;
    private bool inited = false;
    public bool cooked = false;
    public bool[] ingredients = new bool[10];
    public int ingredientTotal = 0;
    public GameObject destroyPizza = null;

    // Start is called before the first frame update
    void Start()
    {
        ingredients = new bool[10];
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i] = false;
        }
        PizzaAdded();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PizzaAdded()
    {
        Debug.Log("New Pizza!");
        if (inherited == false)
        {
            if (inheritPizza != null)
            {
                PizzaScript otherPizza = inheritPizza.GetComponent<PizzaScript>();
                cooked = otherPizza.cooked;
                for (int i = 0; i < ingredients.Length; i++)
                {
                    ingredients[i] = otherPizza.ingredients[i];

                }
                if (destroyPizza != null)
                {
                    Destroy(destroyPizza);
                }
            }
            else
            {
                Debug.Log("ohhh... null pizzer");
            }
            inherited = true;
        }
        if (inited == false)
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i] == true)
                {
                    ingredientTotal += 1;
                }
            }
        }
    }
}
