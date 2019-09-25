using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarKeyToggle : MonoBehaviour
{
    [SerializeField]
    ToggleGroup hotBar;
    [SerializeField]
    Toggle shovel;
    [SerializeField]
    Toggle seed;
    [SerializeField]
    Toggle rake;
    [SerializeField]
    Toggle watercan;

    [SerializeField]
    Inventory inv;

    private void Start()
    {
        if(inv == null)
        {
            inv = FindObjectOfType<Inventory>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            shovel.SetIsOnWithoutNotify(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            seed.SetIsOnWithoutNotify(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            rake.SetIsOnWithoutNotify(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            watercan.SetIsOnWithoutNotify(true);
        }

    }
}
