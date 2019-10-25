using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The canvas so that it can be disabled once the tutorial is done.")]
    private GameObject canvasObject;

    [SerializeField]
    [Tooltip("Info text component for the data given by the next tutorial step.")]
    private Text tutorialText;

    [SerializeField]
    [Tooltip("Info text component for the name of the person giving the next tutorial step.")]
    private Text textName;

    [SerializeField]
    [Tooltip("Info text component for the data given by the next tutorial step.")]
    private string tutorialInfo1;

    [SerializeField]
    [Tooltip("Info text component for the data given by the next tutorial step.")]
    private string tutorialInfo2;

    [SerializeField]
    [Tooltip("String for the name of the message sender.")]
    private string stringName;

}
