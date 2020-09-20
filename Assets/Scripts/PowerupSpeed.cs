using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerupSpeed : MonoBehaviour
{

    [Header("Powerup Settings")]
    [SerializeField] float _speedIncreaseAmount = 20;
    [SerializeField] float _powerupDuration = 5;
    [SerializeField] AudioClip _PSound = null;
    [SerializeField] AudioClip _RSound = null;
    [SerializeField] ParticleSystem _ImpactP = null;

    [Header("Setup")]
    [SerializeField] GameObject _visualsToDeactivate = null;

    Collider _colliderToDeactivate = null;
    bool _poweredUp = false;

    private void Awake()
    {
        _colliderToDeactivate = GetComponent<Collider>();

        EnableObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerShip playerShip
            = other.gameObject.GetComponent<PlayerShip>();
            //if we have a valid player that isn't powered up
        if (playerShip != null && _poweredUp == false)
        {
            //start power up timer; Restart if it's already started
            StartCoroutine(PowerupSequence(playerShip));

            //play audio
            AudioHelper.PlayClip2D(_PSound, 1);

            //use visuals
            Collect();
        }
    }

    IEnumerator PowerupSequence(PlayerShip playerShip)
    {
        //set boolean for detecting lockout
        _poweredUp = true;

        ActivatePowerup(playerShip);
        //Simulate this object being disabled
        //Don't really want it disabled
        //We still need the script to function
        DisableObject();

        //wait for required duration
        yield return new WaitForSeconds(_powerupDuration);
        //reset
        DeactivatePowerup(playerShip);
        EnableObject();
        AudioHelper.PlayClip2D(_RSound, 1);

        //set boolean to release lockout
        _poweredUp = false;
    }

    void ActivatePowerup(PlayerShip playerShip)
    {
        if (playerShip != null)
        {
            //powerup player
            playerShip.SetSpeed(_speedIncreaseAmount);
            //visuals
            playerShip.SetBoosters(true);
        }
    }

    void DeactivatePowerup(PlayerShip playerShip)
    {
        //revert player powerup - will subtract
        playerShip?.SetSpeed(-_speedIncreaseAmount);
        //visuals
        playerShip?.SetBoosters(false);
    }

    public void DisableObject()
    {
        //disable collider so it can't be retriggered
        _colliderToDeactivate.enabled = false;
        //disable visuals to simulate
        _visualsToDeactivate.SetActive(false); 
        //TODO reactivate particle flash/audio
    }

    public void EnableObject()
    {
        //enable collider so it can be retriggered
        _colliderToDeactivate.enabled = true;
        //enable visuals again to draw attention
        _visualsToDeactivate.SetActive(true);
        //TODO reactivate particle flash/audio
    }

    public void Collect()
    {
        //play the collect graphics
        _ImpactP.Play();
    }

    void Update()
    {

    }
}
