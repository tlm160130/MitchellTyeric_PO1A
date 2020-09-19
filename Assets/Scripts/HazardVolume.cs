using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardVolume : MonoBehaviour
{
    public int Respawn;
    
    private void OnTriggerEnter(Collider other)
    {
        //Detect if it's the player
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        // if we found something valid, continue
        if (playerShip != null)
        {
            // do something!
            SceneManager.LoadScene(Respawn);
        }
    }
}
