using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyIngredient : MonoBehaviour
{
    public int myIngredient = 0;
    public TMP_Text myName;
    private bool named = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (named == false)
        {
            myName.text = GetIngredientName(myIngredient);
            named = true;
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
}
