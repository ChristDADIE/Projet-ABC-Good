using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectBubble : MonoBehaviour
{

    public GameObject camera;
    public GameObject canvas;


    void Update()
    {
        canvas.transform.rotation = Quaternion.LookRotation(canvas.transform.position - camera.transform.position);
    }
}
