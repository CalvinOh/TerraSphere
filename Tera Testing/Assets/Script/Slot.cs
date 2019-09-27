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
    public string description;
    public bool empty;

    public Transform slotIconGO;
    public Sprite icon;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //useItem();
        player.GetComponent<PlayerControllerV2>().currentlyHolding = item;
    }

    public void Start()
    {
        slotIconGO = transform.GetChild(0);
        player = FindObjectOfType<Inventory>().gameObject;
    }

    public void UpdateSlot()
    {
        slotIconGO.GetComponent<Image>().sprite = icon;
    }

    public void useItem()
    {
        item.GetComponent<Item>().itemUsage(this);
    }
}
