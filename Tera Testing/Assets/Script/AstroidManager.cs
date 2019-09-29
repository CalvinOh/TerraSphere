using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidManager : MonoBehaviour
{


    [SerializeField]
    private GameObject AstroidInOrbit1;
    [SerializeField]
    private GameObject AstroidInOrbit2;
    [SerializeField]
    private GameObject AstroidInOrbit3;
    [SerializeField]
    private GameObject AstroidInOrbit4;

    [SerializeField]
    private int MaxAstroids;
    [SerializeField]
    private int MinAstroids;

    [SerializeField]
    private int MaxOrbitSpeed;
    [SerializeField]
    private int MinOrbitSpeed;

    [SerializeField]
    private int AstroidMaxHeight = 50;
    [SerializeField]
    private int AstroidMinHeight = 40;
    [SerializeField]
    private float AstroidWarpAmount = 3;
    [Tooltip("Rotation amount in degrees/second")]
    [SerializeField]
    private float AstroidRotationSpeedCap = 20;

    private List<GameObject> AstroidPrefabs = new List<GameObject>();
    private List<GameObject> Astroids = new List<GameObject>();
    private List<float> AstroidsOrbitSpeed = new List<float>();
    private List<Vector3> AstroidsOrbitAxis = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        AstroidPrefabs.Add(AstroidInOrbit1);
        AstroidPrefabs.Add(AstroidInOrbit2);
        AstroidPrefabs.Add(AstroidInOrbit3);
        AstroidPrefabs.Add(AstroidInOrbit4);

        foreach (GameObject i in AstroidPrefabs)
        {
            Astroid AstroidScript= i.GetComponentInChildren<Astroid>();
            AstroidScript.MaxHeight = AstroidMaxHeight;
            AstroidScript.MinHeight = AstroidMinHeight;
            AstroidScript.WarpAmount = AstroidWarpAmount;
            AstroidScript.RotationSpeedCap = AstroidRotationSpeedCap;

        }



        int AmountOfAstroids = Random.Range(MinAstroids, MaxAstroids);
        for (int i = 0; i < AmountOfAstroids; i++)
        {
            GameObject CreatedAstroid = Instantiate(AstroidPrefabs[Random.Range(0,AstroidPrefabs.Count)], transform.position, transform.rotation);
            CreatedAstroid.transform.localRotation = Quaternion.EulerAngles(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            CreatedAstroid.transform.parent = this.gameObject.transform;
            Astroids.Add(CreatedAstroid);
            AstroidsOrbitAxis.Add( new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            AstroidsOrbitSpeed.Add(Random.Range(MinOrbitSpeed, MaxOrbitSpeed));
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < Astroids.Count; i++)
        {
            Astroids[i].transform.Rotate(AstroidsOrbitAxis[i], AstroidsOrbitSpeed[i]*Time.deltaTime);
        }
    }
}
