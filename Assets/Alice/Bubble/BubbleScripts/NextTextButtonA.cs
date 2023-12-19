using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//[RequireComponent(typeof(DisplayLongTextInBubble))]
public class NextTextButtonA : MonoBehaviour
{
    //public InputActionProperty nextText;
    public InputAction nextText;
    //
    private DisplayLongTextInBubble singletonInstance;

    void OnEnable() 
    {
        nextText.Enable();
    }

    void OnDisable() 
    {
        nextText.Disable();
    }

    void Start()
    {
        singletonInstance = DisplayLongTextInBubble.Instance;

        nextText.performed += OnButtonAPressed;
        
        // nextText.Enable();
        // nextText.action.performed += cxt => 
        // {
        //     //GetComponent<DisplayLongTextInBubble>().NextText();
        //     //
        //     //singletonInstance.NextText();
        // };
    }

    void OnButtonAPressed(InputAction.CallbackContext context) 
    {
        singletonInstance.NextText();
    }

}


