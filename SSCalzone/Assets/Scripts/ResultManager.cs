using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public int EasyPizzas = 3;
    public int MediumPizzas = 4;
    public int HardPizzas = 5;
    public int[] difficultyPizza;

    bool levelCompletedBonus = false;

    public TMP_Text winOrLose = null;
    public TMP_Text ratio = null;
    public TMP_Text difText = null;
    public TMP_Text nxtUText = null;
    public TMP_Text lvlText = null;
    public TMP_Text nxtLvlExp = null;

    public Slider expSlider = null;

    PlayerScript scoreScript = null;

    bool textSet = false;
    // Start is called before the first frame update
    void Start()
    {
        textSet = false;
        levelCompletedBonus = false;
        difficultyPizza = new int[3];
        if (SceneManager.GetActiveScene().name == "Results")
        {
            
            var texts = FindObjectsOfType<TMP_Text>();
            foreach (var text in texts)
            {
                if (text.name == "WinLose")
                {
                    winOrLose = text;
                }
                if (text.name == "Ratio")
                {
                    ratio = text;
                }
                if (text.name == "DifTxt")
                {
                    difText = text;
                }
                if (text.name == "NextUpgrade")
                {
                    nxtUText = text;
                }
                if (text.name == "LevelTxt")
                {
                    lvlText = text;
                }
                if (text.name == "LevelUpExpTxt")
                {
                    nxtLvlExp = text;
                }
            }
            var sliders = FindObjectsOfType<Slider>();
            foreach (var slider in sliders)
            {
                if (slider.name == "ExpBar")
                {
                    expSlider = slider;
                }
            }
            }
        else
        {
            scoreScript = GameObject.Find("PlayerObj").GetComponent<PlayerScript>();
            textSet = false;
            var texts = FindObjectsOfType<TMP_Text>();
            foreach (var text in texts)
            {
                if (text.name == "Ratio")
                {
                    ratio = text;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        difficultyPizza[0] = EasyPizzas;
        difficultyPizza[1] = MediumPizzas;
        difficultyPizza[2] = HardPizzas;
        if (SceneManager.GetActiveScene().name == "Results")
        {
            //Debug.Log("Last Round Pizzas:" + PlayerPrefs.GetInt("LRP", 0).ToString() + "Last Round Difficulty:" + PlayerPrefs.GetInt("LRD", 0).ToString());
            if (textSet == false)
            {
                int lastRoundPizzas = PlayerPrefs.GetInt("LRP", 0);
                int lastRoundDifficulty = PlayerPrefs.GetInt("LRD", 0);
                if (lastRoundPizzas >= difficultyPizza[lastRoundDifficulty])
                {
                    winOrLose.text = "You Win!";
                }
                else
                {
                    winOrLose.text = "You Lost!";
                }
                ratio.text = lastRoundPizzas.ToString() + "/" + difficultyPizza[lastRoundDifficulty].ToString();
                switch (lastRoundDifficulty)
                {
                    case 0:
                        difText.text = "Easy";
                        break;
                    case 1:
                        difText.text = "Medium";
                        break;
                    case 2:
                        difText.text = "Hard";
                        break;
                }
                nxtUText.text = ("Level "+ (PlayerPrefs.GetInt("level", 0) + 1).ToString()+" Bonus:<br>"+NextLevelBonus(PlayerPrefs.GetInt("level", 0)+1));
                lvlText.text = "Level "+PlayerPrefs.GetInt("level", 0).ToString();
                nxtLvlExp.text = PlayerPrefs.GetInt("exP", 0).ToString()+"/"+PlayerPrefs.GetInt("nExP", 0).ToString()+" to Next Level";
                textSet = true;
                expSlider.value = (float)PlayerPrefs.GetInt("exP", 0) / PlayerPrefs.GetInt("nExP", 0);
            }
        }
        else
        {
            //Debug.Log("Last Round Pizzas:" + PlayerPrefs.GetInt("LRP", 0).ToString() + "Last Round Difficulty:" + PlayerPrefs.GetInt("LRD", 0).ToString());
            if (textSet == false)
            {
                int lastRoundPizzas = PlayerPrefs.GetInt("LRP", 0);
                int lastRoundDifficulty = PlayerPrefs.GetInt("LRD", 0);
                ratio.text = "Orders Completed: "+ lastRoundPizzas.ToString() + "/" + difficultyPizza[lastRoundDifficulty].ToString();
                if (levelCompletedBonus == false)
                {
                    if (lastRoundPizzas >= difficultyPizza[lastRoundDifficulty])
                    {
                        scoreScript.experiencePoints += 300 * (lastRoundDifficulty + 1);
                        PlayerPrefs.SetInt("exP", scoreScript.experiencePoints);
                        scoreScript.StartCoroutine("ShowGameMessage", "You completed all required orders! +" + (300 * (lastRoundDifficulty + 1)).ToString() + "XP");
                        levelCompletedBonus = true;                        
                    }
                }
            }
            
        }
        
    }

    string NextLevelBonus(int level)
    {
        if (level == 15)
        {
            // unlock blaster pizzas that destroy asteroids 
            return "Blaster Pizzas";
        }

        if (level == 10)
        {
            return "Asteroid Deflector";
        }

        if (level % 3 == 0)
        {
            return "Thruster Gas Capacity Upgrade";
        }
        else
        {
            if (level % 2 == 0)
            {
                return "Thruster Gas Refill Speed Upgrade";       
            }
            else
            {
                if (PlayerPrefs.GetInt("thrusterSpeed", 0) < 20)
                {
                    return "Thruster Speed Upgrade";
                }
                else
                {
                    return "Asteroid Deflector Recharge Upgrade";
                }
            }
        }
        //return "No upgrade";
    }
}
