using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuExperimenter : MonoBehaviour
{

    public PauseManager pauseManager;

    public GameObject Pyhisque;
    public GameObject DesertObject;
    public GameObject ForestObject;
    public GameObject EnvPlane;
    public GameObject Environement;
    public GameObject panelPause;
    public GameObject panelExperimenter;


    public void retour()
    {

        panelPause.SetActive(true);
        Environement.SetActive(false);
        panelExperimenter.SetActive(false);
        pauseManager.ToggleTimeScale(true);
    }

    public void physique()
    {

        Pyhisque.SetActive(true);
        EnvPlane.SetActive(true);

        panelPause.SetActive(false);
        Environement.SetActive(false);
        panelExperimenter.SetActive(false);
        DesertObject.SetActive(false);
        
        ForestObject.SetActive(false);

        pauseManager.ToggleTimeScale(false);
        pauseManager.ToggleMenuAndEnvironment(false);


    }

    //public void chimie()
    //{

    //    Pyhisque.SetActive(true);
    //    EnvPlane.SetActive(true);

    //    panelPause.SetActive(false);
    //    Environement.SetActive(false);
    //    panelExperimenter.SetActive(false);
    //    DesertObject.SetActive(false);

    //    ForestObject.SetActive(false);

    //    pauseManager.ToggleTimeScale(false);
    //    pauseManager.ToggleMenuAndEnvironment(false);


    //}
}
