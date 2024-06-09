using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : Singleton<PauseMenuManager>
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject audioSettingsPanel;
    public bool isPausing = false;

    void Awake()
    {
        pauseMenuPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPausing)
                ToggleOnPauseMenu();
            else
                ToggleOffPauseMenu();
        }
    }

    private void ToggleOnPauseMenu()
    {
        isPausing = true;
        Time.timeScale = 0;

        pauseMenuPanel.SetActive(true);
        audioSettingsPanel.SetActive(false);
    }

    private void ToggleOffPauseMenu()
    {
        isPausing = false;
        Time.timeScale = 1;

        pauseMenuPanel.SetActive(false);
        audioSettingsPanel.SetActive(false);
    }

    private void ToggleOnAudioSettings()
    {
        pauseMenuPanel.SetActive(false);
        audioSettingsPanel.SetActive(true);
    }
    
    private void ToggleOffAudioSettings()
    {
        pauseMenuPanel.SetActive(true);
        audioSettingsPanel.SetActive(false);
    }

    public void ResumeButton() => ToggleOffPauseMenu();

    public void AudioSettingsButton() => ToggleOnAudioSettings();

    public void ExitButton()
    {
        Time.timeScale = 1;
        //OpenScene
    }

    public void BackButton() => ToggleOffAudioSettings();
}
