using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyIngredient : MonoBehaviour
{
    public int myIngredient = 0;
    public TMP_Text myName;
    private bool named = false;
    public ParticleSystemRenderer myParticleSystem;
    private bool spriteFound = false;

    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (named == false)
        {
            myName.text = GetIngredientName(myIngredient);
            named = true;
        }

        if (tag == "IngredientBox")
        {
            if (spriteFound == false)
            {
                Sprite quickSearch = Resources.Load<Sprite>("Ingredient Windows/" + myName.text + "Window");
                //Debug.Log(quickSearch.ToString());
                if (quickSearch != null)
                {
                    GetComponent<SpriteRenderer>().sprite = quickSearch;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    GetComponent<CircleCollider2D>().radius = 3;
                }

                Material quickSearch2 = Resources.Load<Material>("IngredientMats/" + myName.text + "Mat");
                if (quickSearch2 != null)
                {
                    myParticleSystem.material = quickSearch2;
                }
                spriteFound = true;
            }
            
            
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
