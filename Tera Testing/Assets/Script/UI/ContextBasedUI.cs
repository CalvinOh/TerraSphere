using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextBasedUI : MonoBehaviour
{
    [SerializeField]
    private Image emptyIcon;
    [SerializeField]
    private Image greenOutline;
    [SerializeField]
    private RectTransform popUpCanvas;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    public Sprite seedIcon;
    [SerializeField]
    private Sprite waterIcon;
    [SerializeField]
    private Sprite harvestIcon;

    [SerializeField]
    private Collider detector;

    private Collider detectorHistory;
    private int alreadyActivated = 0;
    private Transform newLocation;

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        CheckCollider();
        popUpCanvas.rotation = player.currentlySelecting.transform.rotation;

        popUpCanvas.position = player.currentlySelecting.transform.position;
    }

    private void CheckCollider()
    {
        if(detector != null)
        {
            if (detector.tag == "Plant")
            {
                emptyIcon.sprite = harvestIcon;
            }
            else if (detector.tag == "Seed")
            {
                emptyIcon.sprite = waterIcon;
            }
            else if (detector.tag == "Hole")
            {
                emptyIcon.sprite = seedIcon;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hole" || other.tag == "Seed" || other.tag == "Plant")
        {
            emptyIcon.gameObject.SetActive(true);
            greenOutline.gameObject.SetActive(true);
            detector = other;
            if (alreadyActivated <= 0)
            {
                print("PopUpAnimStart");
                animator.SetBool("AnimStart", true);
                
            }
            alreadyActivated++;
            if (other.tag == "Plant")
            {
                emptyIcon.sprite = harvestIcon;
            }
            else if (other.tag == "Seed")
            {
                emptyIcon.sprite = waterIcon;
            }
            else if (other.tag == "Hole")
            {
                emptyIcon.sprite = seedIcon;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Hole" || other.tag == "Seed" || other.tag == "Plant")
        {
            alreadyActivated--;
            print(alreadyActivated+" "+other.tag);
            if (alreadyActivated <= 0)
            {
                emptyIcon.gameObject.SetActive(false);
                greenOutline.gameObject.SetActive(false);
                emptyIcon.sprite = null;
                animator.SetBool("AnimStart", false);
            }  
        }
    }

    public void AfterHarvest()
    {
        alreadyActivated--;
    }
}
