using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{

    [SerializeField]
    private float PlanetTerraformAmount;

    static float CurrentTerraformAmount;
    private float TerraformAountPerSecond;

    public float TerraformPercentage
    {
        get
        {
            return (CurrentTerraformAmount/PlanetTerraformAmount*100);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentTerraformAmount = 0;
        TerraformAountPerSecond = 0;
    }



    // Update is called once per frame
    private void FixedUpdate()
    {
        CurrentTerraformAmount += TerraformAountPerSecond * Time.deltaTime;
        CurrentTerraformAmount = Mathf.Clamp(CurrentTerraformAmount, 0, PlanetTerraformAmount);
    }
    
        
    
}
