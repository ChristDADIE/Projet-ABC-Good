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

        panelExperimenter.SetActive(true);
        panelPause.SetActive(false);
        Environement.SetActive(false);
        



    }

    public void ModeDeJeu()
    {
        SceneManager.LoadScene("Tutoriel");
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

        SceneManager.LoadScene("Fin");



    }
}
