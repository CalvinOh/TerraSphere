using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toonShaderScript : MonoBehaviour
{
    //Victoria wrote this. I have no idea how to code, if I made a mistake please let me know
    private Light light = null;

    private void OnEnable()
    {
        light = this.GetComponent<Light>();
    }
    private void Update()
    {
        Shader.SetGlobalVector("_ToonLightDirection",-this.transform.forward):
    }
}
