using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniStarManager : MonoBehaviour
{

    [SerializeField]
    private GameObject MiniStarInOrbit1;
    [SerializeField]
    private GameObject MiniStarInOrbit2;
    [SerializeField]
    private GameObject MiniStarInOrbit3;
    [SerializeField]
    private GameObject MiniStarInOrbit4;

    [SerializeField]
    private int MaxMiniStars;
    [SerializeField]
    private int MinMiniStars;

    [SerializeField]
    private int MaxOrbitSpeed;
    [SerializeField]
    private int MinOrbitSpeed;

    [SerializeField]
    private int MiniStarMaxHeight = 50;
    [SerializeField]
    private int MiniStarMinHeight = 40;
    [Tooltip("Rotation amount in degrees/second")]
    [SerializeField]
    private float MiniStarRotationSpeedCap = 20;

    private List<GameObject> MiniStarPrefabs = new List<GameObject>();
    private List<GameObject> MiniStars = new List<GameObject>();
    private List<float> MiniStarsOrbitSpeed = new List<float>();
    private List<Vector3> MiniStarsOrbitAxis = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {

        MiniStarPrefabs.Add(MiniStarInOrbit1);
        MiniStarPrefabs.Add(MiniStarInOrbit2);
        MiniStarPrefabs.Add(MiniStarInOrbit3);
        MiniStarPrefabs.Add(MiniStarInOrbit4);

        foreach (GameObject i in MiniStarPrefabs)
        {
            MiniStar MiniStarScript = i.GetComponentInChildren<MiniStar>();
            MiniStarScript.MaxHeight = MiniStarMaxHeight;
            MiniStarScript.MinHeight = MiniStarMinHeight;
            MiniStarScript.RotationSpeedCap = MiniStarRotationSpeedCap;
        }

        int AmountOfMiniStars = Random.Range(MinMiniStars, MaxMiniStars);
        for (int i = 0; i < AmountOfMiniStars; i++)
        {
            GameObject CreatedMiniStar = Instantiate(MiniStarPrefabs[Random.Range(0, MiniStarPrefabs.Count)], transform.position, transform.rotation);
            CreatedMiniStar.transform.localRotation = Quaternion.EulerAngles(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            CreatedMiniStar.transform.parent = this.gameObject.transform;
            MiniStars.Add(CreatedMiniStar);
            MiniStarsOrbitAxis.Add(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
            MiniStarsOrbitSpeed.Add(Random.Range(MinOrbitSpeed, MaxOrbitSpeed));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < MiniStars.Count; i++)
        {
            MiniStars[i].transform.Rotate(MiniStarsOrbitAxis[i], MiniStarsOrbitSpeed[i] * Time.deltaTime);
        }
    }
}
