using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundEvents : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        AkSoundEngine.RegisterGameObj(gameObject);
    }

    void LeftFootTouchGroundEvent()
    {
        AkSoundEngine.PostEvent("play_ts_sx_uni_plr_footsteps", gameObject);
    }

    void RightFootTouchGroundEvent()
    {
        AkSoundEngine.PostEvent("play_ts_sx_uni_plr_footsteps", gameObject);
    }
}
