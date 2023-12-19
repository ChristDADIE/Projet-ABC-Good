using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnvironemnents : MonoBehaviour
{

    public PauseManager pauseManager;

    

    public GameObject DesertObject;
    public GameObject ForestObject;
    public GameObject EnvPlane;
    public GameObject Environement;
    public GameObject panelPause;
    public GameObject panelExperimenter;


    public void foret()
    {

        panelPause.SetActive(false);
        Environement.SetActive(false);
        panelExperimenter.SetActive(false);
        DesertObject.SetActive(false);
        EnvPlane.SetActive(false);
        ForestObject.SetActive(true);
        
        pauseManager.ToggleTimeScale(false);
        pauseManager.ToggleMenuAndEnvironment(false);

        // penser à disable le bouton pause

    }

    public void desert()
    {

        panelPause.SetActive(false);
        Environement.SetActive(false);
        panelExperimenter.SetActive(false);
        DesertObject.SetActive(true);
        EnvPlane.SetActive(false);
        ForestObject.SetActive(false);
        pauseManager.ToggleTimeScale(false);
        pauseManager.ToggleMenuAndEnvironment(false);

        // penser à disable le bouton pause
    }

    public void retour()
    {

        panelPause.SetActive(true);
        Environement.SetActive(false);
        panelExperimenter.SetActive(false);
        pauseManager.ToggleTimeScale(true);
    }

    public void options()
    {

        //SceneManager.LoadScene("Scene quitter");



    }

    public void quitter()
    {

        //SceneManager.LoadScene("Scene quitter");



    }
}
