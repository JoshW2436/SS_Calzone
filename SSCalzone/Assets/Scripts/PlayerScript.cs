using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float thrustSpeed = 300;
    public Vector3 targetPosition;
    public float thrusterGas = 100;
    public float thrusterGasMax = 100;
    public float gasSpentPerSecond = 20;
    public float gasFilledPerSecond = 15;
    public float thrusterRechargeCooldown = 1;
    public float stunCooldown = 2;

    private bool stunned = false;
    public Slider thrustBar;

    bool startedFuelCooldown = false;
    bool fillingFuel = false;
    Rigidbody2D rb;
    

    public SpriteRenderer armIcon = null;
    public Sprite blastSprite;
    public Sprite grabSprite;

    public TMP_Text scoreText;
    public Slider timerSlider;

    public float timer = 1200;
    public float timerDefault = 60;

    public float score = 0;
    float lastScore = 0;

    // Start is called before the first frame update
    void Start()
    {
       // thrustDirection = GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Hey you need a rigidbody!");
        }
        timer = timerDefault;
        StartCoroutine(TimerAdvance());
    }

    private void Update()
    {
        if (Input.GetAxis("Cancel") == 1)
        {
            Exit();
        }
        if (stunned == false)
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            targetPosition = mouseWorldPosition;
            if (Input.GetMouseButton(1) == true)
            {
                Vector3 directionVector = targetPosition - transform.position;
                directionVector.Normalize();
                transform.right = directionVector;
            }

            if (fillingFuel == true)
            {
                if (thrusterGas + gasFilledPerSecond * Time.deltaTime < thrusterGasMax)
                {
                    thrusterGas += gasFilledPerSecond * Time.deltaTime;
                }
                else
                {
                    thrusterGas = thrusterGasMax;
                    fillingFuel = false;
                }

            }
        }

        //Debug.Log(thrusterGas);

        thrustBar.value = thrusterGas/thrusterGasMax;
        if (scoreText != null)
        {
            if (score != lastScore)
            {
               scoreText.text = "Score: " + score.ToString();
            }
        }
        
        timerSlider.value = timer / timerDefault;
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       // rb.SetRotation();
        if (Input.GetMouseButton(1)==false)
        {
            if (Input.GetMouseButton(0) == true)
            {
                armIcon.enabled = true;
                armIcon.sprite = grabSprite;
            }
            else
            {
                armIcon.enabled = false;
            }
            if (startedFuelCooldown == false && thrusterGas < thrusterGasMax)
            {
                Debug.Log("restarting fuel countdown, not pressing mouse button");
                Invoke("RestartFuelFill", thrusterRechargeCooldown);
                startedFuelCooldown = true;
            }
        }
        else
        {
            if (stunned == false)
            {
                //transform.right = thrustDirection.rotation;
                thrusterGas -= gasSpentPerSecond * Time.deltaTime;


                if (thrusterGas > 0)
                {
                    armIcon.enabled = true;
                    armIcon.sprite = blastSprite;
                    fillingFuel = false;
                    startedFuelCooldown = false;
                    rb.AddForce(transform.right * -thrustSpeed, 0);
                }
                else
                {
                    if (startedFuelCooldown == false)
                    {
                        Debug.Log("restarting fuel countdown, no more fuel");
                        Invoke("RestartFuelFill", thrusterRechargeCooldown);
                        startedFuelCooldown = true;
                    }
                }
            }
            else
            {
                armIcon.enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            stunned = true;
            Invoke("Unstun", stunCooldown);
            rb.AddTorque(100);
        }
    }

    private void LateUpdate()
    {
        lastScore = score;
    }

    void RestartFuelFill()
    {
        fillingFuel = true;
       // Debug.Log("START FILLIN ER UP");
    }
    
    void Unstun()
    {
        stunned = false;
    }

    IEnumerator TimerAdvance()
    {
        yield return new WaitForSeconds(1);
        timer -= 1;
        StartCoroutine(TimerAdvance());
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
}
