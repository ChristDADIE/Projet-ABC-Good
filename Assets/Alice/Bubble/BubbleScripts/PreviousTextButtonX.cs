using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//[RequireComponent(typeof(DisplayLongTextInBubble))]
public class PreviousTextButtonX : MonoBehaviour
{
    public InputAction previousText;
    //public InputActionProperty previousText;
    //
    private DisplayLongTextInBubble singletonInstance;


    void OnEnable() 
    {
        previousText.Enable();
    }

    void OnDisable() 
    {
        previousText.Disable();
    }

    void Start()
    {
        singletonInstance = DisplayLongTextInBubble.Instance;

        previousText.performed += OnButtonXPressed;

        // //previousText.Enable();
        // previousText.action.performed += cxt => 
        // {
        //     //GetComponent<DisplayLongTextInBubble>().PreviousText();
        //     //
        //     singletonInstance.PreviousText();
        // };
    }

    void OnButtonXPressed(InputAction.CallbackContext context) 
    {
        singletonInstance.PreviousText();
    }

}