using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{


    [SerializeField]
    public float MinHeight = 30;
    [SerializeField]
    public float MaxHeight = 40;

    [SerializeField]
    public float WarpAmount = 3;
    [Tooltip("Rotation amount in degrees/second")]
    [SerializeField]
    public float RotationSpeedCap = 20;

    private Vector3 RotationAxis;
    private float RotationSpeed;
    // Start is called before the first frame update


    void Start()
    {
        Prep();
    }


    void FixedUpdate()
    {
        this.transform.Rotate(RotationAxis, RotationSpeed);
    }

    public void Prep()
    {
        Vector3 MyScale;
        MyScale = this.transform.localScale;

        MyScale.x *= Random.Range(1f, WarpAmount);
        MyScale.y *= Random.Range(1f, WarpAmount);
        MyScale.z *= Random.Range(1f, WarpAmount);

        transform.localScale = MyScale;

        RotationAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        RotationSpeed = Random.Range(0, RotationSpeedCap);

        transform.localPosition = new Vector3(0, Random.Range(MinHeight, MaxHeight), 0);
    }

    /*
    public void Prep(float minHeight,float maxHeight,float warpAmount, float rotationSpeedCap)
    {
        MaxHeight = maxHeight;
        MinHeight = minHeight;
        WarpAmount = warpAmount;
        RotationSpeedCap = rotationSpeedCap;

        Vector3 MyScale;
        MyScale = this.transform.localScale;

        MyScale.x *= Random.Range(1f, WarpAmount);
        MyScale.y *= Random.Range(1f, WarpAmount);
        MyScale.z *= Random.Range(1f, WarpAmount);

        transform.localScale = MyScale;

        RotationAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        RotationSpeed = Random.Range(0, RotationSpeedCap);

        transform.localPosition = new Vector3(0, Random.Range(MinHeight, MaxHeight), 0);
    }
    */
}
