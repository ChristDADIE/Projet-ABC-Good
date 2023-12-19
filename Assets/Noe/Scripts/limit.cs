using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limit : MonoBehaviour
{

    void Update()
    {
        float life = MainManager.main.GetComponent<LevelManager>().life/100;
        life = Mathf.Clamp(life, 0, 1);
        GetComponent<MeshRenderer>().material.color = new Color(1 - life, life, 0,0.5f);
    }
}
