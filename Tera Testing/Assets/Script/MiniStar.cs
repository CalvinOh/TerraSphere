using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniStar : MonoBehaviour
{
    [SerializeField]
    public float MinHeight = 30;
    [SerializeField]
    public float MaxHeight = 40;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prep()
    {
        Vector3 MyScale;
        MyScale = this.transform.localScale;

        transform.localScale = MyScale;

        RotationAxis = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        RotationSpeed = Random.Range(0, RotationSpeedCap);

        transform.localPosition = new Vector3(0, Random.Range(MinHeight, MaxHeight), 0);
    }
}
