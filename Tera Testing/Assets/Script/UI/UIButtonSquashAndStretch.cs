using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIButtonSquashAndStretch : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private float startingSize;

    [SerializeField]
    private float endSize;

    [SerializeField]
    [Tooltip("The speed that the UI Button changes size. 1 is fast and 0 is slow.")]
    private float changeSpeed;

    private float currentSize;

    bool isStarting = true;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        currentSize = startingSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSize <= startingSize)
            isStarting = true;
        else if (currentSize >= endSize)
            isStarting = false;
        if (isStarting)
            currentSize+=changeSpeed;
        else if(!isStarting)
            currentSize-=changeSpeed;

        image.rectTransform.sizeDelta = new Vector2(currentSize, currentSize);
    }
}
