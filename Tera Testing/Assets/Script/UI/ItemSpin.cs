using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{

    private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);
    }
}
