using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private Transform cameraPosition;

    private Transform emptyPosition;

    // Update is called once per frame
    void Update()
    {
        emptyPosition.rotation = rectTransform.rotation;
        emptyPosition.LookAt(rectTransform);
        rectTransform.rotation.Set(rectTransform.rotation.x, emptyPosition.rotation.y, rectTransform.rotation.z, rectTransform.rotation.w);
    }
}
