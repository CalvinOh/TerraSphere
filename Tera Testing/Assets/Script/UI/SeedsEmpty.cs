using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsEmpty : MonoBehaviour
{
    private PlayerController player;

    [SerializeField]
    private GameObject Panel;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.seedItem.GetComponent<Item>().stackNumber == 0)
        {
            Panel.SetActive(true);

        }
        else
        {
            Panel.SetActive(false);
        }
    }
}
