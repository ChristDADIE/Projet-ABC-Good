using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEnvironemnents : MonoBehaviour
//{

//    public GameObject DesertObject;
//    public GameObject ForestObject;
//    public GameObject EnvPlane;
//    public GameObject Environement;
//    public GameObject panelPause;
//    public GameObject panelExperimenter;


//    // Start is called before the first frame update
//    void Start()
//    {
//        EnvPlane.SetActive(true);
//    }

//    public void foret() {

//        panelPause.SetActive(false);
//        Environement.SetActive(false);
//        panelExperimenter.SetActive(false);
//        DesertObject.SetActive(false);
//        EnvPlane.SetActive(false);
//        ForestObject.SetActive(true);

//        // penser à disable le bouton pause

//    }

//    public void desert() {

//        panelPause.SetActive(false);
//        Environement.SetActive(false);
//        panelExperimenter.SetActive(false);
//        DesertObject.SetActive(true);
//        EnvPlane.SetActive(false);
//        ForestObject.SetActive(false);

//        // penser à disable le bouton pause
//    }

//    public void retour() {

//        panelPause.SetActive(true);
//        Environement.SetActive(false);
//        panelExperimenter.SetActive(false);
//    }

//    public void options()
//    {

//        //SceneManager.LoadScene("Scene quitter");



//    }

//    public void quitter()
//    {

//        //SceneManager.LoadScene("Scene quitter");



//    }
//}



{
    public GameObject DesertObject;
    public GameObject ForestObject;
    public GameObject EnvPlane;
    public GameObject Environement;
    public GameObject panelPause;
    public GameObject panelExperimenter;

    public void foret()
    {
        GameManager.UnpauseGame(); // Reprend le temps
        ToggleMenu(false);
        ActivateEnvironmentObjects(false, true, false);
    }

    public void desert()
    {
        GameManager.UnpauseGame(); // Reprend le temps
        ToggleMenu(false);
        ActivateEnvironmentObjects(true, false, false);
    }

    public void retour()
    {
        GameManager.PauseGame(); // Pause le temps
        ToggleMenu(true);
        ActivateEnvironmentObjects(false, false, false);
    }

    public void options()
    {
        // Gestion des options
    }

    public void quitter()
    {
        // Gestion de la sortie
    }

    public void Unpause()
    {
        ToggleMenu(false);
        ActivateEnvironmentObjects(false, false, false);
    }

    private void ToggleMenu(bool active)
    {
        panelPause.SetActive(active);
        panelExperimenter.SetActive(!active);
    }

    private void ActivateEnvironmentObjects(bool desertActive, bool forestActive, bool envPlaneActive)
    {
        DesertObject.SetActive(desertActive);
        ForestObject.SetActive(forestActive);
        EnvPlane.SetActive(envPlaneActive);
        Environement.SetActive(desertActive || forestActive); // Activer l'environnement si l'un des deux est actif
    }
}
