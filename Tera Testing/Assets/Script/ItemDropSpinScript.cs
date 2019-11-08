using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpinScript : MonoBehaviour
{

    [SerializeField]
    private float turnSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(0, turnSpeed, 0);
    }
}
