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
    private Color colorStart;

    [SerializeField]
    private Color colorEnd;

    private float maxOxygen;
    private float currentOxygen;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        maxOxygen = playerController.oxygenMax;
    }

    void FixedUpdate()
    {
        currentOxygen = playerController.oxygenValue;
        currentPercentage = currentOxygen / maxOxygen;
        oxybar.fillAmount = currentPercentage;
        ColorChange();
    }

    private void ColorChange()
    {
        oxybar.color = Color.Lerp(colorEnd,colorStart, currentPercentage*1.3f);
        /* (currentPercentage >= .6)
        {
            oxybar.color = colorStart;
        }
        else if (currentPercentage >= .3)
        {
            oxybar.color = Color.Lerp(colorStart, colorMiddle, 3);
        }
        else if(currentPercentage >= 0)
        {
            
        }*/
    }
}
