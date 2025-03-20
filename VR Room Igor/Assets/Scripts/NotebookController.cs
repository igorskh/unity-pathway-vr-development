using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class NotebookController : MonoBehaviour
{

    [SerializeField] private HingeJoint hinge;
    private XRGrabInteractable grabInteractable;

    private void OnEnable()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private JointMotor GetMotor(float targetVelocity, float force = 200)
    {
        var motor = hinge.motor;
        motor.force = force;
        motor.targetVelocity = targetVelocity;
        motor.freeSpin = false;

        return motor;
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        hinge.motor = GetMotor(90, 1500);
        hinge.useMotor = true;
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        hinge.useMotor = false;

        Quaternion closedRotation = hinge.gameObject.transform.localRotation;
        closedRotation.z = 0;

        hinge.gameObject.transform.localRotation = closedRotation;
    }
}
