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
    Transform spawnLocation;
    [SerializeField]
    GameObject currentlyHolding;

    private Vector3 moveDirection;
    private bool itemSelected;

    private void Start()
    {
        //inventory = new GameObject[10];
        currentlyHolding = inventory[0];
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        SelectItem();
        if(!itemSelected)
        {
            DisplayItemSelected();
            itemSelected = true;
        }
        
        PlaceItem();
    }

    private void PlaceItem()
    {
      
    }

    private void DisplayItemSelected()
    {
        currentlyHolding = inventory[itemInInventorySelected];
        //currentlyHolding.transform.position = spawnLocation.transform.position;
        GameObject item = (GameObject)Instantiate(currentlyHolding, spawnLocation.transform);
        item.transform.parent = this.transform;

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
}
