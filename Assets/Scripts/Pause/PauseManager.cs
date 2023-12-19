using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public class PauseManager : MonoBehaviour
{

    public InputAction PauseButton;
    public GameObject menuPause;
    public GameObject Environement;
    public GameObject panelExperimenter;
    public GameObject[] environmentObjectsDesactivate;
    public GameObject[] environmentObjectsActivate;

    

    

    // Savoir si le bouton est mis sur pause ou pas
    private bool isMenuActive = false;

    private void OnEnable()
    {
        PauseButton.Enable();
    }

    private void OnDisable()
    {
        PauseButton.Disable();
    }

    private void Start()
    {
        PauseButton.performed += OnButtonAPressed;
       
    }

    private void OnButtonAPressed(InputAction.CallbackContext context)
    {
        // Inverse l'état du menu pause
        isMenuActive = !isMenuActive;


        // Acitve ou désactive le menu
        ToggleMenuAndEnvironment(isMenuActive);

        // on fait pause au jeu
        ToggleTimeScale(isMenuActive);


    }

    public void ToggleMenuAndEnvironment(bool pauseMenuActive)
    {
        // Active/désactive le canvas du menu en fonction du paramètre
        menuPause.SetActive(pauseMenuActive);


        // Active/désactive les objets de l'environnement en fonction du paramètre
        foreach (GameObject obj in environmentObjectsActivate)
        {
            obj.SetActive(pauseMenuActive);
        }

        // Désactive/active les objets de l'environnement en fonction du paramètre
        foreach (GameObject obj in environmentObjectsDesactivate)
        {
            obj.SetActive(!pauseMenuActive);
        }
    }

    public void ToggleTimeScale(bool pauseMenuActive)
    {
        if (pauseMenuActive)
        {
            // Si le menu est actif, on arrete le temps (timeScale à 0)
            Time.timeScale = 0f;
        }
        else
        {
            // Sinon on le mets à 1 pour que le temps s'écoule
            Time.timeScale = 1f;
        }
    }


}
