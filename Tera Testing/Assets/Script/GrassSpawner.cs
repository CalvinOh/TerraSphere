using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject GrassOnSurfacePrefab01;
    [SerializeField]
    private GameObject GrassOnSurfacePrefab02;
    [SerializeField]
    private GameObject GrassOnSurfacePrefab03;

    [SerializeField]
    private int ApproximateGrassAmount;



    private List<GameObject> GrassPrefabs = new List<GameObject>();
    private List<GameObject> GrassOnPlanet = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        GrassPrefabs.Add(GrassOnSurfacePrefab01);
        GrassPrefabs.Add(GrassOnSurfacePrefab02);
        GrassPrefabs.Add(GrassOnSurfacePrefab03);

        int AmountOfGrass =   (int)Random.Range(ApproximateGrassAmount*0.75f,ApproximateGrassAmount*1.25f);

        for (int i = 0; i < AmountOfGrass; i++)
        {
            GameObject CreatedGrass = Instantiate(GrassPrefabs[Random.Range(0, GrassPrefabs.Count)], transform.position, transform.rotation);
            CreatedGrass.transform.localRotation = Quaternion.EulerAngles(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            CreatedGrass.transform.parent = this.gameObject.transform;
            CreatedGrass.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            GrassOnPlanet.Add(CreatedGrass);
        }

    }

}
