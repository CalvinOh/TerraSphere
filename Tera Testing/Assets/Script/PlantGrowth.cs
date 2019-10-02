using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    private Transform MyTransform;
    private ParticleSystem MyPS;

    [SerializeField]
    [Tooltip("Speed of growth, leave at 1 for default")]
    private float GrowthSpeed = 1;
    [SerializeField]
    [Tooltip("Size scale at end of growth, leave at 2 for default")]
    private float FinalSize = 2;
    [SerializeField]
    [Tooltip("Time at which Stage 1 ends")]
    private float Stage1Cap = 10;
    [SerializeField]
    [Tooltip("Time at which plant growth stops, need to be bigger than Stage1Cap")]
    private float Stage2Cap = 20;

    [SerializeField]
    [Tooltip("Terraform amount persecond provided by this plant in Stage 1")]
    private float Stage1TerraFormPerSecond = 10;
    [SerializeField]
    [Tooltip("Terraform amount persecond provided by this plant in Stage 2")]
    private float Stage2TerraFormPerSecond = 20;


    [SerializeField]
    private GameObject Stage1;
    [SerializeField]
    private GameObject Stage2;

    [SerializeField]
    private int MinAmountOfSeedDrop;
    [SerializeField]
    private int MaxAmountOfSeedDrop;
    [SerializeField]
    private GameObject SeedSpawnedWhenHarvested;
    [SerializeField]
    private GameObject PlantSpawnedWhenHarvested;

    [SerializeField]
    private Material UnOutlined;
    [SerializeField]
    private Material Outlined;

    public bool Grow;


    public float AccelTimeer; //how much longer the plant will remain in accelerated growth, after watering. public for debugging ONLY.
    public float CurrentGrowthAmount; //public for debugging ONLY
    private int Stage;
    public float CurrentTerraFormAmountProvidedPerSecond
    {
        
        get
        {
            if (!Grow)
            {
                return 0;
            }
            else
            {
                float TerraformAmount = 0;

                if (Stage == 1)
                    TerraformAmount = Stage1TerraFormPerSecond;
                else if (Stage == 2)
                    TerraformAmount = Stage2TerraFormPerSecond;

                if (AccelTimeer > 0)
                    TerraformAmount *= 2;

                return TerraformAmount;
            }
        }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        MyTransform = GetComponent<Transform>();
        MyPS = GetComponent<ParticleSystem>();
        CurrentGrowthAmount = 0;
        Stage = 1;
        Stage1.SetActive(true);
        Stage2.SetActive(false);
        MyPS.enableEmission = false;
        FindObjectOfType<PlanetManager>().AddPlant(this);
        this.gameObject.tag = "Seed";
    }


    void FixedUpdate()
    {
        if (Grow)
        {
            UpdateGrowth();
        }

    }


    private void UpdateGrowth()
    {
        //this part is about switching models and activating the particle emission, purely visual
        if (CurrentGrowthAmount < Stage1Cap && (CurrentGrowthAmount + GrowthSpeed * Time.deltaTime) >= Stage1Cap)
        {
            Stage1.SetActive(false);
            Stage2.SetActive(true);
            Stage += 1;
        }
        else if (CurrentGrowthAmount < Stage2Cap && (CurrentGrowthAmount + GrowthSpeed * Time.deltaTime) >= Stage2Cap)
        {
            MyPS.enableEmission = true;
            this.gameObject.tag = "Plant";
        }
        

        if (CurrentGrowthAmount >= Stage2Cap)
        {
                AccelTimeer -= Time.deltaTime;
            //happens when plant is fully grown every frame, eg. produce terraforming points, dooesn't grow anymore, provides terraform amount.
        }
        else
        {
            if (AccelTimeer < 0)
                CurrentGrowthAmount += GrowthSpeed * Time.deltaTime;
            else
            {
                CurrentGrowthAmount += GrowthSpeed * Time.deltaTime * 2;
                AccelTimeer -= Time.deltaTime;
            }
                
            float scale = ((CurrentGrowthAmount / Stage2Cap) * FinalSize);
            MyTransform.localScale =new Vector3( scale,scale,scale);
        }


    }

    public void Water(float water)
    {
        AccelTimeer = water;
    }

    public void Harvest()
    {
        int NumberOfSeeds = Random.Range(MinAmountOfSeedDrop, MaxAmountOfSeedDrop);

        for (int i = 0; i < NumberOfSeeds; i++)
        {
            GameObject DroppedSeed = Instantiate(SeedSpawnedWhenHarvested, transform.position+ new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), transform.rotation);
            print("Spawning Seed");
        }
        
        if (PlantSpawnedWhenHarvested != null)
        {
            GameObject DroppedPlant = Instantiate(PlantSpawnedWhenHarvested, transform.position, transform.rotation);
            print("Spawning Plant");
        }

        DestroyObject(this.gameObject);
    }

    public void ToggleOutline()
    {
        MeshRenderer[] AllMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer i in AllMeshRenderers)
        {
            if (i.material == Outlined)
                i.material = UnOutlined;
            else
                i.material = Outlined;
        }
    }
}
