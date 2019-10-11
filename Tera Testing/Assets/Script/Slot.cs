using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject player;
    public GameObject item;
    public int ID;
    public string type;
    public string subType;
    public string description;
    public int stackNumber; // how many of an item are in a stack for a slot.
    public bool empty;

    public Transform slotIconGO;
    public Sprite icon;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(item.GetComponent<Item>().type == "Plant")
        {
            useItem();

        }
        if(item.GetComponent<Item>().type == "Seed")
        {
            
        }
        //player.GetComponent<PlayerControllerV2>().currentlyHolding = item;
    }

    public void Start()
    {
        slotIconGO = transform.GetChild(0);
        player = FindObjectOfType<Inventory>().gameObject;
        this.gameObject.GetComponent<Toggle>().isOn = false;

        if(item != null)
        {
            item = item.GetComponent<Item>().gameObject;
            icon = item.GetComponent<Item>().icon;
            type = item.GetComponent<Item>().type;
            ID = item.GetComponent<Item>().ID;
            description = item.GetComponent<Item>().description;
            subType = item.GetComponent<Item>().subType;
            stackNumber = 1;
            icon = item.GetComponent<Item>().icon;
            UpdateSlot();
        }
    }

    public void Update()
    {
        if(this.gameObject.GetComponent<Toggle>().isOn)
        {
            this.gameObject.GetComponent<Toggle>().isOn = false; // deactivate to prevent using infinite times
            if (item.GetComponent<Item>().type == "Plant")
            {
                useItem();
                this.player.GetComponent<Inventory>().ToggleDisplayInventory();
            }
            else if (item.GetComponent<Item>().type == "Seed")
            {

                this.player.GetComponent<PlayerController>().hotBarInventory[1] = item;
                this.player.GetComponent<PlayerController>().seedSlotBackground.GetComponent<Image>().sprite = item.GetComponent<Item>().icon;
                this.player.GetComponent<Inventory>().ToggleDisplayInventory();

            }
        }
    }



    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void useItem()
    {
        item.GetComponent<Item>().itemUsage(this);
        stackNumber--;
        if(stackNumber <= 0)
        {
            empty = true;

        }
    }
}
