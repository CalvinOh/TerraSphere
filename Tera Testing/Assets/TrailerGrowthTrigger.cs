using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerGrowthTrigger : MonoBehaviour
{

    [SerializeField]
    private float TriggerRadius = 10;
    [SerializeField]
    private float SpinSpeed = 1;
    [SerializeField]
    private float TerraformGrowth;
    [SerializeField]
    private float StartDelay;



    private Transform ParentTransform;
    private PlanetManager PM;
    private bool Widened;
    private Transform[] ChildTransforms;
    // Start is called before the first frame update
    void Start()
    {
        Widened = false;
        PM = FindObjectOfType<PlanetManager>();
        ParentTransform = GetComponent<Transform>();

        ChildTransforms = GetComponentsInChildren<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        if (StartDelay <= 0)
        {
            foreach (Transform A in ChildTransforms)
            {
                TriggerGrowth(A);
            }
            RotateParent();
            FillTerraBar();
        }

        if (Time.fixedTime > 23 && !Widened)
        {
            TriggerRadius *= 10;
            Widened = true;
        }

        StartDelay -= Time.deltaTime;
        Debug.Log(Time.fixedTime);
    }

    private void TriggerGrowth(Transform A)
    {
        Collider[] allOverlappingColliders = Physics.OverlapSphere(A.position, TriggerRadius);
        foreach (Collider C in allOverlappingColliders)
        {
            if (C.gameObject.GetComponent<GrassGrow>() != null)
                C.gameObject.GetComponent<GrassGrow>().StartGrowth();
            else if (C.gameObject.GetComponent<EnviromentTree>() != null)
                C.gameObject.GetComponent<EnviromentTree>().StartGrow();
        }
    }

    private void RotateParent()
    {
        if(ParentTransform!=null)
        ParentTransform.Rotate(new Vector3(0,-SpinSpeed*Time.deltaTime,0));
    }

    private void FillTerraBar()
    {
        PlanetManager.CurrentTerraformAmount += TerraformGrowth*Time.deltaTime;
    }
}
