using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The toggle the inventory starts on.")]
    private GameObject toggleStartS;
    [SerializeField]
    private GameObject toggleStartP;

    public bool inventoryDisplaying = true;
    public GameObject seedInventoryCanvas;
    public GameObject plantInventoryCanvas;

    private int totalSeedSlots;
    private int totalPlantSlots;
    private int enabledSlots;

    /*ckrueger audio*/
    //differentiates between two sounds: UI open(0) and UI close(1)
    private int uISoundNumber;
    //allows navigation sound to be played each time the player presses a button to navigate the menu
    private GameObject SelectHistory;

    //public int itemsPickedUp;
    public GameObject[] seedSlot;
    public GameObject[] plantSlot;
    private EventSystem eventSystem;
    private GameObject esLastSelected;

    public GameObject seedSlotHolder;
    public GameObject plantSlotHolder;
    public bool seedTab = true; // True = seed tab displaying, false = plant tab displaying
    public Text seedTextBox;
    public Text plantTextBox;

    //Check Controller Connected
    string[] connectedControllers;

    public float nextDig = 0;

    private void Start()
    {
        /*ckrueger audio*/
        uISoundNumber = 0;


        // Find how many slots the UI has for the inventory
        SetUpSeedAndPlantSlots();

        eventSystem = FindObjectOfType<EventSystem>();
        SelectHistory = eventSystem.currentSelectedGameObject;

        connectedControllers = Input.GetJoystickNames();

        if (toggleStartS == null)
        {
            toggleStartS = seedSlotHolder.GetComponentInChildren<Toggle>().gameObject;
        }
        if (toggleStartP == null)
        {
            toggleStartP = plantSlotHolder.GetComponentInChildren<Toggle>().gameObject;
        }

        ToggleDisplayInventory();
    }

    public void SetUpSeedAndPlantSlots()
    {
        totalSeedSlots = seedSlotHolder.GetComponentsInChildren<Slot>().Length;
        seedSlot = new GameObject[totalSeedSlots];
        for (int i = 0; i < totalSeedSlots; i++)
        {
            seedSlot[i] = seedSlotHolder.transform.GetChild(i).gameObject;

            if (seedSlot[i].GetComponent<Slot>().item == null)
            {
                seedSlot[i].GetComponent<Slot>().empty = true;
            }
        }

        totalPlantSlots = seedSlotHolder.GetComponentsInChildren<Slot>().Length;
        plantSlot = new GameObject[totalPlantSlots];
        for (int i = 0; i < totalPlantSlots; i++)
        {
            plantSlot[i] = plantSlotHolder.transform.GetChild(i).gameObject;

            if (plantSlot[i].GetComponent<Slot>().item == null)
            {
                plantSlot[i].GetComponent<Slot>().empty = true;
            }
        }
    }

    void Update()
    {

        /*v ckrueger audio v*/
        if (eventSystem.currentSelectedGameObject != SelectHistory && inventoryDisplaying && Time.time >= 0.1f)
        {
            PlaySoundUINavigate();
        }
        SelectHistory = eventSystem.currentSelectedGameObject;
        /*^ ckrueger audio ^*/

        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Open Inventory"))
        {
            /*v ckrueger audio v*/
            if (uISoundNumber == 0)
            {
                PlaySoundUIOpen();
            }
            else
            {
                PlaySoundUIClose();
            }
            /*^ ckrueger audio ^*/

            ToggleDisplayInventory();

            if (inventoryDisplaying)
            {
                /*ckrueger audio*/
                uISoundNumber = 1;

                Cursor.lockState = CursorLockMode.None;

            }
            else if (!inventoryDisplaying)
            {
                /*ckrueger audio*/
                uISoundNumber = 0;

                Cursor.lockState = CursorLockMode.Locked;

                //if (connectedControllers.Length > 0)
                //{
                //    DetermineIfUsingController();
                //}
                //else
                //{
                //    if (Input.GetKeyDown(KeyCode.E))
                //    {
                //        ToggleDisplayInventory();

                //        if (inventoryDisplaying)
                //            Cursor.lockState = CursorLockMode.None;
                //        else if (!inventoryDisplaying)
                //            Cursor.lockState = CursorLockMode.Locked;

                //    }
                //}


                if (inventoryDisplaying)
                {
                    UpdateDescriptionBox(true); //update during inventory displaying
                    if (Input.GetButtonDown("Right Bumper") || Input.GetButtonDown("Left Bumper"))
                    {
                        seedTab = !seedTab;
                        ToggleTabDisplayed();
                    }
                }
                //print(eventSystem.currentSelectedGameObject);
            }
        }
        if (inventoryDisplaying)
        {
            UpdateDescriptionBox(true); //update during inventory displaying
            if (Input.GetButtonDown("Right Bumper") || Input.GetButtonDown("Left Bumper"))
            {
                seedTab = !seedTab;
                ToggleTabDisplayed();
            }
        }
    }

    private void DetermineIfUsingController()
    {
        for (int i = 0; i < connectedControllers.Length; i++)
        {
            if (!string.IsNullOrEmpty(connectedControllers[i]))
            {
                if (Input.GetButtonDown("Open Inventory"))
                {
                    ToggleDisplayInventory();
                }
            }

        }
    }

    private void UpdateDescriptionBox(bool force = false)
    {
        if (eventSystem.currentSelectedGameObject != esLastSelected || force)
        {
            if (seedTab)
            {
                seedTextBox.text = eventSystem.currentSelectedGameObject.GetComponent<Slot>().item.GetComponent<Item>().itemDescriptionBoxContent()
                    ?? "Slot is Empty";
            }
            else
            {
                plantTextBox.text = eventSystem.currentSelectedGameObject.GetComponent<Slot>().item.GetComponent<Item>().itemDescriptionBoxContent()
                    ?? "Slot is Empty";
            }
        }
        esLastSelected = eventSystem.currentSelectedGameObject;
    }

    public void ToggleDisplayInventory()
    {
        UpdateDescriptionBox(); // Update before displaying inventory
                                //eventSystem.SetSelectedGameObject(toggleStart);
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

            //toggleStartS = seedSlotHolder.GetComponentInChildren<Toggle>().gameObject;
            // making sure that all of the slots are not "chosen"
            for (int i = 0; i < seedSlot.Length; i++)
            {
                seedSlot[i].GetComponent<Toggle>().isOn = false;
                if (!seedSlot[i].GetComponent<Slot>().empty)
                    seedSlot[i].GetComponent<Toggle>().interactable = true;
                plantSlot[i].GetComponent<Toggle>().interactable = false;
            }
            eventSystem.SetSelectedGameObject(toggleStartS);
            eventSystem.UpdateModules();
            //Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            seedInventoryCanvas.GetComponent<Canvas>().targetDisplay = 7;
            plantInventoryCanvas.GetComponent<Canvas>().targetDisplay = 0;

            //toggleStartP = plantSlotHolder.GetComponentInChildren<Toggle>().gameObject;
            // making sure that all of the slots are not "chosen"
            for (int i = 0; i < plantSlot.Length; i++)
            {
                plantSlot[i].GetComponent<Toggle>().isOn = false;
                if (!plantSlot[i].GetComponent<Slot>().empty)
                    plantSlot[i].GetComponent<Toggle>().interactable = true;
                seedSlot[i].GetComponent<Toggle>().interactable = false;
            }
            eventSystem.SetSelectedGameObject(toggleStartP); // Event system keeps going back to seedSlot as selected.
            eventSystem.UpdateModules();
            //Cursor.lockState = CursorLockMode.None;
        }
        UpdateDescriptionBox(); // Update after switching inventory tabs
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
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

            AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon, item.subType, item.stackNumber, new GameObject[6]);
        }
    }

    // CLEAN UP REQUIRED - PT
    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon, string subType, int stackNumber, GameObject[] temp)
    {
        if (itemObject.tag == "Seed")
        {
            temp = seedSlot;
        }
        else if (itemObject.tag == "Plant")
        {
            temp = plantSlot;
        }

        for (int i = 0; i < temp.Length; i++)
        {
            // Already has this kind of item, stack instead of add new.
            if (!temp[i].GetComponent<Slot>().empty && temp[i].GetComponent<Slot>().item.GetComponent<Item>().ID == itemID)
            {
                temp[i].GetComponent<Slot>().stackNumber++;
                temp[i].GetComponent<Slot>().UpdateStackNumber();
                temp[i].GetComponent<Slot>().item.GetComponent<Item>().stackNumber++;
                itemObject.transform.position = this.gameObject.GetComponent<PlayerController>().planet.transform.position; //new Vector3(0, 0, 0);
                return;
            }
            else if (temp[i].GetComponent<Slot>().empty)
            {
                itemObject.GetComponent<Item>().pickedUp = true;

                temp[i].GetComponent<Slot>().item = itemObject;
                temp[i].GetComponent<Slot>().icon = itemIcon;
                temp[i].GetComponent<Slot>().type = itemType;
                temp[i].GetComponent<Slot>().ID = itemID;
                temp[i].GetComponent<Slot>().description = itemDescription;
                temp[i].GetComponent<Slot>().subType = subType;
                temp[i].GetComponent<Slot>().stackNumber = 1;

                itemObject.transform.position = this.gameObject.GetComponent<PlayerController>().planet.transform.position; //new Vector3(0, 0, 0);

                temp[i].GetComponent<Slot>().UpdateSlot();
                return;
            }
        }

        if (itemObject.tag == "Seed")
            seedSlot = temp;
        else if (itemObject.tag == "Plant")
            plantSlot = temp;

    }

    /*ckrueger audio*/
    private void PlaySoundUIOpen()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_ui_open", gameObject);
    }

    private void PlaySoundUIClose()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_ui_close", gameObject);
    }

    private void PlaySoundUINavigate()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_ui_navigate", gameObject);
    }

    private void PlaySoundUISelect()
    {
        AkSoundEngine.PostEvent("Play_ts_sx_uni_ui_select", gameObject);
    }
}
