using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleManager : MonoBehaviour
{
    InputActionProperty triggerRight;
    InputActionProperty triggerLeft;
    UnityEngine.XR.Interaction.Toolkit.XRRayInteractor rightHand;
    UnityEngine.XR.Interaction.Toolkit.XRRayInteractor leftHand;

    // Update is called once per frame

    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        foreach (var device in inputDevices)
        {
            bool triggerValue;
            if(device.characteristics == UnityEngine.XR.InputDeviceCharacteristics.Right)
            {
                if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    RightLaunch();
                }
            }

            if (device.characteristics == UnityEngine.XR.InputDeviceCharacteristics.Left)
            {
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    LeftLaunch();
                }
            }
        }
    }
    public void RightLaunch()
    {
        
        if(rightHand.interactablesSelected[0].transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb.GetComponent<XRGrabInteractable>());
            rb.AddForce(rb.transform.position - rightHand.transform.position, ForceMode.VelocityChange);
        }
        else
        {

        }
    }

    public void LeftLaunch()
    {

        if (leftHand.interactablesSelected[0].transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb.GetComponent<XRGrabInteractable>());
            rb.AddForce(rb.transform.position - leftHand.transform.position, ForceMode.VelocityChange);
        }
        else
        {

        }
    }
}
