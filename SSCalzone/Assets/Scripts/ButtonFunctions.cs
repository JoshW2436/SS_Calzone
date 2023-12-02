using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public int pageValue = 0;

    private void Update()
    {
        //Debug.Log(pageValue);
    }

    public void Play(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public static void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        pageValue = 1;
    }

    public void Continue()
    {
        pageValue = 1;
    }

    public void PlayNewData()
    {
        pageValue = 2;
    }

    public void ShowPage(int page)
    {
        pageValue = page;
    }
}