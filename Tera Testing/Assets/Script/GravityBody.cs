using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{

    public GravityAttractor gravityAttractor;

    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        this.GetComponent<Rigidbody>().useGravity = false;
       
       

        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        
        this.gravityAttractor.Attract(myTransform);
    
    }

}
