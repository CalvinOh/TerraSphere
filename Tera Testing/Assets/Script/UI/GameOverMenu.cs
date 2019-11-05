using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The event system which handles currently selected or hovered UI option, important for keyboard or gamepad only.")]
    private EventSystem eventSystem;

    [SerializeField]
    [Tooltip("The darkened panel containing the game over UI so when set active, all the UI becomes visible.")]
    private GameObject gameOverPanel;

    [SerializeField]
    [Tooltip("The button the gameover panel should start on.")]
    private Button startingButton;

    [SerializeField]
    [Tooltip("The scene index number for this scene.")]
    private int thisSceneIndex;

    private bool DeathScreenIsActive = false;

    private void FixedUpdate()
    {
        if(DeathScreenIsActive && eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(startingButton.gameObject);
        }
    }

    public void DeathScreen()
    {
        gameOverPanel.SetActive(true);
        eventSystem.SetSelectedGameObject(startingButton.gameObject);
        //activates death screen panel and sets the starting button to be selected entering the player into that menu navigation.
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(thisSceneIndex);
    }
}
