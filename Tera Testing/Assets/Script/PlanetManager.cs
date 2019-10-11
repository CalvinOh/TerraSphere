using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{

    [SerializeField]
    private float PlanetTerraformAmount;

    [SerializeField]
    private float PercentageWhichTreesStartToAppear;

    //static on purpose, for plant burst growth
    static float CurrentTerraformAmount;

    private List<PlantGrowth> PlantsOnPlanet = new List<PlantGrowth>();
    private List<EnviromentTree> EnviromentalTrees = new List<EnviromentTree>();


    public float TerraformPercentage
    {
        get
        {
            return (CurrentTerraformAmount/PlanetTerraformAmount*100);
        }
    }

    public float TerraformAmountPerSecond
    {
        get
        {
            float Amount=0;
            foreach (PlantGrowth plant in PlantsOnPlanet)
            {
                Amount += plant.CurrentTerraFormAmountProvidedPerSecond;
            }
            return Amount;
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
        CurrentTerraformAmount += TerraformAmountPerSecond;
        
        CurrentTerraformAmount = Mathf.Clamp(CurrentTerraformAmount, 0, PlanetTerraformAmount);
    }

    public void AddPlant(PlantGrowth PlantToAdd)
    {
        PlantsOnPlanet.Add(PlantToAdd);
    }

    public void AddTree(EnviromentTree TreeToAdd)
    {
        EnviromentalTrees.Add(TreeToAdd);
    }
    


    private void SpawnTrees()
    {
        if (TerraformPercentage >= PercentageWhichTreesStartToAppear)
        {

            for (int i = 0; i < EnviromentalTrees.Count; i++)
            {
                if (TerraformPercentage <= ((100 - PercentageWhichTreesStartToAppear) / EnviromentalTrees.Count) * i && TerraformPercentage + TerraformAmountPerSecond > (100 - PercentageWhichTreesStartToAppear) / EnviromentalTrees.Count *i)
                {
                    EnviromentalTrees[i].StartGrow();
                }
            }
            
        }
    }
}
