using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    GameObject[] inventory = new GameObject[10];

    [SerializeField]
    int itemInInventorySelected;

    [SerializeField]
    Transform spawnHeldLocation;
    [SerializeField]
    Transform spawnItemLocation;
    [SerializeField]
    GameObject currentlyHolding;
    [SerializeField]
    GameObject blankSlot;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private GameObject currentlySelecting;
    [SerializeField]
    private GameObject shovel;
    [SerializeField]
    private GameObject rake;
    [SerializeField]
    private GameObject wateringCan;

    [SerializeField]
    [Tooltip("Speed of rotation, 100 suggested")]
    private float speed = 100;

    [SerializeField]
    [Tooltip("Plug in thid person camera")]
    private GameObject TPSCamera;
    [SerializeField]
    [Tooltip("Plug in top down camera")]
    private GameObject TDCamera;

    [SerializeField]
    private GameObject planet;

    public List<GameObject> objectsInTrigger;

    private Vector3 moveDirection;
    private bool itemSelected;
    private bool hasJumped;

    private void Start()
    {
        //inventory = new GameObject[10];
        currentlyHolding = inventory[0];
        if(planet == null)
        {
            
            planet = FindObjectOfType<GravityAttractor>().gameObject;
            print("Planet Found");
        }
        objectsInTrigger = new List<GameObject>();

        shovel.transform.position = spawnHeldLocation.transform.position;
        rake.transform.position = spawnHeldLocation.transform.position;
        wateringCan.transform.position = spawnHeldLocation.transform.position;

        inventory[0] = shovel;
        inventory[2] = rake;
        inventory[3] = wateringCan;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        //SelectItemKeyboard();
        SelectItemController();
        RotateCamera();
        ChangeCameraView();
        if(!itemSelected)
        {
            // Since Item was switched, destroy what was previously held and assign the new value
            Destroy(currentlyHolding.gameObject);
            DisplayItemSelected();
            itemSelected = true;
        }
        Jump();
        UseItem();

        if (objectsInTrigger.Count == 0 || (objectsInTrigger.Count == 1 && objectsInTrigger[0] == planet))
        {
            currentlySelecting = planet;
        }
    }


    private void DisplayItemSelected()
    {
        // Assigning the currently held item
        currentlyHolding = (GameObject)Instantiate(inventory[itemInInventorySelected], spawnHeldLocation.transform.position, spawnHeldLocation.transform.rotation);
        currentlyHolding.transform.parent = spawnHeldLocation.transform;
        //currentlyHolding.transform.position = spawnLocation.transform.position;
        //GameObject item = (GameObject)Instantiate(currentlyHolding, spawnLocation.transform);
        //item.transform.parent = this.transform;

    }

    private void SelectItemKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory[0] != null)
        {
            itemInInventorySelected = 0;
            itemSelected = false;
        }
            
        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory[1] != null)
        {
            itemInInventorySelected = 1;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && inventory[2] != null)
        {
            itemInInventorySelected = 2;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && inventory[3] != null)
        {
            itemInInventorySelected = 3;
            itemSelected = false;
        }
   
    }

    private void SelectItemController()
    {
        if(Input.GetButtonDown("Left Bumper"))
        {

            print("leftbumper");
           if(itemInInventorySelected == 0)
            {
                itemInInventorySelected = 3;
                itemSelected = false;
            }
           else
            {
                itemInInventorySelected--;
                itemSelected = false;
            }
        }
        if(Input.GetButtonDown("Right Bumper"))
        {
            print("rightbumper");
            if (itemInInventorySelected == 3)
            {
                itemInInventorySelected = 0;
                itemSelected = false;
            }
            else
            {
                itemInInventorySelected++;
                itemSelected = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {

       this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
      
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && hasJumped == false)
        {
            Debug.Log("Jumping");
            this.gameObject.GetComponent<Rigidbody>().AddForce(transform.up *jumpHeight);
            hasJumped = true;
            
        }
    }

    void UseItem()
    {

        if(Input.GetButtonDown("Use Item"))
        {
            //When left click, based on item held, trigger function
            if (currentlyHolding.CompareTag("Shovel"))
            {
                DigGround();
            }
            if (currentlyHolding.CompareTag("Seed"))
            {
                PlantSeed();
            }
            if (currentlyHolding.CompareTag("Rake"))
            {
                CoverSeed();
            }
            if (currentlyHolding.CompareTag("WateringCan"))
            {
                Water();
            }
        }
    }

    private void Water()
    {
        if (currentlySelecting.CompareTag("Seeded") || currentlySelecting.CompareTag("Plant"))
        {
            //Trigger plant accelarate function
        }
    }

    private void CoverSeed()
    {
        if (currentlySelecting.CompareTag("Seed"))
        {
            //Trigger plant growth function plus cover up seed 
            print("Raking...");
        }
    }

    private void DigGround()
    {
        if(currentlySelecting.CompareTag("Ground"))
        {
            //Trigger change in ground
            //Spawn dug ground on floor
            print("Digging");
        }
    }

    void PlantSeed()
    {
        if(currentlySelecting.CompareTag("Dug"))
        {
            //Spawn a new seed/plant onto the world, and starts the plant growth cycle, removes a stack until 0 and removes from inventory.
            GameObject newSeed = (GameObject)Instantiate(inventory[itemInInventorySelected], spawnItemLocation.transform.position, spawnItemLocation.transform.rotation);
            blankSlot = new GameObject();
            inventory[itemInInventorySelected] = blankSlot;
            currentlyHolding = inventory[itemInInventorySelected];

            //check for stack, if yes -- from stack; else remove from inventory;
        }

    }


    private void RotateCamera()
    {
        transform.Rotate((new Vector3(0, Input.GetAxis("Right Joystick X"), 0)) * Time.deltaTime * speed);


        //button switch for the two cameras
      
    }

    private void ChangeCameraView()
    {
        if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Select"))
        {
            TPSCamera.active = !TPSCamera.active;
            TDCamera.active = !TDCamera.active;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hasJumped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        objectsInTrigger.Add(other.gameObject);
        currentlySelecting = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInTrigger.Remove(other.gameObject);

      
    }
}
