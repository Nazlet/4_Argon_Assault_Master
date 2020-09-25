using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xcontrolSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 12f;

    [Tooltip("In ms^-1")] [SerializeField] float ycontrolSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float yRangeMin = -12f;
    [Tooltip("In ms^-1")] [SerializeField] float yRangeMax = 12f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float postionPitchFactor = 2f;
    [SerializeField] float positionYawFactor = -1f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame

    void Update()
    {
        if(isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void OnPlayerDeath() // called by string reference
    {
        isControlEnabled = false;

    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * postionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;


        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * xcontrolSpeed * Time.deltaTime;
        float yOffset = yThrow * ycontrolSpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, +xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
