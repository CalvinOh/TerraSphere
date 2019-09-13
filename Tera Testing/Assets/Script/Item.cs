using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    public string type;
    public string description;
    public Sprite icon;
    public bool pickedUp;

    [HideInInspector]
    public bool equipped; // Tool currently equipped

    [HideInInspector]
    public GameObject tool; // this is the tool inside the player's tool manager.

    [HideInInspector]
    public GameObject toolManager; // this is the tool inside the player's tool manager.

    public bool playersTool; // is it in the player's tool manager?

    public void Start()
    {
        toolManager = GameObject.FindWithTag("ToolManager");
        if(!playersTool)
        {
            int alltools = toolManager.transform.childCount;
            for(int i = 0; i < alltools; i++)
            {
                if(toolManager.transform.GetChild(i).gameObject.GetComponent<Item>().ID == ID)
                {
                    tool = toolManager.transform.GetChild(i).gameObject;
                }
            }
        }
    }


    public void Update()
    {
        if(equipped)
        {
            // do tool stuff here

            if(Input.GetKeyDown(KeyCode.G))
            {
                equipped = false;
            }

            if (!equipped)
                this.gameObject.SetActive(false);
        }
    }

    public void itemUsage()
    {
        if (type == "Tool")
        {
            tool.SetActive(true);
            tool.GetComponent<Item>().equipped = true;
        }


    }
}
