﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The toggle the inventory starts on.")]
    private GameObject toggleStart;

    public bool inventoryDisplaying = true;
    public GameObject seedInventoryCanvas;
    public GameObject plantInventoryCanvas;
    //public GameObject seedInventory;
    //public GameObject plantInventory;

    private int totalSeedSlots;
    private int totalPlantSlots;
    private int enabledSlots;
    //public int itemsPickedUp;
    public GameObject[] seedSlot;
    public GameObject[] plantSlot;
    private EventSystem eventSystem;
    private GameObject esLastSelected;

    public GameObject seedSlotHolder;
    public GameObject plantSlotHolder;
    public bool seedTab = true; // True = seed tab, false = plant tab
    public Text seedTextBox;
    public Text plantTextBox;

    //Check Controller Connected
    string[] connectedControllers;

    private void Start()
    {
        // Find how many slots the UI has for the inventory
        totalSeedSlots = seedSlotHolder.GetComponentsInChildren<Slot>().Length;
        seedSlot = new GameObject[totalSeedSlots];
        totalPlantSlots = seedSlotHolder.GetComponentsInChildren<Slot>().Length;
        plantSlot = new GameObject[totalPlantSlots];

        eventSystem = FindObjectOfType<EventSystem>();

        connectedControllers = Input.GetJoystickNames();

        if(toggleStart==null)
        {
            toggleStart = seedSlotHolder.GetComponentInChildren<Toggle>().gameObject;
        }

        for(int i = 0; i < totalSeedSlots; i++)
        {
            seedSlot[i] = seedSlotHolder.transform.GetChild(i).gameObject;

            if (seedSlot[i].GetComponent<Slot>().item == null)
            {
                seedSlot[i].GetComponent<Slot>().empty = true;
            }
        }
        for (int i = 0; i < totalPlantSlots; i++)
        {
            plantSlot[i] = plantSlotHolder.transform.GetChild(i).gameObject;

            if (plantSlot[i].GetComponent<Slot>().item == null)
            {
                plantSlot[i].GetComponent<Slot>().empty = true;
            }
        }
        seedTab = true;

        ToggleDisplayInventory();
    }

    void Update()
    {
        if(connectedControllers.Length > 0)
        {
            for(int i = 0; i < connectedControllers.Length; i++)
            {
                if(!string.IsNullOrEmpty(connectedControllers[i]))
                {
                    if(Input.GetButtonDown("Open Inventory"))
                    {
                        ToggleDisplayInventory();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ToggleDisplayInventory();

                        if (inventoryDisplaying)
                            Cursor.lockState = CursorLockMode.None;
                        else if (!inventoryDisplaying)
                            Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
        }


        //if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Open Inventory"))
        //{
        //    ToggleDisplayInventory();

        //    if (inventoryDisplaying)
        //        Cursor.lockState = CursorLockMode.None;
        //    else if (!inventoryDisplaying)
        //        Cursor.lockState = CursorLockMode.Locked;
        //}

        if(inventoryDisplaying)
        {
            if (Input.GetButtonDown("Right Bumper") || Input.GetButtonDown("Left Bumper"))
            {
                seedTab = !seedTab;
                ToggleTabDisplayed();
            }
            UpdateDescriptionBox();
        }
        print(eventSystem.currentSelectedGameObject);
    }

    private void UpdateDescriptionBox()
    {
        if (eventSystem.currentSelectedGameObject != esLastSelected)
        {
            if(seedTab)
                seedTextBox.text = eventSystem.currentSelectedGameObject.GetComponent<Slot>().item.GetComponent<Item>().itemDescriptionBoxContent();
            else
                plantTextBox.text = eventSystem.currentSelectedGameObject.GetComponent<Slot>().item.GetComponent<Item>().itemDescriptionBoxContent();
        }
        esLastSelected = eventSystem.currentSelectedGameObject;
    }

    public void ToggleDisplayInventory()
    {
        eventSystem.SetSelectedGameObject(toggleStart);
        inventoryDisplaying = !inventoryDisplaying;
        if (inventoryDisplaying)
        {
            ToggleTabDisplayed();
        }
        else
        {
            seedInventoryCanvas.GetComponentInParent<Canvas>().targetDisplay = 7;
            plantInventoryCanvas.GetComponent<Canvas>().targetDisplay = 7;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ToggleTabDisplayed()
    {
        if (seedTab)
        {
            seedInventoryCanvas.GetComponent<Canvas>().targetDisplay = 0;
            plantInventoryCanvas.GetComponent<Canvas>().targetDisplay = 7;

            toggleStart = seedSlotHolder.GetComponentInChildren<Toggle>().gameObject;
            eventSystem.SetSelectedGameObject(toggleStart);
            // making sure that all of the slots are not "chosen"
            for (int i = 0; i < seedSlot.Length; i++)
            {
                seedSlot[i].GetComponent<Toggle>().isOn = false;
            }
            //Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            seedInventoryCanvas.GetComponent<Canvas>().targetDisplay = 7;
            plantInventoryCanvas.GetComponent<Canvas>().targetDisplay = 0;

            toggleStart = plantSlotHolder.GetComponentInChildren<Toggle>().gameObject;
            eventSystem.SetSelectedGameObject(toggleStart);
            // making sure that all of the slots are not "chosen"
            for (int i = 0; i < plantSlot.Length; i++)
            {
                plantSlot[i].GetComponent<Toggle>().isOn = false;
            }
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            if (item.type == "Seed")
            {
                item.tag = "Seed";
            }
            else if (item.type == "Plant")
            {
                item.tag = "Plant";
            }

            AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon, item.subType, item.stackNumber);
            //item.gameObject.transform.parent = slot[0].GetComponent<Slot>().player.gameObject.GetComponent<PlayerController>().spawnHeldLocation;
            //SetSeedSlot();

            //seedItem and ediblePlant
            //if()
            //other.gameObject.transform.position.Set(0, 0, 0);
        }
    }

    //private void SetSeedSlot()
    //{
    //    if (slot[0].GetComponent<Slot>().player.GetComponent<PlayerController>().hotBarInventory[1] == null)
    //    {
    //        for (int i = 0; i < slot.Length; i++)
    //        {
    //            if (slot[i].GetComponent<Slot>().item.tag == "Seed")
    //            {
    //                slot[0].GetComponent<Slot>().player.GetComponent<PlayerController>().hotBarInventory[1] =
    //                    slot[i].GetComponent<Slot>().item.GetComponent<Item>().gameObject;
    //            }
    //        }
    //    }
    //}

        // CLEAN UP REQUIRED - PT
    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon, string subType, int stackNumber)
    {
        if(itemObject.tag == "Seed")
        {
            for (int i = 0; i < totalSeedSlots; i++)
            {
                // Already has this kind of item, stack instead of add new.
                if (!seedSlot[i].GetComponent<Slot>().empty && seedSlot[i].GetComponent<Slot>().item.GetComponent<Item>().ID == itemID)
                {
                    seedSlot[i].GetComponent<Slot>().stackNumber++;
                    seedSlot[i].GetComponent<Slot>().item.GetComponent<Item>().stackNumber++;
                    itemObject.transform.position = new Vector3(0, 0, 0);
                    return;
                }
                else if (seedSlot[i].GetComponent<Slot>().empty)
                {
                    itemObject.GetComponent<Item>().pickedUp = true;

                    seedSlot[i].GetComponent<Slot>().item = itemObject;
                    seedSlot[i].GetComponent<Slot>().icon = itemIcon;
                    seedSlot[i].GetComponent<Slot>().type = itemType;
                    seedSlot[i].GetComponent<Slot>().ID = itemID;
                    seedSlot[i].GetComponent<Slot>().description = itemDescription;
                    seedSlot[i].GetComponent<Slot>().subType = subType;
                    seedSlot[i].GetComponent<Slot>().stackNumber = 1;


                    itemObject.transform.position = new Vector3(0, 0, 0);


                    seedSlot[i].GetComponent<Slot>().UpdateSlot();
                    seedSlot[i].GetComponent<Slot>().empty = false;
                    return;
                }
            }
        }
        else if (itemObject.tag == "Plant")
        {
            for (int i = 0; i < totalSeedSlots; i++)
            {
                // Already has this kind of item, stack instead of add new.
                if (!plantSlot[i].GetComponent<Slot>().empty && plantSlot[i].GetComponent<Slot>().item.GetComponent<Item>().ID == itemID)
                {
                    plantSlot[i].GetComponent<Slot>().stackNumber++;
                    plantSlot[i].GetComponent<Slot>().item.GetComponent<Item>().stackNumber++;
                    itemObject.transform.position = new Vector3(0, 0, 0);
                    return;
                }
                else if (plantSlot[i].GetComponent<Slot>().empty)
                {
                    itemObject.GetComponent<Item>().pickedUp = true;

                    plantSlot[i].GetComponent<Slot>().item = itemObject;
                    plantSlot[i].GetComponent<Slot>().icon = itemIcon;
                    plantSlot[i].GetComponent<Slot>().type = itemType;
                    plantSlot[i].GetComponent<Slot>().ID = itemID;
                    plantSlot[i].GetComponent<Slot>().description = itemDescription;
                    plantSlot[i].GetComponent<Slot>().subType = subType;
                    plantSlot[i].GetComponent<Slot>().stackNumber = 1;


                    itemObject.transform.position = new Vector3(0, 0, 0);


                    plantSlot[i].GetComponent<Slot>().UpdateSlot();
                    plantSlot[i].GetComponent<Slot>().empty = false;
                    return;
                }
            }
        }
    }

}
