using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The dark panel image is on")]
    private Image panelImage;

    [SerializeField]
    private float panelTimer = 3;

    [SerializeField]
    private float fadeInTimer = 2;

    [SerializeField]
    private GameObject image1;

    [SerializeField]
    private GameObject image2;

    [SerializeField]
    private GameObject image3;

    [SerializeField]
    private GameObject image4;

    [SerializeField]
    private GameObject image5;

    [SerializeField]
    private GameObject text;

    [SerializeField]
    private GameObject image6;

    private float timerReset;
    private float timeAtAwake;

    private void Awake()
    {
        timeAtAwake = Time.time;
    }

    void Update()
    {
        timerReset = Time.time - timeAtAwake;
        if(timerReset >= fadeInTimer + panelTimer)
        {
            this.gameObject.SetActive(false);
        }
        if(timerReset >= panelTimer || Input.GetButton("Use Item"))
        {
            panelImage.CrossFadeAlpha(0, fadeInTimer,false);
            image1.SetActive(false);
            image2.SetActive(false);
            image3.SetActive(false);
            image4.SetActive(false);
            image5.SetActive(false);
        }

    }
}
