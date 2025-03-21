using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RecorderHandle : XRBaseInteractable
{
    [SerializeField] private Vector3 activatedRotation;
    [SerializeField] private RotatingRectacle rotatingPlate;
    [SerializeField] private AudioSource audioSource;

    private Vector3 idleRotation;
    private Vector3 targetRotation;

    private Vector3 currentRotation;
    private bool isActivated = false;
    private bool isRotating = false;


    void Start()
    {
        idleRotation = transform.localEulerAngles;

        selectEntered.AddListener(OnActivated);
    }

    private void OnActivated(SelectEnterEventArgs arg0)
    {
        if (isRotating) return;

        targetRotation = isActivated ? idleRotation : activatedRotation;
        isActivated = !isActivated;
        isRotating = true;

        if (!isActivated)
        {
            audioSource.Stop();
        }

        rotatingPlate.SetIsRotating(isActivated);
    }

    void Update()
    {
        if (isRotating)
        {
            currentRotation = Vector3.Lerp(currentRotation, targetRotation, 0.1f);
            transform.localEulerAngles = currentRotation;

            if (Vector3.Distance(currentRotation, targetRotation) < 0.01f)
            {
                isRotating = false;
                if (isActivated)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
