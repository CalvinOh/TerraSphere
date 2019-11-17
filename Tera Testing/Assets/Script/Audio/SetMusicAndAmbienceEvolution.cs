using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicAndAmbienceEvolution : MonoBehaviour
{
    /*ckrueger audio*/

    public AK.Wwise.RTPC TerraformRTPC;

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
        TerraformRTPC.SetGlobalValue(t.barPercentage);

        //Debug.Log("MusicStateSwitcher Running");

        if ((t.barPercentage < .35f) && (t.barPercentage > .0f))
        {
            AkSoundEngine.SetState("TerraformProgress", "Desolate");
        }
        else if ((t.barPercentage > .36f) && (t.barPercentage < .75f))
        {
            AkSoundEngine.SetState("TerraformProgress", "Hopeful");
        }
        else if (t.barPercentage >= .76f)
        {
            AkSoundEngine.SetState("TerraformProgress", "Cheerful"); 
        }
    }
}
