using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HazardVolume : MonoBehaviour
{
    public int Respawn;
    [SerializeField] AudioClip _DSound = null;
    public GameObject loseText;

    void Awake()
    {
        loseText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Detect if it's the player
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
        // if we found something valid, continue
        if (playerShip != null)
        {
            // do something!
            playerShip.Kill();
            AudioHelper.PlayClip2D(_DSound, 1);
            loseText.SetActive(true);
            DelayHelper.DelayAction(this, RespawnPlayer, 2f);
        }
    }

    void RespawnPlayer()
    {
        SceneManager.LoadScene(Respawn);
    }
}
