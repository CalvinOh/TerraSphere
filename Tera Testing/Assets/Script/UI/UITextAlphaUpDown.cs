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
    private Color color1;
    private Color color2;

    private bool isFaded = true;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        currentColor = text.color.a;
        color1 = new Color(1, 1, 1, 1);
        color2 = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentColor >= 1f)
            isFaded = false;
        else //if (currentColor <= 0.1f)
            isFaded = true;
        if (isFaded )
        {
            text.CrossFadeColor(color1, changeSpeed, false, true);
            //text.CrossFadeAlpha(1f, changeSpeed, false); 
        }
        else if (!isFaded )
        {
            text.CrossFadeColor(color2, changeSpeed, false, true);
            //text.CrossFadeAlpha(0f, changeSpeed, false);
        }
        currentColor = text.color.a;
    }
}
