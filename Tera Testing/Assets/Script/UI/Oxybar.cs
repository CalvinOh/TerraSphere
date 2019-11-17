using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oxybar : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Player script to grab current oxygen and max oxygen")]
    private PlayerController playerController;

    [SerializeField]
    [Tooltip("The image for the bar UI that can be raised and lowered.")]
    private Image oxybar;

    [SerializeField]
    [Tooltip("Current percentage of oxygen the player has.")]
    private float currentPercentage;

    [SerializeField]
    [Tooltip("Color the Oxygen bar starts at.")]
    private Color colorStart;

    [SerializeField]
    [Tooltip("Color the Oxygen bar ends at.")]
    private Color colorEnd;

    [SerializeField]
    [Tooltip("The multiplier that helps the color clamping, 1-2 recommended.")]
    private float multiplier = 1.3f;

    [SerializeField]
    [Tooltip("Canvas when oxygen runs out.")]
    private GameObject canvasObject;

    private float maxOxygen;
    private float currentOxygen;
    private bool canvasCalled = false;
    private GameOverMenu gameOverMenu;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        maxOxygen = playerController.oxygenMax;

        CheckForLowOxygen();
    }

    void FixedUpdate()
    {
        currentOxygen = playerController.oxygenValue;
        currentPercentage = currentOxygen / maxOxygen;
        oxybar.fillAmount = currentPercentage;
        ColorChange();

        if (currentPercentage <= 0 && !canvasCalled)
        {
            canvasObject.SetActive(true);
            gameOverMenu = FindObjectOfType<GameOverMenu>();
            gameOverMenu.DeathScreen();
            Time.timeScale = 0;
            canvasCalled = !canvasCalled;

            /* ckrueger audio */
            AkSoundEngine.StopAll();
        }
    }

    private void ColorChange()
    {
        oxybar.color = Color.Lerp(colorEnd, colorStart, currentPercentage*multiplier);
    }

    /* ckrueger audio */
    private void PlaySoundLowOxygen()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_ui_oxygen_low", gameObject);
    }

    //check for the oxygen to be below a set level
    private void CheckForLowOxygen()
    {
        /* ckrueger audio */
        if (currentPercentage < .20f)
        {
            InvokeRepeating("PlaySoundLowOxygen", 0f, 5f);
        }
    }
    /*^ ckrueger audio ^*/
}
