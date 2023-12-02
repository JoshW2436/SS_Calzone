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
    public ParticleSystem smokeSystem = null;

    public AudioSource cookingSound = null;
    public AudioSource hitSound = null;
    public AudioSource doneSound = null;
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
            if (!cookingSound.isPlaying)
            {
                if (cookingSound.enabled)
                {
                    cookingSound.Play();
                }
                
            }
                cookBar.gameObject.SetActive(true);
            float ratio = (Time.time - startTime) / cookTime;
            cookBar.value = ratio;
            cookingSound.pitch = 0.5f + (0.5f * ratio);
            if (Time.time > finishTime)
            {
                GetComponent<SquashStretch>().squashing = true;
                if (doneSound.enabled)
                {
                    doneSound.Play();
                }
                cooking = false;
                cookingDone = true;
                GetComponent<PizzaScript>().cooked = true;
            }
        }
        else
        {
            smokeSystem.Stop();
            cookBar.gameObject.SetActive(false);
            if (cookingSound.isPlaying)
            {
                cookingSound.Stop();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (cooking == false && cookingDone == false)
        {
            if (collision.gameObject.tag == "PizzaLoose")
            {
                if (collision.gameObject.GetComponent<PizzaScript>().cooked == false && collision.gameObject.GetComponent<PizzaScript>().ingredientTotal > 0)
                {
                    if (collision.gameObject.GetComponent<PizzaScript>().thrownPizza == true)
                    {
                        scoreScript.DistanceBonus(collision.gameObject.GetComponent<PizzaScript>().distanceThrown);
                    }
                    if (hitSound.enabled)
                    {
                        hitSound.Play();
                    }
                    smokeSystem.Play();
                    cooking = true;
                    GetComponent<PizzaScript>().inherited = false;
                    GetComponent<PizzaScript>().inheritPizza = collision.gameObject;
                    GetComponent<PizzaScript>().destroyPizza = collision.gameObject;
                    GetComponent<PizzaScript>().PizzaAdded();
                    GetComponent<SquashStretch>().squashing = true;
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
        GetComponent<PizzaScript>().ingredientTotal = 0;
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
