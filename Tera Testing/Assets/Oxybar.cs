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
    }
}
