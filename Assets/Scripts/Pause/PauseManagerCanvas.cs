using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManagerCanvas : MonoBehaviour
{

    public GameObject panelPause;
    public GameObject panelExperimenter;
    public GameObject Environement;
    // Start is called before the first frame update
    void Start()
    {
        //panelPause.SetActive(true);
        
    }


    public void exprimenter() {

        panelPause.SetActive(false);
        Environement.SetActive(false);
        panelExperimenter.SetActive(true);



    }

    public void ModeDeJeu()
    {
        //SceneManager.LoadScene("SceneModeJeu");


    }

    public void environnements()
    {

        panelPause.SetActive(false);
        panelExperimenter.SetActive(false);
        Environement.SetActive(true);



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
