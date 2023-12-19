using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationSkin : MonoBehaviour
{
    [SerializeField]
    Mesh high, low;

    [SerializeField]
    float distance = 15;

    bool isHigh = true;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude < distance && !isHigh)
        {
            GetComponent<MeshFilter>().mesh = high;
            isHigh = true;
        }
        else if(transform.position.magnitude > distance && isHigh)
        {
            GetComponent<MeshFilter>().mesh = low;
            isHigh = false;
        }
    }
}
