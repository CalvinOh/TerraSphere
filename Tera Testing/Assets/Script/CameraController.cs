using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Speed of rotation, 100 suggested")]
    private float speed = 100;

    [SerializeField]
    [Tooltip("Plug in thid person camera")]
    private GameObject TPSCamera;
    [SerializeField]
    [Tooltip("Plug in top down camera")]
    private GameObject TDCamera;







    // Start is called before the first frame update
    void Start()
    {
        //locks cursor to screen and hides.
        Cursor.lockState= CursorLockMode.Locked;   
    }
    
    // Update is called once per frame
    void Update()
    {
        //rotate player as according to mouse X axis
        transform.Rotate(( new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * speed);

        if (Input.GetKeyDown(KeyCode.V))
        {
            TPSCamera.active = !TPSCamera.active;
            TDCamera.active = !TDCamera.active;
        }


    }
}
