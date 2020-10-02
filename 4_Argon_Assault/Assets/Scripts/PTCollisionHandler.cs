﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok as long as this is the only script that loads scenes

public class PTCollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Untagged":
                DeathEvent();
                break;
            case "GateBlue":
                ChargeUp();
                break;
            case "GateAmber":
                AmberDamage();
                break;
            case "GateRed":
                ChargeDown();
                break;
            default:
                break;

        }
       
    }

    private void DeathEvent()
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene() // string referenced
    {
        SceneManager.LoadScene(1);
    }

    private void ChargeUp()
    {
        print("chargeup");
    }

    private void AmberDamage()
    {
        print("amberdamage");
    }

    private void ChargeDown()
    {
        print("chargedown");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
