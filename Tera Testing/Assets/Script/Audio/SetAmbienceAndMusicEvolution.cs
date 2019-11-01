using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAmbienceAndMusicEvolution : MonoBehaviour
{
    /*ckrueger audio*/

    public AK.Wwise.RTPC TerraformRTPC;
    public AK.Wwise.State TerraformState;

    TerraBar t;

    // Use this for initialization.
    void Start()
    {
        t = (TerraBar)FindObjectOfType(typeof(TerraBar));
    }

    // Update is called once per frame.
    void Update()
    {
        TerraformRTPC.SetGlobalValue(t.barPercentage);

        if (t.barPercentage < 35)
        {
            //TerraformState.SetValue;
        }
        
    }
}
