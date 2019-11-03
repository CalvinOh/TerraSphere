using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentTree : MonoBehaviour
{
    [SerializeField]
    private float PopupSize;

    [SerializeField]
    private GameObject VisualObject;

   


    private Vector3 StartSize;
    public float CurrentScale;
    private Collider MyCollider;
    private float StartTime;
    private bool Grow;


        

    // Start is called before the first frame update
    void Start()
    {
        StartTime = 0;
        StartSize = VisualObject.transform.localScale;
        CurrentScale = 0.0000001f;
        MyCollider = GetComponent<Collider>();
        MyCollider.enabled = false;
        VisualObject.SetActive(false);
        //this need to be set to false for the tree to not appear at the start
        Grow = false;
        FindObjectOfType<PlanetManager>().AddTree(this);
        //StartGrow();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Grow)
        {
            
                DeterminCurrentSize();
            

            VisualObject.transform.localScale = StartSize * CurrentScale;
        }
    }


    void DeterminCurrentSize()
    {
        // CurrentScale *= 1.01f;
        //CurrentScale = (-(20 * Mathf.Sin(20 * (Time.fixedTime - StartTime))) / (20 * (Time.fixedTime - StartTime)) + 10) / 10 * PopupSize;

        if(Time.fixedTime - StartTime <= 0.5)
            CurrentScale += PopupSize / 0.5f *Time.deltaTime;
        else
            CurrentScale += 0.002f * Time.deltaTime;



        if (CurrentScale >= 1)
            Grow = false;
        CurrentScale = Mathf.Clamp(CurrentScale, 0f, 1f);
    }

    public void StartGrow()
    {
        StartTime = Time.fixedTime;
        MyCollider.enabled = true;
        Grow = true;
        VisualObject.SetActive(true);
    }


}
