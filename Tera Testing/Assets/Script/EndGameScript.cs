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

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetButtonDown("Use Item") && terraBar.barPercentage >= 1)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
