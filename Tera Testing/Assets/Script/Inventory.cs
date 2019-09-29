using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public bool inventoryDisplaying = true;
    public GameObject inventory;

    private int allSlots;
    private int enabledSlots;
    public GameObject[] slot;

    public GameObject slotHolder;

    private void Start()
    {
        // Find how many slots the UI has for the inventory
        allSlots = slotHolder.GetComponentsInChildren<Slot>().Length;
        slot = new GameObject[allSlots];

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

    private void ToggleDisplayInventory()
    {
        inventoryDisplaying = !inventoryDisplaying;
        if (inventoryDisplaying)
        {
            inventory.GetComponentInParent<Canvas>().targetDisplay = 0;
            Cursor.lockState = CursorLockMode.None;
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
            addItem(itemPickedUp, item.ID, item.type, item.description, item.icon);

            //seedItem and ediblePlant
            //if()
            //other.gameObject.transform.position.Set(0, 0, 0);
        }
    }

    void addItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {
        for(int i = 0; i < allSlots; i++)
        {
            if(slot[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                slot[i].GetComponent<Slot>().item = itemObject;
                slot[i].GetComponent<Slot>().icon = itemIcon;
                slot[i].GetComponent<Slot>().type = itemType;
                slot[i].GetComponent<Slot>().ID = itemID;
                slot[i].GetComponent<Slot>().description = itemDescription;

                itemObject.transform.position = new Vector3(0, 0, 0);
                //itemObject.transform.parent = slot[i].transform;
                //itemObject.SetActive(false);

                slot[i].GetComponent<Slot>().UpdateSlot();
                slot[i].GetComponent<Slot>().empty = false;
            }

            return;
        }
    }

}
