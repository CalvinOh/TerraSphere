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
    private GameObject SelectHistory;
    private bool firstSelectOverride = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        eventSystem = FindObjectOfType<EventSystem>();
        SelectHistory = eventSystem.currentSelectedGameObject;
        AkSoundEngine.PostEvent("Play_MainTheme", this.gameObject);
    }

    void Update()
    {
        if(eventSystem.currentSelectedGameObject != SelectHistory && Time.time >= 0.1f)
        {
            AkSoundEngine.PostEvent("Play_ts_sx_uni_ui_navigate", this.gameObject);
        }
        SelectHistory = eventSystem.currentSelectedGameObject;
        firstSelectOverride = true;
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

    public void StopMusic()
    {
        AkSoundEngine.StopAll(this.gameObject);
    }
}
