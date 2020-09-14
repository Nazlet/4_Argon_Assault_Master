using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 12f;

    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 20f;
    [Tooltip("In ms^-1")] [SerializeField] float yRangeMin = -12f;
    [Tooltip("In ms^-1")] [SerializeField] float yRangeMax = 12f;

    [SerializeField] float postionPitchFactor = 2f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = -1f;
    [SerializeField] float controlRollFactor = -25f;

    float xThrow, yThrow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        print("Player Player triggered something");
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

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

        float xOffset = xThrow * xSpeed * Time.deltaTime;
        float yOffset = yThrow * ySpeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, +xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, yRangeMin, yRangeMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
