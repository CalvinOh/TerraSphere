using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Movement Variables

    //Player Move Speed
    [SerializeField]
    float moveSpeed;
    //How fast the player can rotate
    [SerializeField]
    [Tooltip("Speed of rotation, 100 suggested")]
    private float speed = 100;
    //How long it will take to move at full speed. 
    [SerializeField]
    private float SecondsTakenToFullSpeed =1;
    //What direction the player will move in
    private Vector3 moveDirection;
    //Multiplier for move faster in a direction 
    private float MoveSpeedCurrentMultiplier;

    //Game Object Variables 

    //Reference to Inventory Script
    [SerializeField]
    Inventory invScript;
    //The gameObject that the player is currently selecting wihtin the trigger box
    [SerializeField]
    private GameObject currentlySelecting;
    //The currently selected seed item
    [SerializeField]
    public GameObject seedItem;
    //The ground prefab that the player spawns in
    [SerializeField]
    private GameObject groundHole;
    //Knowing the planet 
    [SerializeField]
    public GameObject planet;

    //Variables using the Trigger Box

    //The location where the item will spawn, is a trigger box
    [SerializeField]
    Transform spawnItemLocation;
    //A list of all gameObejcts in the trigger box
    public List<GameObject> objectsInTrigger;


    //Player Oxygen Value

    //The current value for the players oxygen value
    [SerializeField]
    public float oxygenValue;
    //How much oxygen is being decreased 
    [SerializeField]
    private float oxygenDecreaseValue;
    //Max oxygen value
    public float oxygenMax = 100f;

    //UI

    //UI for context based direction.
    private ContextBasedUI contextBasedUI;

    

    private void Start()
    {
        if(contextBasedUI == null)
        {
            contextBasedUI = FindObjectOfType<ContextBasedUI>();
        }
        MoveSpeedCurrentMultiplier = 0;

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


        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

       
        if (!invScript.inventoryDisplaying)
        {
            RotateCamera();
            ContextSelectingItem();
        }
       
        ReassigningPlanetAsBaseSelection();

        DecreaseOxygen();
    }
    private void FixedUpdate()
    {
        Movement();
    }

    //Player Losing Oxygen
    private void DecreaseOxygen()
    {
        oxygenValue -= oxygenDecreaseValue;
        oxygenValue = Mathf.Clamp(oxygenValue, 0f, 100f);
    }

    //After the player walks away from an object, this makes the planet as the selected object. 
    private void ReassigningPlanetAsBaseSelection()
    {
        if (objectsInTrigger.Count == 0 || (objectsInTrigger.Count == 1 && objectsInTrigger[0] == planet))
        {
            currentlySelecting = planet;
           
        }

    }

    //Functions that the player call when needing to interact with a gameObject in the game. 

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
            GameObject newSeed = (GameObject)Instantiate(seedItem.GetComponent<SeedItem>().plantToGrowInto, spawnItemLocation.transform.position, spawnItemLocation.transform.rotation);
            newSeed.gameObject.GetComponent<PlantGrowth>().Grow = true;
            print("Planting");

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
    private void RotateCamera()
    {
        transform.Rotate((new Vector3(0, Input.GetAxis("Right Joystick X"), 0)) * Time.deltaTime * speed);
        transform.Rotate((new Vector3(0, Input.GetAxis("Mouse X"), 0)) * Time.deltaTime * speed);

        //button switch for the two cameras

    }

    //OnTriggerEnter Functions

    private void OnTriggerEnter(Collider other)
    {
        objectsInTrigger.Add(other.gameObject);
        currentlySelecting = other.gameObject;
    
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInTrigger.Remove(other.gameObject);
       
        //Outline still in development, not going to be in alpha

    }
}
