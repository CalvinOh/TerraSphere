using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSound : MonoBehaviour
{
    private float currentSceneTime;
    [SerializeField]
    private GameObject soundObj;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= currentSceneTime + .5f)
        {
            soundObj.SetActive(true);
        }
    }
}
