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
    private GameObject destroyPizza = null;

    public AudioSource grabSFX = null;
    public AudioSource missSFX = null;
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
                    int ingredientType = ingTouching.GetComponent<MyIngredient>().myIngredient;
                    if (GetComponentInParent<PizzaScript>().ingredients[ingredientType] == false)
                    {
                        if (grabSFX.enabled)
                        {
                            grabSFX.Play();
                        }
                        GetComponentInParent<PizzaScript>().ingredientTotal += 1;
                        GetComponentInParent<PizzaScript>().ingredients[ingredientType] = true;

                        Destroy(ingTouching.gameObject);
                        ingTouching = null;
                        GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(ingredientType);
                    }
                }
                else
                {

                }
            }
            
        }
        if (Input.GetMouseButton(0) == true)
        {
            if (pzaTouching != null && (Input.GetMouseButtonDown(0) == true))
            {
                if (GetComponentInParent<PizzaScript>().ingredientTotal > 0)
                {
                    GetComponentInParent<PizzaBuildingScript>().ThrowPizza();
                }

                GetComponentInParent<PizzaScript>().inheritPizza = pzaTouching;
                GetComponentInParent<PizzaScript>().Invoke("GetANewPizza", 0.05f);
                //ovnTouching.GetComponent<CookTime>().Invoke("ClearPizza", 0.02f);
                //Debug.Log("starting up!");
                for (int i = 0; i < 10; i++)
                {
                    if (grabSFX.enabled)
                    {
                        grabSFX.Play();
                    }
                    if (pzaTouching.GetComponent<PizzaScript>().ingredients[i] == true)
                    {
                        GetComponentInParent<PizzaScript>().ingredientTotal += 1;
                        GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(i);
                        //Debug.Log("yeah we at " + i.ToString());
                    }
                }
                Invoke("DestroyPizza", 0.06f);
                //Destroy();
                destroyPizza = pzaTouching;
            }
            else
            {
                if (ovnTouching != null && (Input.GetMouseButtonDown(0) == true))
                {
                    
                    if (GetComponentInParent<PizzaScript>().ingredientTotal != 0)
                    {
                        PizzaBuildingScript blip = GetComponentInParent<PizzaBuildingScript>();
                        if (blip.throwSFX.enabled)
                        {
                            blip.throwSFX.Play();
                        }
                        //spriteBoy.GetComponent<SquashStretch>().squashing = true;
                        GameObject newPizza = Instantiate(blip.pizzaPrefab, blip.transform.position + 0.5f * blip.GetComponentInChildren<MoveAndRotate>().directionVector, blip.transform.rotation);
                        newPizza.GetComponent<PizzaScript>().inheritPizza = blip.gameObject;
                        newPizza.GetComponent<Rigidbody2D>().velocity = blip.pizzaLaunchSpeed * blip.GetComponentInChildren<MoveAndRotate>().directionVector;
                        if (PlayerPrefs.GetInt("level", 0) >= 15)
                        {
                            newPizza.GetComponent<PizzaScript>().blastPizza = true;
                        }
                        newPizza.GetComponent<PizzaScript>().thrownPizza = true;
                        blip.Invoke("ClearPizza", 0.02f);
                        blip.thrownPizza = newPizza;
                        blip.thrownPizzaStart = blip.thrownPizza.transform.position;
                    }
                    
                    if (ovnTouching.GetComponent<PizzaScript>().ingredientTotal != 0)
                    {

                        if (grabSFX.enabled)
                        {
                            grabSFX.Play();
                        }
                        GetComponentInParent<PizzaScript>().inheritPizza = ovnTouching;
                        GetComponentInParent<PizzaScript>().Invoke("GetANewPizza", 0.03f);
                        //GetComponentInParent<PizzaScript>().inherited = false;
                        //GetComponentInParent<PizzaScript>().PizzaAdded();
                        ovnTouching.GetComponent<CookTime>().Invoke("ClearPizza", 0.04f);

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
                }
                else
                {
                    if (inbTouching != null)
                    {
                        inbTouching.GetComponent<SquashStretch>().squashing = true;
                        if (GetComponentInParent<PizzaScript>().cooked == false)
                        {
                            
                            int ingredientType = inbTouching.GetComponent<MyIngredient>().myIngredient;
                            if (GetComponentInParent<PizzaScript>().ingredients[ingredientType] == false)
                            {
                                if (grabSFX.enabled)
                                {
                                    grabSFX.Play();
                                }
                                GetComponentInParent<PizzaScript>().ingredientTotal += 1;
                                GetComponentInParent<PizzaScript>().ingredients[ingredientType] = true;
                                GetComponentInParent<PizzaBuildingScript>().pizzaContents.text = GetComponentInParent<PizzaBuildingScript>().pizzaContents.text + "<br>" + GetComponentInParent<PizzaBuildingScript>().GetIngredientName(ingredientType);
                            }
                        }
                        else
                        {
                            if (missSFX.enabled)
                            {
                                missSFX.Play();
                            }
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

    private void DestroyPizza()
    {
        Destroy(destroyPizza.gameObject);
    }
    
}
