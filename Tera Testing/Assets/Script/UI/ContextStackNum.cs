using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextStackNum : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currentlySelecting.tag == "Hole")
        {
            text.text = player.seedItem.GetComponent<Item>().stackNumber.ToString();
        }
        else
        {
            text.text = " ";
        }
    }
}
