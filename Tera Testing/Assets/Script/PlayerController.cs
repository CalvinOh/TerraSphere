using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    [SerializeField]
    public GameObject[] hotBarInventory = new GameObject[10];
    [SerializeField]
    Inventory invScript;

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
    public float oxygenValue;
    [SerializeField]
    private float oxygenDecreaseValue;

    public float oxygenMax = 100f;

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
        if (invScript == null)
        {
            invScript = this.gameObject.GetComponent<Inventory>();
        }
        if (planet == null)
        {
            
            planet = FindObjectOfType<GravityAttractor>().gameObject;
            print("Planet Found");
        }
        objectsInTrigger = new List<GameObject>();

        shovel.transform.position = spawnHeldLocation.transform.position;
        rake.transform.position = spawnHeldLocation.transform.position;
        wateringCan.transform.position = spawnHeldLocation.transform.position;

        hotBarInventory[0] = shovel;
        hotBarInventory[2] = rake;
        hotBarInventory[3] = wateringCan;

        currentlyHolding = hotBarInventory[0];
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        SelectItemKeyboard();
        SelectItemController();
        if (!invScript.inventoryDisplaying)
        {
            RotateCamera();
            Jump();
        }

        ChangeCameraView();
        if (!itemSelected)
        {
            // Since Item was switched, destroy what was previously held and assign the new value
            Destroy(currentlyHolding.gameObject);
            DisplayItemSelected();
            itemSelected = true;
        }
        
        UseItem();

        ReassigningPlanetAsBaseSelection();

        DecreaseOxygen();
    }

    private void DecreaseOxygen()
    {
        oxygenValue -= oxygenDecreaseValue;
    }

    private void ReassigningPlanetAsBaseSelection()
    {
        if (objectsInTrigger.Count == 0 || (objectsInTrigger.Count == 1 && objectsInTrigger[0] == planet))
        {
            currentlySelecting = planet;
           
        }

    }

    private void DisplayItemSelected()
    { 
        // Assigning the currently held item
        currentlyHolding = (GameObject)Instantiate(hotBarInventory[itemInInventorySelected], spawnHeldLocation.transform.position, spawnHeldLocation.transform.rotation);
        currentlyHolding.transform.parent = spawnHeldLocation.transform;
        //GameObject item = (GameObject)Instantiate(currentlyHolding, spawnLocation.transform);
        //item.transform.parent = this.transform;

    }

    private void SelectItemKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hotBarInventory[0] != null)
        {
            itemInInventorySelected = 0;
            itemSelected = false;
        }
            
        if (Input.GetKeyDown(KeyCode.Alpha2) && hotBarInventory[1] != null)
        {
            itemInInventorySelected = 1;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && hotBarInventory[2] != null)
        {
            itemInInventorySelected = 2;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && hotBarInventory[3] != null)
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
        // Move only if the inventory menu is not present.
        if (!invScript.inventoryDisplaying)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
        }
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
                Dig();
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
            currentlySelecting.gameObject.GetComponent<PlantGrowth>().Water(10f);
        }
    }

    private void CoverSeed()
    {
        if (currentlySelecting.CompareTag("Seed"))
        {
            //Trigger plant growth function plus cover up seed 
            print("Raking...");

            currentlySelecting.gameObject.GetComponent<PlantGrowth>().Grow = true;

        }
    }

    private void Dig()
    {
        if(currentlySelecting.CompareTag("Plant"))
        {
            print("Harvesting");
            currentlySelecting.gameObject.GetComponent<PlantGrowth>().Harvest();
            
        }
    }

    void PlantSeed()
    {
        if(currentlySelecting.CompareTag("Ground"))
        {
            //Spawn a new seed/plant onto the world, removes a stack until 0 and removes from inventory.
            GameObject newSeed = (GameObject)Instantiate(hotBarInventory[itemInInventorySelected].gameObject.GetComponent<SeedItem>().plantToGrowInto, spawnItemLocation.transform.position, spawnItemLocation.transform.rotation);
            //blankSlot = new GameObject();
            //hotBarInventory[itemInInventorySelected] = blankSlot;
            //currentlyHolding = hotBarInventory[itemInInventorySelected];
            print("Planting");
            //check for stack, if yes -- from stack; else remove from inventory;
        }

    }


    private void RotateCamera()
    {
        transform.Rotate((new Vector3(0, Input.GetAxis("Right Joystick X"), 0)) * Time.deltaTime * speed);
        transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * speed);

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
        if(currentlySelecting.CompareTag("Plant"))
        {
            currentlySelecting.gameObject.GetComponent<PlantGrowth>().ToggleOutline();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInTrigger.Remove(other.gameObject);
       
        //Outline still in development, not going to be in alpha

    }
}
