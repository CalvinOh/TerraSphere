using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextAlphaUpDown : MonoBehaviour
{
    [SerializeField]
    private float changeSpeed = 1f;

    private Text text;
    private float currentColor;

    private bool isFaded = true;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        currentColor = text.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        currentColor = text.color.a;
        if (currentColor <= 1f)
            isFaded = false;
        else if (currentColor >= 0f)
            isFaded = true;
        if (isFaded)
        {
            text.CrossFadeAlpha(1, changeSpeed, false); 
        }
        else if (!isFaded)
        {
            text.CrossFadeAlpha(0, changeSpeed, false);
        }
    }
}
