using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok as long as this is the only script that loads scenes

public class PTCollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    [SerializeField] int chargeUpAmount1 = 20;
    [SerializeField] int amberDamageAmount1 = -10;
    [SerializeField] int chargeDownAmount1 = -20;

    PT1ScoreBoard pt1scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        pt1scoreBoard = FindObjectOfType<PT1ScoreBoard>();
    }

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
        pt1scoreBoard.ScoreHit(chargeUpAmount1);
    }

    private void AmberDamage()
    {
        pt1scoreBoard.ScoreHit(amberDamageAmount1);
    }

    private void ChargeDown()
    {
        pt1scoreBoard.ScoreHit(chargeDownAmount1);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
