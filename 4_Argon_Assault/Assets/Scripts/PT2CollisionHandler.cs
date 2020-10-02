using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok as long as this is the only script that loads scenes - but its not now

public class PT2CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab on player")] [SerializeField] GameObject deathFX;

    [SerializeField] int chargeUpAmount2 = 20;
    [SerializeField] int amberDamageAmount2 = -10;
    [SerializeField] int chargeDownAmount2 = -20;

    PT2ScoreBoard pt2scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        pt2scoreBoard = FindObjectOfType<PT2ScoreBoard>();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Untagged":
                DeathEvent();
                break;
            case "GateBlue":
                ChargeDown();
                break;
            case "GateAmber":
                AmberDamage();
                break;
            case "GateRed":
                ChargeUp();
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
        pt2scoreBoard.ScoreHit2(chargeUpAmount2);
    }

    private void AmberDamage()
    {
        pt2scoreBoard.ScoreHit2(amberDamageAmount2);
    }

    private void ChargeDown()
    {
        pt2scoreBoard.ScoreHit2(chargeDownAmount2);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
