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

    private GameObject planet;

    public List<GameObject> objectsInTrigger;

    private Vector3 moveDirection;
    private bool itemSelected;
    private bool hasJumped;

    private void Start()
    {
        //inventory = new GameObject[10];
        currentlyHolding = inventory[0];
        if(planet = null)
        {
            planet = FindObjectOfType<GravityAttractor>().gameObject;
        }
        objectsInTrigger = new List<GameObject>();

        shovel.transform.position = spawnHeldLocation.transform.position;
        rake.transform.position = spawnHeldLocation.transform.position;
        wateringCan.transform.position = spawnHeldLocation.transform.position;

        inventory[0] = shovel;
        inventory[2] = rake;
        inventory[3] = wateringCan;

    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        SelectItem();
        if(!itemSelected)
        {
            // Since Item was switched, destroy what was previously held and assign the new value
            Destroy(currentlyHolding.gameObject);
            DisplayItemSelected();
            itemSelected = true;
        }
        Jump();
        UseItem();
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

    private void SelectItem()
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
        if (Input.GetKeyDown(KeyCode.Alpha5) && inventory[4] != null)
        {
            itemInInventorySelected = 4;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && inventory[5] != null)
        {
            itemInInventorySelected = 5;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && inventory[6] != null)
        {
            itemInInventorySelected = 6;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && inventory[7] != null)
        {
            itemInInventorySelected = 7;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && inventory[8] != null)
        {
            itemInInventorySelected = 8;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && inventory[9] != null)
        {
            itemInInventorySelected = 9;
            itemSelected = false;
        }
    }

    private void FixedUpdate()
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

        if(Input.GetButtonDown("Fire1"))
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

        if(objectsInTrigger.Count == 0 || (objectsInTrigger.Count == 1 && objectsInTrigger[0] == planet) )
        {
            currentlySelecting = planet;
        }
    }
}
