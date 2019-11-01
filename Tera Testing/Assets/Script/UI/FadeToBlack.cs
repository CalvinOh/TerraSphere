using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField]
    Image blackImage;

    [SerializeField]
    bool doOnce = false;
    

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf && blackImage.color.a < 1)
            Fade();
        

        //if (fadeStart)

    }

    public void Fade()
    {
        //if (!doOnce)
        //{
        //    Destroy(FindObjectOfType<Inventory>().gameObject);
        //    doOnce = true;
        //}

        blackImage.color = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, blackImage.color.a + 0.004f);
    }
}
