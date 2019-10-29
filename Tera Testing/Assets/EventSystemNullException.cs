using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemNullException : MonoBehaviour
{
    private EventSystem eventSystem;

    [SerializeField]
    [Tooltip("Gameobject you'd like to have selected as default.")]
    private GameObject defaultSelected;

    private void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    void Update()
    {
        if(eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(defaultSelected);
        }
    }
}
