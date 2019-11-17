using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Terrabar so the ship has knowledge of completed planet.")]
    private TerraBar terraBar;

    [SerializeField]
    [Tooltip("The box collider that will detect player's proximity.")]
    private Collider collider;

    [SerializeField]
    [Tooltip("The scene index number of the next desired scene.")]
    private int nextScene;

    [SerializeField]
    private GameObject ReadyPanel;

    [SerializeField]
    private GameObject NotReadyPanel;

    private void OnTriggerStay(Collider other)
    {
        if(terraBar.barPercentage >= 1)
        {
            ReadyPanel.SetActive(true);
        }
        else if(terraBar.barPercentage < 1)
        {
            NotReadyPanel.SetActive(true);
        }



        if(other.tag == "Player" && Input.GetButtonDown("Use Item") && terraBar.barPercentage >= 1)
        {
            AkSoundEngine.StopAll();
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerExit(Collider other)
    {
       ReadyPanel.SetActive(false);
       NotReadyPanel.SetActive(false);
    }
}
