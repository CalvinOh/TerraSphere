using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentTree : MonoBehaviour
{
    [SerializeField]
    private float PopupSize;


    private float CurrentScale;

    private float StartTime;



        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        DeterminCurrentSize();

        this.transform.localScale =new Vector3( 7 * CurrentScale,7*CurrentScale,7*CurrentScale);
    }


    void DeterminCurrentSize()
    {
        CurrentScale = -(20 * Mathf.Cos(Time.fixedTime - StartTime)) / (Time.fixedTime - StartTime) + 10;
        CurrentScale = Mathf.Clamp(CurrentScale, 0, 1000000);

    }




}
