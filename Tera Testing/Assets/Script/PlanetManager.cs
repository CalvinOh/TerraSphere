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
    public List<EnviromentTree> EnviromentalTrees = new List<EnviromentTree>();
    private int NumberOfTreeToGrowNext;

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
        //StartCoroutine(GrowTrees());
        CurrentTerraformAmount = 0;
        NumberOfTreeToGrowNext = 0;
        //GrowAllTrees();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        CurrentTerraformAmount += TerraformAmountPerSecond;
        CurrentTerraformAmount = Mathf.Clamp(CurrentTerraformAmount, 0, PlanetTerraformAmount);
        DetermineTreeGrowth();
    }

    public void AddPlant(PlantGrowth PlantToAdd)
    {
        PlantsOnPlanet.Add(PlantToAdd);
    }

    public void AddTree(EnviromentTree TreeToAdd)
    {
        EnviromentalTrees.Add(TreeToAdd);
    }
    

    /*
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
    */

    private IEnumerator GrowTrees()
    {
        int NumberOfTreeToGrowNext =0;
        while (TerraformPercentage < 100)
        {
            if (TerraformPercentage > ((100 - PercentageWhichTreesStartToAppear) / EnviromentalTrees.Count) * (NumberOfTreeToGrowNext+1))
            {
                
            }

        }

        return null;

    }

    private void DetermineTreeGrowth()
    {
        if (TerraformPercentage > ((100 - PercentageWhichTreesStartToAppear) / EnviromentalTrees.Count) * (NumberOfTreeToGrowNext )+PercentageWhichTreesStartToAppear)
        {
            EnviromentalTrees[NumberOfTreeToGrowNext].StartGrow();
            NumberOfTreeToGrowNext++;
            Debug.Log("time to grow another tree");
        }
    }
    private void GrowAllTrees()
    {
        foreach (EnviromentTree T in EnviromentalTrees)
        {
            T.StartGrow();
        }
    }

}
