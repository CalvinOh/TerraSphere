using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Slot ParentSlot { get; private set; }
    public int ID;
    public string type; // seed, tool, etc. Like an internal tag
    public string description;
    public Sprite icon; // the icon used for the inventory menu
    public bool pickedUp;

    [SerializeField]
    float oxygenValue;

    //Calvin - Edits
    [SerializeField]
    public int stackNumber;
    [SerializeField]
    public string subType; //Secondary tag for seeds, ie. Shroom Seed etc
    

    public void Start()
    {
        
    }


    public void Update()
    {

    }

    public void AssignParent(Slot parent)
    {
        this.ParentSlot = parent;
    }

    public void itemUsage(Slot itemFromSlot)
    {
        if(type == "Plant" || this.tag == "Plant")
        {
            print("Used Plant!");
            itemFromSlot.player.GetComponent<PlayerController>().oxygenValue += this.oxygenValue;
        }
        stackNumber--;
    }

    public string itemDescriptionBoxContent()
    {
        return ($"Item: {subType}\n\nDescription: {description}" ?? "Slot is Empty");
    }

    /*ckrueger audio*/
    private void PlaySoundPlantSeed()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_int_seed_plant", gameObject);
    }
}
