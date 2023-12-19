using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandleManager : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.XRRayInteractor rightHand;
    public UnityEngine.XR.Interaction.Toolkit.XRRayInteractor leftHand;

    // Update is called once per frame

    public InputAction bouton;


    private void OnEnable()
    {
        bouton.Enable();
    }

    private void OnDisable()
    {
        bouton.Disable();
    }

    private void Start()
    {
        bouton.performed += OnButtonAPressed;

    }

    private void OnButtonAPressed(InputAction.CallbackContext context)
    {
        
        RightLaunch();
        LeftLaunch();

    }


    public void RightLaunch()
    {
        if(rightHand.interactablesSelected.Count > 0)
        {
            if (rightHand.interactablesSelected[0].transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Destroy(rb.GetComponent<XRGrabInteractable>());
                rb.AddForce(rightHand.transform.forward*20, ForceMode.VelocityChange);
            }
        }

    }

    public void LeftLaunch()
    {
        if (leftHand.interactablesSelected.Count > 0)
        {
            if (leftHand.interactablesSelected[0].transform.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Destroy(rb.GetComponent<XRGrabInteractable>());
                rb.AddForce(rightHand.transform.forward * 20, ForceMode.VelocityChange);
            }
            else
            {

            }
        }
    }
}
