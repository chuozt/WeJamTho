using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, optionMenu, title;

    void Awake()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        title.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Back()
    {
        optionMenu.SetActive(false);
        mainMenu.SetActive(true);
        title.SetActive(true);
    }
    public void Option()
    {
        optionMenu.SetActive(true);
        mainMenu.SetActive(false);
        title.SetActive(false);
    }

    public void Play()
    {
        //SceneManager.LoadScene();
    }

}
