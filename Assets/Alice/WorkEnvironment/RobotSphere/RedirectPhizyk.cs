using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedirectPhizyk : MonoBehaviour
{

    public GameObject robot;


    void Update()
    {
        robot.transform.rotation = Quaternion.LookRotation(robot.transform.position - Camera.main.transform.position) ;
    }
}
