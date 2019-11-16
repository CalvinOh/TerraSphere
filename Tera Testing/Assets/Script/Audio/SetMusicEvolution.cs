using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicEvolution : MonoBehaviour
{
    TerraBar t;

    // Use this for initialization.
    void Start()
    {
        AkSoundEngine.SetState("TerraformProgress", "Desolate");

        t = (TerraBar)FindObjectOfType(typeof(TerraBar));
    }

    // Update is called once per frame.
    void Update()
    {
        UpdateTerraformState();
    }

    void UpdateTerraformState()
    {
        //Debug.Log("MusicStateSwitcher Running");

        if (t.barPercentage <= 5)
        {
            Debug.Log("music desolate");
            AkSoundEngine.SetState("TerraformProgress", "Desolate");
        }
        else if ((t.barPercentage > 5) && (t.barPercentage < 10))
        {
            Debug.Log("music hopeful");
            AkSoundEngine.SetState("TerraformProgress", "Hopeful");
        }
        else if (t.barPercentage >= 10)
        {
            Debug.Log("music cheerful");
            AkSoundEngine.SetState("TerraformProgress", "Cheerful");
        }
    }
}
