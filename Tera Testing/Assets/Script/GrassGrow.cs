using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGrow : MonoBehaviour
{


    [SerializeField]
    private GameObject VisualObject;
    [SerializeField]
    private float FinalSize = 1;

    public bool Growing;
    private float CurrentScale;
    private float MaxScale;
    // Start is called before the first frame update
    void Start()
    {
        
        Growing = false;
        CurrentScale = 0;
        VisualObject.SetActive(false);
        MaxScale = Random.Range(0.75f*FinalSize, 1.25f*FinalSize);
        this.transform.localScale = CurrentScale * Vector3.one * MaxScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Growing)
        {
            Grow();
        }
    }

    private void Grow()
    {
        

        if (CurrentScale >= MaxScale)
        {
            Growing = false;
            CompleteGrowth();
        }
        else
        {
            CurrentScale += 0.2f*Time.deltaTime;
            CurrentScale = Mathf.Clamp(CurrentScale, 0f, 1f);
            this.transform.localScale = CurrentScale * Vector3.one*MaxScale;
        }
    }


    public void StartGrowth()
    {
        VisualObject.SetActive(true);
        Growing = true;
    }

    private void CompleteGrowth()
    {
        Destroy(this.GetComponent<GrassGrow>());
        this.gameObject.GetComponent<GrassGrow>().enabled = false;
    }
}
