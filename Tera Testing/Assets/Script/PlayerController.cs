using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject currentlySelecting;
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

    [SerializeField]
    private GameObject Hoe;
    [SerializeField]
    private GameObject WaterCan;
    [SerializeField]
    private GameObject Spade;
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
    public ContextBasedUI contextBasedUI { get; private set; }
    [SerializeField]
    private Animator MyAnimator;

    int firstPass = 0;

    private void Start()
    {

        /*v ckrueger audio v*/
        //play oxygen low sound when oxygen reaches low levels
        if (oxygenValue <= 15f)
        {
            InvokeRepeating("PlaySoundLowOxygen", 0f, 5f);
        }
        /*^ ckrueger audio ^*/

        if (contextBasedUI == null)
        
        //MyAnimator = GetComponent<Animator>();
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

        if (invScript.seedSlot.Length < 6)
            firstPass = 1;
        //    invScript.SetUpSeedAndPlantSlots();
        if(firstPass == 0 && seedItem == null)
            seedItem = invScript.seedSlot[0].GetComponent<Slot>().item;
        contextBasedUI.seedIcon = seedItem.GetComponent<Item>().icon;

        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        EasyMode();


        if (!invScript.inventoryDisplaying)
        {
            if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("breath") || MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
            {
                RotateCamera();
                ContextSelectingItem();
            }
        }

        ReassigningPlanetAsBaseSelection();
        HandleAnimatioons();
        DecreaseOxygen();
        ChangeCurrentlySelecting();
    }

    private void ChangeCurrentlySelecting()
    {
        int i = objectsInTrigger.IndexOf(currentlySelecting);

        if(Input.GetButtonDown("Right Bumper"))
        {
            i++;
            if(objectsInTrigger.IndexOf(currentlySelecting) != objectsInTrigger.Count-1)
            {
                currentlySelecting = objectsInTrigger[i];
            }
            else
            {
                currentlySelecting = objectsInTrigger[0];
            }
            
        }
        if (Input.GetButtonDown("Left Bumper"))
        {
            i--;
            if (objectsInTrigger.IndexOf(currentlySelecting) != objectsInTrigger.IndexOf(objectsInTrigger[0]))
            {
                currentlySelecting = objectsInTrigger[i];
            }
            else
            {
                currentlySelecting = objectsInTrigger[objectsInTrigger.Count-1];
            }
        }
    }

    private void EasyMode()
    {
        if (firstPass < 4)
            firstPass += 1;
        if (firstPass == 1)
            // Success
            if (firstPass == 3)
                seedItem = invScript.seedSlot[0].GetComponent<Slot>().item;
    }

    private void FixedUpdate()
    {
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("breath") || MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        Movement();
    }

    //Player Losing Oxygen
    private void DecreaseOxygen()
    {
        oxygenValue -= oxygenDecreaseValue;
        oxygenValue = Mathf.Clamp(oxygenValue, 0f, 100f);
    }

    private void HandleAnimatioons()
    {
        MyAnimator.SetFloat("Speed", MoveSpeedCurrentMultiplier);
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("breath") || MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("walk"))
        {
            Hoe.SetActive(false);
            WaterCan.SetActive(false);
            Spade.SetActive(false);
        }
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("dig"))
        {
            Spade.SetActive(true);
        }
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("hoeing"))
        {
            Hoe.SetActive(true);
        }
        if (MyAnimator.GetCurrentAnimatorStateInfo(0).IsName("water"))
        {
            WaterCan.SetActive(true);
        }
    }

    public void PlayEatingAnimation()
    {
        MyAnimator.Play("eat");
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
        if(!invScript.GetComponent<Inventory>().inventoryDisplaying)
        {

            if (Input.GetButtonDown("Use Item"))
            {
                //When left click, based on item held, trigger function
                if (currentlySelecting.CompareTag("Ground") && (Time.time >= invScript.nextDig))
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
                if (currentlySelecting.CompareTag("Plant"))
                {
                    objectsInTrigger.Remove(currentlySelecting);
                    contextBasedUI.AfterHarvest();
                    currentlySelecting.gameObject.GetComponent<PlantGrowth>().Harvest();
                    
                        PlaySoundPlantHarvest();
                }

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

            /*ckrueger audio*/
            PlaySoundWater();

            MyAnimator.Play("water");

        }
    }
    private void DigHole()
    {
        if(currentlySelecting.CompareTag("Ground"))
        {
            print("Digging");
            Instantiate(groundHole, spawnItemLocation.transform.position, spawnItemLocation.transform.rotation);

            /*ckrueger audio*/
            PlaySoundShovel();

            MyAnimator.Play("dig");

        }
    }
    void PlantSeed()
    {
        if(currentlySelecting.CompareTag("Hole"))
        {
            

            GameObject newSeed = (GameObject)Instantiate(seedItem.GetComponent<SeedItem>().plantToGrowInto, currentlySelecting.transform.position, currentlySelecting.transform.rotation);
            newSeed.gameObject.GetComponent<PlantGrowth>().Grow = true;
            print("Planting");

            objectsInTrigger.Remove(currentlySelecting);
            Destroy(currentlySelecting);
            currentlySelecting = objectsInTrigger[0];

            /*ckrueger audio*/
            PlaySoundPlantSeed();

            MyAnimator.Play("pickup");

        }

    }
    void CheckSeedNumber()
    {
        if (seedItem.gameObject.GetComponent<Item>().stackNumber >= 1)
        {
            PlantSeed();


            seedItem.GetComponent<Item>().ParentSlot.GetComponent<Slot>().UseItem();
            //seedItem.gameObject.GetComponent<Item>().stackNumber--; //Bugged Right now

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
        if(other.gameObject.tag != "Grass"&& other.gameObject.tag != "Tree")
        {
            objectsInTrigger.Add(other.gameObject);
            currentlySelecting = other.gameObject;
        }
       
    
    }

    private void OnTriggerExit(Collider other)
    {
        objectsInTrigger.Remove(other.gameObject);
       
        //Outline still in development, not going to be in alpha

    }

    /*v ckrueger audio v*/
    private void PlaySoundPlantSeed()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_int_seed_plant", gameObject);
    }

    private void PlaySoundWater()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_int_water", gameObject);
    }

    private void PlaySoundPlantHarvest()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_int_plant_harvest", gameObject);
    }

    private void PlaySoundShovel()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_int_shovel", gameObject);
    }

    private void PlaySoundLowOxygen()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_ui_oxygen_low", gameObject);
    }
    /*^ ckrueger audio ^*/
}
