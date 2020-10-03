using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PT2PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xcontrolSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xRangeMin = -12f;
    [Tooltip("In ms^-1")] [SerializeField] float xRangeMax = 12f;

    [Tooltip("In ms^-1")] [SerializeField] float ycontrolSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float yRangeMin = -12f;
    [Tooltip("In ms^-1")] [SerializeField] float yRangeMaxRaw = 12f;

    [Tooltip("In ms^-1")] [SerializeField] float zRangeMin = -4;
    [Tooltip("In ms^-1")] [SerializeField] float zRangeMax = 4f;

    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float postionPitchFactor = 2f;
    [SerializeField] float positionYawFactor = -1f;
    [SerializeField] float zFactor = 1;
    [SerializeField] float zYAdjuster = 1;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Update is called once per frame

    void Update()
    {
        if (isControlEnabled)
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
        xThrow = CrossPlatformInputManager.GetAxis("P2_horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("P2_vertical");

        float xOffset = xThrow * xcontrolSpeed * Time.deltaTime;
        float yOffset = yThrow * ycontrolSpeed * Time.deltaTime;
        float zOffset = xOffset * zFactor;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;
        float rawZPos = transform.localPosition.z + zOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, xRangeMin, xRangeMax);

        float zYRangeMaxAdjust = clampedXPos * zYAdjuster;
        float yRangeMax = zYRangeMaxAdjust + yRangeMaxRaw;

        float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);
        float clampedZPos = Mathf.Clamp(rawZPos, zRangeMin, zRangeMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, clampedZPos);
    }
    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("P2_fire"))
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
