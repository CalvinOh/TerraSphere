using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentTree : MonoBehaviour
{
    [SerializeField]
    private float PopupSize;



    private Vector3 StartSize;
    private float CurrentScale;

    private float StartTime;



        

    // Start is called before the first frame update
    void Start()
    {
        StartTime = 0;
        StartSize = transform.localScale;
        CurrentScale = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CurrentScale < 1f)
            DeterminCurrentSize();

        this.transform.localScale = StartSize * CurrentScale;
    }


    void DeterminCurrentSize()
    {
        // CurrentScale *= 1.01f;
        //CurrentScale = (-(20 * Mathf.Sin(20 * (Time.fixedTime - StartTime))) / (20 * (Time.fixedTime - StartTime)) + 10) / 10 * PopupSize;
        
        if (Time.fixedTime - StartTime >= 0.824)
        {
            CurrentScale *= 1.00005f;
        }
        else
        {
            CurrentScale = (-(20 * Mathf.Sin(30 * (Time.fixedTime - StartTime))) / (60 * (Time.fixedTime - StartTime)) + 10)/10 * PopupSize;
        }
        

        CurrentScale = Mathf.Clamp(CurrentScale, 0f, 1f);
    }




}
