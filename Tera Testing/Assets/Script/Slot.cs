using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public GameObject player { get; private set; }
    public GameObject item;
    public int ID;
    public string type;
    public string subType;
    public string description;
    public int stackNumber; // how many of an item are in a stack for a slot.
    public bool empty;

    public Transform slotIconGO;
    public Sprite icon;
    public Text text;

    public void Start()
    {
        slotIconGO = transform.GetChild(1);//.GetChild(0);
        text = this.GetComponentInChildren<Text>();
        player = FindObjectOfType<Inventory>().gameObject;
        this.gameObject.GetComponent<Toggle>().isOn = false;

        if(item != null)
        {
            item = item.GetComponent<Item>().gameObject;
            Color temp = slotIconGO.GetComponent<Image>().color;
            slotIconGO.GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 1);
            icon = item.GetComponent<Item>().icon;
            type = item.GetComponent<Item>().type;
            ID = item.GetComponent<Item>().ID;
            description = item.GetComponent<Item>().description;
            subType = item.GetComponent<Item>().subType;
            stackNumber = 1;
            UpdateStackNumber();
            icon = item.GetComponent<Item>().icon;
            UpdateSlot();
        }
        else
        {
            this.gameObject.GetComponent<Toggle>().interactable = false;
        }
    }

    public void Update()
    {
        if (player.GetComponent<Inventory>().inventoryDisplaying)
        {
            CheckChosenSlot();
        }
    }

    private void CheckChosenSlot()
    {
        if (this.gameObject.GetComponent<Toggle>().isOn)
        {
            this.gameObject.GetComponent<Toggle>().isOn = false; // deactivate to prevent using infinite times
            if (item.GetComponent<Item>().type == "Plant" && stackNumber > 0)
            {
                UseItem();
            }
            else if (item.GetComponent<Item>().type == "Seed" && stackNumber > 0)
            {
                this.player.GetComponent<PlayerController>().seedItem = item;
                this.player.GetComponent<PlayerController>().contextBasedUI.GetComponent<ContextBasedUI>().seedIcon = item.GetComponent<Item>().icon;
                //this.player.GetComponent<PlayerController>().seedSlotBackground.GetComponent<Image>().sprite = item.GetComponent<Item>().icon;

            }
            this.player.GetComponent<Inventory>().nextDig = Time.time + 0.6f;
            this.player.GetComponent<Inventory>().ToggleDisplayInventory();
        }
    }

    public void UpdateSlot()
    {
        this.gameObject.GetComponent<Toggle>().interactable = true;
        slotIconGO.GetComponent<Image>().sprite = icon;
        Color temp = slotIconGO.GetComponent<Image>().color;
        slotIconGO.GetComponent<Image>().color = new Color(temp.r, temp.g, temp.b, 1);
        UpdateStackNumber();
        this.item.GetComponent<Item>().AssignParent(this);
        this.empty = false;
    }

    public void UseItem()
    {
        item.GetComponent<Item>().itemUsage(this);
        stackNumber--;
        UpdateStackNumber();
        if (stackNumber <= 0)
        {
            //empty = true;


        }
        else
        {

        }
    }

    public void UpdateStackNumber()
    {
        text.text = stackNumber.ToString();
    }
}
