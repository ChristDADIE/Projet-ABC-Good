using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GloveBox : MonoBehaviour
{

    public InputAction putOnGloves;

    [SerializeField] GameObject handLhand;
    [SerializeField] GameObject handRhand;

    Color gloveColor;
    Color normalColor;

    bool glovesOn;


    void OnEnable() 
    {
        putOnGloves.Enable();
    }

    void OnDisable() 
    {
        putOnGloves.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        glovesOn = false; 

        gloveColor = new Color(0.173f, 0.651f, 0.91f, 1.0f);
        normalColor = new Color(0.878f, 0.608f, 0.067f, 1.0f);

        putOnGloves.performed += OnGrab;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnGrab(InputAction.CallbackContext context) 
    {
        if (glovesOn)
        {
            handLhand.GetComponent<Renderer>().material.SetColor("_Color", normalColor);
            handRhand.GetComponent<Renderer>().material.SetColor("_Color", normalColor);
        }
        
        else 
        {
            handLhand.GetComponent<Renderer>().material.SetColor("_Color", gloveColor);
            handRhand.GetComponent<Renderer>().material.SetColor("_Color", gloveColor);
        }
    }
}
