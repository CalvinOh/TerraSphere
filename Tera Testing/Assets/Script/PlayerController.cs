using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    //[SerializeField]
    //public GameObject[] hotBarInventory = new GameObject[10];
    [SerializeField]
    Inventory invScript;

    [SerializeField]
    int itemInInventorySelected;

    //[SerializeField]
    //public Transform spawnHeldLocation;
    [SerializeField]
    Transform spawnItemLocation;
    //[SerializeField]
    //GameObject currentlyHolding;
    //[SerializeField]
    //GameObject blankSlot;
    //[SerializeField]
    //private float jumpHeight;
    [SerializeField]
    private GameObject currentlySelecting;
    [SerializeField]
    public GameObject seedItem;
    [SerializeField]
    private GameObject groundHole;
    //private GameObject shovel;
    //[SerializeField]
    //private GameObject rake;
    //[SerializeField]
    //private GameObject wateringCan;

    //private GameObject spawnShovelItem;

    //private GameObject spawnRakeItem;
    //private GameObject spawnWateringGun;

    [SerializeField]
    public float oxygenValue;
    [SerializeField]
    private float oxygenDecreaseValue;

    public float oxygenMax = 100f;

    [SerializeField]
    [Tooltip("Speed of rotation, 100 suggested")]
    private float speed = 100;

    //[SerializeField]
    //[Tooltip("Plug in thid person camera")]
    //private GameObject TPSCamera;
    //[SerializeField]
    //[Tooltip("Plug in top down camera")]
    //private GameObject TDCamera;

    [SerializeField]
    public GameObject planet;

    public List<GameObject> objectsInTrigger;

    private Vector3 moveDirection;
    //private bool itemSelected;
    //private bool hasJumped;

    private float MoveSpeedCurrentMultiplier;

    private ContextBasedUI contextBasedUI;

    [SerializeField]
    private float SecondsTakenToFullSpeed =1;

    private void Start()
    {
        if(contextBasedUI == null)
        {
            contextBasedUI = FindObjectOfType<ContextBasedUI>();
        }
        MoveSpeedCurrentMultiplier = 0;
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

        //shovel.transform.position = spawnHeldLocation.transform.position;
        //rake.transform.position = spawnHeldLocation.transform.position;
        //wateringCan.transform.position = spawnHeldLocation.transform.position;

        //hotBarInventory[0] = shovel;
        //hotBarInventory[2] = rake;
        //hotBarInventory[3] = wateringCan;

        //currentlyHolding = hotBarInventory[0];
        Cursor.lockState = CursorLockMode.Locked;

        //SpawnItems();

    }

    // Update is called once per frame
    void Update()
    {
        //SelectItemKeyboard();
        //SelectItemController();
       
        if (!invScript.inventoryDisplaying)
        {
            RotateCamera();
            ContextSelectingItem();
            //Jump();
            //ChangeCameraView();
        }

        //SetItemToPlayer();

        //if (!itemSelected)
        //{
        //    // Since Item was switched, destroy what was previously held and assign the new value
        //    //Destroy(currentlyHolding.gameObject);
           
        //    SetItemPosition();
        //    itemSelected = true;
        //}
        
       
        ReassigningPlanetAsBaseSelection();

        DecreaseOxygen();
    }

    private void DecreaseOxygen()
    {
        oxygenValue -= oxygenDecreaseValue;
        oxygenValue = Mathf.Clamp(oxygenValue, 0f, 100f);
    }

    private void ReassigningPlanetAsBaseSelection()
    {
        if (objectsInTrigger.Count == 0 || (objectsInTrigger.Count == 1 && objectsInTrigger[0] == planet))
        {
            currentlySelecting = planet;
           
        }

    }

    //private void SpawnItems()
    //{
    //    // Assigning the currently held item
    //    //currentlyHolding = (GameObject)Instantiate(hotBarInventory[itemInInventorySelected], spawnHeldLocation.transform.position, spawnHeldLocation.transform.rotation);
    //    //
    //    //GameObject item = (GameObject)Instantiate(currentlyHolding, spawnLocation.transform);
    //    //item.transform.parent = this.transform;
    //    spawnShovelItem = (GameObject)Instantiate(hotBarInventory[0], spawnHeldLocation.transform.position, spawnHeldLocation.transform.rotation);
    //    spawnSeedItem = (GameObject)Instantiate(hotBarInventory[1], spawnHeldLocation.transform.position, spawnHeldLocation.transform.rotation);
    //    spawnRakeItem = (GameObject)Instantiate(hotBarInventory[2], spawnHeldLocation.transform.position, spawnHeldLocation.transform.rotation);
    //    spawnWateringGun = (GameObject)Instantiate(hotBarInventory[3], spawnHeldLocation.transform.position, spawnHeldLocation.transform.rotation);

    //    spawnShovelItem.transform.parent = this.spawnHeldLocation;
    //    spawnSeedItem.transform.parent = this.spawnHeldLocation;
    //    spawnRakeItem.transform.parent = this.spawnHeldLocation;
    //    spawnWateringGun.transform.parent = this.spawnHeldLocation;

    //    spawnSeedItem.transform.position = planet.transform.position;
    //    spawnRakeItem.transform.position = planet.transform.position;
    //    spawnWateringGun.transform.position = planet.transform.position;


    //}

    //private void SetItemToPlayer()
    //{
    //    currentlyHolding = hotBarInventory[itemInInventorySelected];
    //}

    //private void SetItemPosition()
    //{
    //    if(itemInInventorySelected == 0)
    //    {
    //        spawnShovelItem.transform.position = spawnHeldLocation.transform.position;
    //        spawnSeedItem.transform.position = planet.transform.position;
    //        spawnRakeItem.transform.position = planet.transform.position;
    //        spawnWateringGun.transform.position = planet.transform.position;
    //    }
    //    else if (itemInInventorySelected == 1)
    //    {
    //        spawnShovelItem.transform.position = planet.transform.position;
    //        spawnSeedItem.transform.position = spawnHeldLocation.transform.position;
    //        spawnRakeItem.transform.position = planet.transform.position;
    //        spawnWateringGun.transform.position = planet.transform.position;
    //    }
    //    else if (itemInInventorySelected == 2)
    //    {
    //        spawnShovelItem.transform.position = planet.transform.position;
    //        spawnSeedItem.transform.position = planet.transform.position;
    //        spawnRakeItem.transform.position = spawnHeldLocation.transform.position;
    //        spawnWateringGun.transform.position = planet.transform.position;
    //    }
    //    else if (itemInInventorySelected == 3)
    //    {
    //        spawnShovelItem.transform.position = planet.transform.position;
    //        spawnSeedItem.transform.position = planet.transform.position;
    //        spawnRakeItem.transform.position = planet.transform.position;
    //        spawnWateringGun.transform.position = spawnHeldLocation.transform.position;
    //    }
    //}

    //private void SelectItemKeyboard()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1) && hotBarInventory[0] != null)
    //    {
    //        itemInInventorySelected = 0;
    //        itemSelected = false;
    //    }
            
    //    if (Input.GetKeyDown(KeyCode.Alpha2) && hotBarInventory[1] != null)
    //    {
    //        itemInInventorySelected = 1;
    //        itemSelected = false;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha3) && hotBarInventory[2] != null)
    //    {
    //        itemInInventorySelected = 2;
    //        itemSelected = false;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Alpha4) && hotBarInventory[3] != null)
    //    {
    //        itemInInventorySelected = 3;
    //        itemSelected = false;
    //    }
   
    //}

    //private void SelectItemController()
    //{
    //    if(Input.GetButtonDown("Left Bumper"))
    //    {

    //        print("leftbumper");
    //       if(itemInInventorySelected == 0)
    //        {
    //            itemInInventorySelected = 3;
               
    //            SetItemPosition();
                
    //            itemSelected = false;
    //        }
    //       else
    //        {
    //            itemInInventorySelected--;
  
    //            SetItemPosition();
               
    //            itemSelected = false;
    //        }



    //    }
    //    if(Input.GetButtonDown("Right Bumper"))
    //    {
    //        print("rightbumper");
    //        if (itemInInventorySelected == 3)
    //        {
    //            itemInInventorySelected = 0;
    
    //            SetItemPosition();
             
    //            itemSelected = false;
    //        }
    //        else
    //        {
    //            itemInInventorySelected++;
  
    //            SetItemPosition();
               
    //            itemSelected = false;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        Movement();
    }

    //Use Items

    void ContextSelectingItem()
    {

        if(Input.GetButtonDown("Use Item"))
        {
            //When left click, based on item held, trigger function
            if (currentlySelecting.CompareTag("Ground"))
            {
                DigHole();
            }
            if (currentlySelecting.CompareTag("Hole"))
            {
                CheckSeedNumber();

            }
            if (currentlySelecting.CompareTag("Seed"))
            {
                Water();
            }
            if(currentlySelecting.CompareTag("Plant"))
            {
                objectsInTrigger.Remove(currentlySelecting);
                contextBasedUI.AfterHarvest();
                currentlySelecting.gameObject.GetComponent<PlantGrowth>().Harvest();
            }
        }
    }

    private void Water()
    {
        if (currentlySelecting.CompareTag("Seed"))
        {
            //Trigger plant accelarate function
            print("Watering");
            currentlySelecting.gameObject.GetComponent<PlantGrowth>().Water(10f);
        }
    }


    private void DigHole()
    {
        if(currentlySelecting.CompareTag("Ground"))
        {
            print("Digging");
            Instantiate(groundHole, spawnItemLocation.transform.position, spawnItemLocation.transform.rotation);

        }
    }

    void PlantSeed()
    {
        if(currentlySelecting.CompareTag("Hole"))
        {
            //Spawn a new seed/plant onto the world, removes a stack until 0 and removes from inventory.
            GameObject newSeed = (GameObject)Instantiate(seedItem.GetComponent<SeedItem>().plantToGrowInto, spawnItemLocation.transform.position, spawnItemLocation.transform.rotation);
            newSeed.gameObject.GetComponent<PlantGrowth>().Grow = true;
            //blankSlot = new GameObject();
            //hotBarInventory[itemInInventorySelected] = blankSlot;
            //currentlyHolding = hotBarInventory[itemInInventorySelected];
            print("Planting");
            //check for stack, if yes -- from stack; else remove from inventory;
        }

    }

    void CheckSeedNumber()
    {
        if (seedItem.gameObject.GetComponent<Item>().stackNumber <= 1)
        {
            PlantSeed();
           
            seedItem.gameObject.GetComponent<Item>().stackNumber--; //Bugged Right now
            //hotBarInventory[itemInInventorySelected] = blankSlot; //Bugged, stays even when 0 and swapped.
           
        }
        //else if(currentlyHolding.gameObject.GetComponent<Item>().stackNumber > 1)
        //{
        //    PlantSeed();
        //    print("Decrease Stack");
        //    currentlyHolding.gameObject.GetComponent<Item>().stackNumber--;
           
        //}
    }

   

    //private void ChangeCameraView()
    //{
    //    if (Input.GetKeyDown(KeyCode.V) || Input.GetButtonDown("Select"))
    //    {
    //        TPSCamera.active = !TPSCamera.active;
    //        TDCamera.active = !TDCamera.active;
    //    }
    //}

    //Movement Functions
    private void Movement()
    {
        // Move only if the inventory menu is not present.
        if (!invScript.inventoryDisplaying)
        {
            
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            if (moveDirection != Vector3.zero)
            {
                MoveSpeedCurrentMultiplier += Time.deltaTime / SecondsTakenToFullSpeed;
            }
            else
            {
                MoveSpeedCurrentMultiplier = 0;
            }
            MoveSpeedCurrentMultiplier = Mathf.Clamp(MoveSpeedCurrentMultiplier, 0f, 1f);
            this.GetComponent<Rigidbody>().MovePosition(this.GetComponent<Rigidbody>().position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime*MoveSpeedCurrentMultiplier);
        }
    }

    //void Jump()
    //{
    //    if (Input.GetButtonDown("Jump") && hasJumped == false)
    //    {
    //        Debug.Log("Jumping");
    //        this.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpHeight);
    //        hasJumped = true;

    //    }
    //}

    private void RotateCamera()
    {
        transform.Rotate((new Vector3(0, Input.GetAxis("Right Joystick X"), 0)) * Time.deltaTime * speed);
        transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * speed);

        //button switch for the two cameras

    }






    private void OnTriggerEnter(Collider other)
    {
        objectsInTrigger.Add(other.gameObject);
        currentlySelecting = other.gameObject;
        //if(currentlySelecting.CompareTag("Plant"))
        //{
        //    currentlySelecting.gameObject.GetComponent<PlantGrowth>().ToggleOutline();
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInTrigger.Remove(other.gameObject);
       
        //Outline still in development, not going to be in alpha

    }
}
