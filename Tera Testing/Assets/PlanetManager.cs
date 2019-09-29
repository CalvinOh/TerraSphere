using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{

    [SerializeField]
    private float PlanetTerraformAmount;

    //static on purpose, for plant burst growth
    static float CurrentTerraformAmount;

    private List<PlantGrowth> PlantsOnPlanet = new List<PlantGrowth>();


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
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (PlantGrowth plant in PlantsOnPlanet)
        {
            CurrentTerraformAmount += plant.CurrentTerraFormAmountProvidedPerSecond;
        }
        CurrentTerraformAmount = Mathf.Clamp(CurrentTerraformAmount, 0, PlanetTerraformAmount);
    }
    
        
    
}
