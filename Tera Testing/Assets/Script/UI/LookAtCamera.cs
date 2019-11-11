using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{

    

    [SerializeField]
    private Transform cameraPosition;

    //private Transform emptyPosition;
    [SerializeField]
    private RectTransform rectTransform;
    //[SerializeField]
    //private GameObject player;


    private void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float t = cameraPosition.rotation.y - rectTransform.rotation.y;
        Vector3 NewDir = Vector3.RotateTowards(rectTransform.forward, cameraPosition.position, 10000, 10f);
        rectTransform.rotation = cameraPosition.rotation;
        //rectTransform.LookAt(cameraPosition);
    }
}
