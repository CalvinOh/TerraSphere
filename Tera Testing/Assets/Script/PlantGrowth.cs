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
    [Tooltip("Time at which stage 1 ends")]
    private float Stage1Cap = 10;
    [SerializeField]
    [Tooltip("Time at which plant growth stops, need to be bigger than Stage1Cap")]
    private float Stage2Cap = 20;

    [SerializeField]
    private GameObject Stage1;
    [SerializeField]
    private GameObject Stage2;

    public bool Grow;


    public float AccelTimeer; //how much longer the plant will remain in accelerated growth, after watering. public for debugging ONLY.
    public float CurrentGrowthAmount; //public for debugging ONLY
    private int stage;




    // Start is called before the first frame update
    void Start()
    {
        MyTransform = GetComponent<Transform>();
        MyPS = GetComponent<ParticleSystem>();
        CurrentGrowthAmount = 0;
        stage = 1;
        Stage1.SetActive(true);
        Stage2.SetActive(false);
        MyPS.enableEmission = false;
    }


    void FixedUpdate()
    {
        if(Grow)
        UpdateGrowth();
    }


    private void UpdateGrowth()
    {
        
        if (CurrentGrowthAmount < Stage1Cap && (CurrentGrowthAmount + GrowthSpeed * Time.deltaTime) >= Stage1Cap)
        {
            Stage1.SetActive(false);
            Stage2.SetActive(true);
            stage += 1;
        }
        else if (CurrentGrowthAmount < Stage2Cap && (CurrentGrowthAmount + GrowthSpeed * Time.deltaTime) >= Stage2Cap)
        {
            MyPS.enableEmission = true;
        }
        
        if (CurrentGrowthAmount >= Stage2Cap)
        {
            if (AccelTimeer < 0)
            {

            }
            else
            {
                AccelTimeer -= Time.deltaTime;
            }
            //happens when plant is fully grown every frame, eg. produce terraforming points
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
}
