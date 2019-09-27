﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerV2 : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    public GameObject planet;

    [SerializeField]
    Inventory invScript;

    [SerializeField]
    int itemInInventorySelected;

    [SerializeField]
    public Transform spawnLocation;
    [SerializeField]
    public GameObject currentlyHolding;
    [SerializeField]
    GameObject blankSlot;
    [SerializeField]
    private float jumpHeight;

    private Vector3 moveDirection;
    private bool itemSelected;
    private bool hasJumped;

    private void Start()
    {
        //inventory = new GameObject[10];
        if(invScript == null)
        {
            invScript = this.gameObject.GetComponent<Inventory>();
        }
        if(planet == null)
        {
            planet = FindObjectOfType<GravityAttractor>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SelectItem();
        if (!itemSelected)
        {
            // Since Item was switched, destroy what was previously held and assign the new value
            Destroy(currentlyHolding.gameObject);
            DisplayItemSelected();
            itemSelected = true;
        }
        Jump();
        UseItem();
    }

    private void Movement()
    {
        if (!invScript.inventoryDisplaying)
        {
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }
    }

    private void DisplayItemSelected()
    {
        // Assigning the currently held item

        // Previously working
        //currentlyHolding = (GameObject)Instantiate(inventory[itemInInventorySelected], spawnLocation.transform.position, spawnLocation.transform.rotation);
        //currentlyHolding.transform.parent = spawnLocation.transform;
        
        
        //currentlyHolding.transform.position = spawnLocation.transform.position;
        //GameObject item = (GameObject)Instantiate(currentlyHolding, spawnLocation.transform);
        //item.transform.parent = this.transform;



    }

    private void SelectItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && invScript.slot[0] != null)
        {
            itemInInventorySelected = 0;
            itemSelected = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && invScript.slot[1] != null)
        {
            itemInInventorySelected = 1;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && invScript.slot[2] != null)
        {
            itemInInventorySelected = 2;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && invScript.slot[3] != null)
        {
            itemInInventorySelected = 3;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && invScript.slot[4] != null)
        {
            itemInInventorySelected = 4;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && invScript.slot[5] != null)
        {
            itemInInventorySelected = 5;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && invScript.slot[6] != null)
        {
            itemInInventorySelected = 6;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && invScript.slot[7] != null)
        {
            itemInInventorySelected = 7;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && invScript.slot[8] != null)
        {
            itemInInventorySelected = 8;
            itemSelected = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && invScript.slot[9] != null)
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
        if (!invScript.inventoryDisplaying)
        {
            if (Input.GetButtonDown("Jump") && hasJumped == false)
            {
                Debug.Log("Jumping");
                this.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * jumpHeight);
                hasJumped = true;

            }
        }
    }

    void UseItem()
    {
        if(!invScript.inventoryDisplaying)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (invScript.slot[itemInInventorySelected] != null)
                {
                    invScript.slot[itemInInventorySelected].GetComponent<Slot>().useItem();
                    print("Item Used");
                }
                //Seed, 
                if (currentlyHolding.CompareTag("Seed"))
                {
                    //PlantSeed();
                }
            }
        }        
    }

    //void PlantSeed()
    //{
    //    //Spawn a new seed/plant onto the world, and starts the plant growth cycle, removes a stack until 0 and removes from inventory.
    //    GameObject newSeed = (GameObject)Instantiate(inventory[itemInInventorySelected], spawnLocation.transform.position, spawnLocation.transform.rotation);
    //    blankSlot = new GameObject();
    //    inventory[itemInInventorySelected] = blankSlot;
    //    currentlyHolding = inventory[itemInInventorySelected];

    //    //check for stack, if yes -- from stack; else remove from inventory;

    //}


    private void OnCollisionEnter(Collision collision)
    {
        hasJumped = false;
    }
}
