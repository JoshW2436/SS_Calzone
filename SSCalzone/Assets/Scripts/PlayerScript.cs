using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Collections.AllocatorManager;

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
    public float distanceForBonus = 5;
    public float asteroidCooldown = 8;

    private bool stunned = false;
    

    bool startedFuelCooldown = false;
    bool fillingFuel = false;
    Rigidbody2D rb;
    
    

    public SpriteRenderer armIcon = null;
    public Sprite blastSprite;
    public Sprite grabSprite;

   

    public float timer = 1200;
    public float timerDefault = 60;

    public float score = 0;
    float lastScore = 0;

    [HideInInspector]
    public int experiencePoints = 0;

    public int levelAddedExp = 500;
    public int levelOneExp = 1000;
    public int currentLevel = 1;
    public int nextLevelExp = 1000;

    bool deflectAvailable = false;
    //float deflectTimerStartTime = 0f;
    //float deflectTimerEndTime = 0f;
    float deflectTimer = 0f;
    bool deflectUnlocked = false;

    public LayerMask asteroidMask;


    public TMP_Text expText = null;
    public TMP_Text scoreText;
    public TMP_Text lvlTxt;
    public TMP_Text messageText;

    public Slider timerSlider;
    public Slider expSlider;
    public Slider defSlider;
    public Slider thrustBar;

    public AudioSource whackSFX;
    public AudioSource lvlUpSFX;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("LRP", 0);
        PlayerPrefs.SetInt("LRD", 0);
        if (SceneManager.GetActiveScene().name == "Easy")
        {
            PlayerPrefs.SetInt("LRD", 0);
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Medium")
            {
                PlayerPrefs.SetInt("LRD", 1);
            }
            else
            {
                if (SceneManager.GetActiveScene().name == "Hard")
                {
                    PlayerPrefs.SetInt("LRD", 2);
                }
            }
        }
            
        // thrustDirection = GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.Log("Hey you need a rigidbody!");
        }
        timer = timerDefault;
        StartCoroutine(TimerAdvance());
        thrusterGasMax = PlayerPrefs.GetInt("thrusterMax", 100);
        gasFilledPerSecond = PlayerPrefs.GetInt("thrusterFillSpeed", 20);
        experiencePoints = PlayerPrefs.GetInt("exP", 0);
        currentLevel = PlayerPrefs.GetInt("level", 0);
        thrustSpeed = PlayerPrefs.GetInt("thrusterSpeed", 10);
        asteroidCooldown = PlayerPrefs.GetFloat("deflectCooldown", 8);
        var sliders = FindObjectsOfType<Slider>();
        foreach (var slider in sliders)
        {
            if (slider.name == "TimeBar")
            {
                timerSlider = slider;
            }

            if (slider.name == "ExpBar")
            {
                expSlider = slider;
            }

            if (slider.name == "DeflectBar")
            {
                defSlider = slider;
            }

            if (slider.name == "ThrustGas")
            {
                thrustBar = slider;
            }
        }
        var texts = FindObjectsOfType<TMP_Text>();
        foreach (var text in texts)
        {
            if (text.name == "ExperienceText")
            {
                expText = text;
            }
            if (text.name == "LevelText")
            {
                lvlTxt = text;
            }
            if (text.name == "ScoreText")
            {
                scoreText = text;
            }

            if (text.name == "MessageText")
            {
                messageText = text;
            }
        }
        messageText.text = "";
        //timerText.text = "Time Remaining: " + timeLeft.ToString();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            LevelUp();
            experiencePoints = 0;
            PlayerPrefs.SetInt("exP", experiencePoints);
        }
        if (!deflectUnlocked)
        {
            if (currentLevel >= 10)
            {
                deflectUnlocked = true;
            }
        }
        
        PlayerPrefs.SetInt("nExP", levelOneExp + (levelAddedExp * (currentLevel)));
        expText.text = "Experience Points:" + PlayerPrefs.GetInt("exP", 0).ToString();
        lvlTxt.text = "Level " + PlayerPrefs.GetInt("level", 0).ToString();
        if (PlayerPrefs.GetInt("exP", 0) >= levelOneExp+(levelAddedExp*currentLevel))//level one exp +(levelAddedExp*currentLevel)
        {
            LevelUp();
            
        }
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
        expSlider.value = (float)experiencePoints / ((float)levelOneExp + ((float)levelAddedExp * (float)currentLevel));
        //Debug.Log((((float)experiencePoints)/((float)levelOneExp)).ToString());

        if (deflectUnlocked == true)
        {
            if (deflectAvailable == false)
            {
                defSlider.gameObject.SetActive(true);
                defSlider.value = deflectTimer / asteroidCooldown;
                deflectTimer += Time.deltaTime;
                if (deflectTimer > asteroidCooldown)
                {
                    deflectAvailable = true;
                    deflectTimer = 0;
                }
            }
            else
            {
                defSlider.gameObject.SetActive(false);
            }
        }
        else
        {
            defSlider.gameObject.SetActive(false);
        }

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
                //Debug.Log("restarting fuel countdown, not pressing mouse button");
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


                if (thrusterGas > 1)
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
                        armIcon.enabled = false;
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

        if (deflectAvailable == true)
        {
            if (Physics2D.OverlapCircle(transform.position, 3, asteroidMask) != null)
            {
                Debug.Log("Destroy the asteroid nearby");
                deflectAvailable = false;
                deflectTimer = 0;
                Collider2D[] asteroidsTouching = Physics2D.OverlapCircleAll(transform.position, 3, asteroidMask);
                foreach (Collider2D a in asteroidsTouching)
                {
                    Destroy(a.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("We are colliding with "+collision.gameObject.name);
        if (collision.gameObject.tag == "Asteroid")
        {
            whackSFX.Play();
            stunned = true;
            Invoke("Unstun", stunCooldown);
            rb.AddTorque(100);
            
            
            Destroy(collision.gameObject);
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
        if (SceneManager.GetActiveScene().name != "TutorialLevel")
        {
            yield return new WaitForSeconds(1);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                timer -= 40;
            }
            else
            {
                timer -= 1;
            }

            StartCoroutine(TimerAdvance());
            if (timer < 0)
            {
                SceneManager.LoadScene("Results");
            }
        }
        else
        {
            timerSlider.gameObject.SetActive(false);
        }
        
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }

    void LevelUpBonus(int level)
    {
        if (level == 15)
        {
            // unlock blaster pizzas that destroy asteroids 
            return;
        }

        if (level == 10)
        {
            // unlock automatic asteroid deflect
            return;
        }
        
        if (level % 3 ==0)
        {
            thrusterGasMax += 20;
            PlayerPrefs.SetInt("thrusterMax", Mathf.FloorToInt(thrusterGasMax));
        }
        else
        {
            if (level % 2 == 0)
            {
                
                {
                    gasFilledPerSecond += 10;
                    PlayerPrefs.SetInt("thrusterFillSpeed", Mathf.FloorToInt(gasFilledPerSecond));
                    return;// "Thruster Gas Refill Speed Upgrade";
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("thrusterSpeed", 0) < 20)
                {
                    thrustSpeed += 1;
                    PlayerPrefs.SetInt("thrusterSpeed", Mathf.FloorToInt(thrustSpeed));
                    return;
                }
                else
                {
                    asteroidCooldown -= 0.2f;
                    PlayerPrefs.SetFloat("deflectCooldown", asteroidCooldown);
                    return;
                    //improve cooldown for asteroid deflect
                }
            }
        }
        return;
    }

    public void DistanceBonus(float dist)
    {
        if (dist >= distanceForBonus)
        {
            experiencePoints += Mathf.FloorToInt(50 + (Mathf.Floor(dist) * 2));
            PlayerPrefs.SetInt("exP", experiencePoints);
            StartCoroutine("ShowGameMessage","Nice Throw! +" + (Mathf.FloorToInt(50 + (Mathf.Floor(dist) * 2))).ToString() + "XP");
        }
    }
    public void LevelUp()
    {
        lvlUpSFX.Play();
        experiencePoints = experiencePoints - (levelOneExp + (levelAddedExp * currentLevel));
        LevelUpBonus(currentLevel);
        currentLevel += 1;
        PlayerPrefs.SetInt("nExP", levelOneExp + (levelAddedExp * currentLevel));

        PlayerPrefs.SetInt("exP", experiencePoints);
        PlayerPrefs.SetInt("level", currentLevel);
    }

    public IEnumerator ShowGameMessage(string message)
    {
        Debug.Log(message);
        messageText.text = message;
        yield return new WaitForSeconds(5);
        messageText.text = "";
    }
}
