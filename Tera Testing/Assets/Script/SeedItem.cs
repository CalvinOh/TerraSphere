using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedItem : MonoBehaviour
{
    [SerializeField]
    private GameObject PlantToGrowInto;

    public GameObject plantToGrowInto
    {
        get
        {
            return PlantToGrowInto;
        }
    }
}
