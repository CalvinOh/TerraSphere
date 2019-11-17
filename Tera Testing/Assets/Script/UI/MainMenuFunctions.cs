using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class MainMenuFunctions : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject creditsPanel;

    [SerializeField]
    [Tooltip("The button the credit menu will start on.")]
    private GameObject creditsStart;
    [SerializeField]
    [Tooltip("The button the main menu will start on.")]
    private GameObject mainMenuStart;

    [SerializeField]
    [Tooltip("The number for the Scene Index you wish to switch to.")]
    private int sceneSwitch = 1;

    [SerializeField]
    [Tooltip("The number for the Scene Index you wish to switch from.")]
    private int currentScene = 0;

    private string currentPanel;
    private string previousPanel;
    private EventSystem eventSystem;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePanel();
        CheckButton();
    }

    private void CheckButton()
    {
        if (eventSystem.currentSelectedGameObject == null)
            eventSystem.SetSelectedGameObject(mainMenuStart);
    }

    public void GoToScene()
    {
        /* ckrueger audio */
        AkSoundEngine.StopAll();

        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadScene(sceneSwitch);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    private void UpdatePanel()
    {
        if (creditsPanel.activeSelf == true)
            currentPanel = creditsPanel.name;
        else if (mainMenuPanel.activeSelf == true)
            currentPanel = mainMenuPanel.name;

        if (currentPanel != previousPanel)
        {
            if (currentPanel == creditsPanel.name)
                eventSystem.SetSelectedGameObject(creditsStart);
            else if (currentPanel == mainMenuPanel.name)
                eventSystem.SetSelectedGameObject(mainMenuStart);
            previousPanel = currentPanel;
        }

    }
}
