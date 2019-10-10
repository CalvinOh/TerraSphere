using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuStart : MonoBehaviour
{

    private bool menuOnce = false;
    private bool animOnce = false;

    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private Text title;

    [SerializeField]
    [Tooltip("Time needed for animation to end.")]
    private float timeNeededAnim;

    [SerializeField]
    [Tooltip("Color crossfade time")]
    private float crossfadeTime;

    [SerializeField]
    [Tooltip("Time for menu to appear.")]
    private float timeNeededMenu;

    private EventSystem eventSystem;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Use Item"))
        {
            timeNeededAnim = 0;
            timeNeededMenu = 0;
            crossfadeTime = 0;
        }
        if (timeNeededAnim <= Time.timeSinceLevelLoad && !animOnce)
        {
            title.CrossFadeAlpha(255.0f, crossfadeTime, true);
        }
        if (timeNeededMenu <= Time.timeSinceLevelLoad && !menuOnce)
        {
            mainMenuPanel.SetActive(true);
            eventSystem.SetSelectedGameObject(null);
            menuOnce = true;
        }
        
    }
}
