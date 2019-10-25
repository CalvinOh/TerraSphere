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
    private Material grass;

    [SerializeField]
    private Animator animator;
    
    [Tooltip("The percentage of the bar that is filled indicated using decimals 0 to 1.")]
    public float barPercentage;

    void Update()
    {
        barPercentage = FindObjectOfType<PlanetManager>().TerraformPercentage/100;
        barFill.fillAmount = barPercentage;
        Color alpha = new Color(1, 1, 1, barPercentage);
        grass.color = alpha;
        animator.SetFloat("Flower", barPercentage);
    }
}
