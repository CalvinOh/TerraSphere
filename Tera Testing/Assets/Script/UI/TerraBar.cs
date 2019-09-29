using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerraBar : MonoBehaviour
{
    [SerializeField]
    private Image barFill;

    [SerializeField]
    [Tooltip("The maximum amount for the terraform bar to be completely filled.")]
    private float maxBarValue;
    
    [Tooltip("The percentage of the bar that is filled indicated using decimals 0 to 1.")]
    private float barPercentage;
    [Tooltip("The amount added everysecond for having plants unharvested.")]
    private float addRate;
    private float currentBarValue;

    void Update()
    {
        //currentBarValue = currentBarValue + (addRate * Time.deltaTime);
        //Adds the rate for current unharvested plants, the rate adds every second.
        //calculatePercentage();

        barFill.fillAmount = FindObjectOfType<PlanetManager>().TerraformPercentage/100;
    }

    /*
    private void calculatePercentage()
    {
        //Calculates the percentage of the bar filled and sets the bar to that amount.
        barPercentage = currentBarValue / maxBarValue;
        barFill.fillAmount = barPercentage;
    }

    private void AddRate(float rateIncrease)
    {
        addRate += rateIncrease;
    }

    private void ResetRate()
    {
        addRate = 0f;
    }

    private void AddAmount(float amountToAdd)
    {
        //Used when plant becomes first harvestable, adds one sizable boost to the bar.
        currentBarValue += amountToAdd;
    }
    */
}
