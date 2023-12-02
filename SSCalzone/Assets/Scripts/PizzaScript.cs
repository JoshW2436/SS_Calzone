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
    public bool thrownPizza = false;
    public bool blastPizza = false;
    public float distanceThrown = 0;
    public LayerMask asteroidMask;
    public GameObject asteroidBreakPrefab = null;

    public AudioSource hitSound = null;

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
        if (blastPizza)
        {
            if (tag == "PizzaLoose")
            {
                if (Physics2D.OverlapCircle(transform.position, 1.5f, asteroidMask) != null)
                {
                    Debug.Log("Destroy the asteroid nearby");
                    Collider2D[] asteroidsTouching = Physics2D.OverlapCircleAll(transform.position, 1.5f, asteroidMask);
                    foreach (Collider2D a in asteroidsTouching)
                    {
                        Destroy(a.gameObject);
                        Instantiate(asteroidBreakPrefab, a.gameObject.transform.position, Quaternion.identity);
                    }
                }
            }
        }    
        
    }

    public void PizzaAdded()
    {
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
                //Debug.Log("Null Pizza");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "PizzaLoose")
        {
            if (hitSound.enabled)
            {
                hitSound.Play();
            }
            
            //Debug.Log("loose pizza");
            if (collision.gameObject.tag == "Floor")
            {
                thrownPizza = false;
                distanceThrown = 0;
                //Debug.Log("Hit the wall");
            }

        }
        
    }

    public void GetANewPizza()
    {
        inherited = false;
        PizzaAdded();
    }
}
