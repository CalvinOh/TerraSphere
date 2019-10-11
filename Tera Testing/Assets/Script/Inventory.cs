using System.Collections;
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
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    //public int itemsPickedUp;
    public GameObject[] slot;
    private EventSystem eventSystem;

    public GameObject slotHolder;

    private void Start()
    {
        // Find how many slots the UI has for the inventory
        allSlots = slotHolder.GetComponentsInChildren<Slot>().Length;
        slot = new GameObject[allSlots];

        eventSystem = FindObjectOfType<EventSystem>();

        for(int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slot[i].GetComponent<Slot>().item == null)
            {
                slot[i].GetComponent<Slot>().empty = true;
            }
        }

        ToggleDisplayInventory();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Open Inventory"))
        {
            ToggleDisplayInventory();
        }
    }

    public void ToggleDisplayInventory()
    {
        eventSystem.SetSelectedGameObject(toggleStart);
        inventoryDisplaying = !inventoryDisplaying;
        if (inventoryDisplaying)
        {
            inventory.GetComponentInParent<Canvas>().targetDisplay = 0;
            // making sure that all of the slots are not "chosen"
            for (int i = 0; i < slot.Length; i++)
            {
                slot[i].GetComponent<Toggle>().isOn = false;
            }
            //Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            inventory.GetComponentInParent<Canvas>().targetDisplay = 7;
            Cursor.lockState = CursorLockMode.Locked;
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

                //Loop through inventory and hot bar, if current seed exists then add to stack, if not add to inventory 
                //if(item.subType == "Shroom Seed")
                //{
                //    item.stackNumber++;
                //}
            }
            else if (item.type == "Plant")
            {
                item.tag = "Plant";//same as seed, add to stack if have, if not add new. 
            }

            addItem(itemPickedUp, item.ID, item.type, item.description, item.icon, item.subType, item.stackNumber);
            item.gameObject.transform.parent = slot[0].GetComponent<Slot>().player.gameObject.GetComponent<PlayerController>().spawnHeldLocation;
            SetSeedSlot();

            //seedItem and ediblePlant
            //if()
            //other.gameObject.transform.position.Set(0, 0, 0);
        }
    }

    private void SetSeedSlot()
    {
        if (slot[0].GetComponent<Slot>().player.GetComponent<PlayerController>().hotBarInventory[1] == null)
        {
            for (int i = 0; i < slot.Length; i++)
            {
                if (slot[i].GetComponent<Slot>().item.tag == "Seed")
                {
                    slot[0].GetComponent<Slot>().player.GetComponent<PlayerController>().hotBarInventory[1] =
                        slot[i].GetComponent<Slot>().item.GetComponent<Item>().gameObject;
                }
            }
        }
    }

    void addItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon, string subType, int stackNumber)
    {
        for(int i = 0; i < allSlots; i++)
        {
            // Already has this kind of item
            if (!slot[i].GetComponent<Slot>().empty && slot[i].GetComponent<Slot>().item.GetComponent<Item>().ID == itemID)
            {
                slot[i].GetComponent<Slot>().stackNumber++;
                itemObject.transform.position = new Vector3(0, 0, 0);
                return;
            }
            else if (slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().description = itemDescription;
                slot[i].GetComponent<Slot>().subType = subType;
                slot[i].GetComponent<Slot>().stackNumber = 1;


                itemObject.transform.position = new Vector3(0, 0, 0);


                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;
                return;
            }
        }
    }

}
