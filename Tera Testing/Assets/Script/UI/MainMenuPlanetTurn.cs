using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPlanetTurn : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The greater this number the quicker the rate of the spinning of the planet.")]
    private float turnSpeed;

    [SerializeField]
    private Transform planetTransform;

    private void FixedUpdate()
    {
        planetTransform.Rotate(0, turnSpeed, 0);
    }
}
