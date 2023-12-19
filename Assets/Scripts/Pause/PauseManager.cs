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
        // Inverse l'�tat du menu pause
        isMenuActive = !isMenuActive;


        // Acitve ou d�sactive le menu
        ToggleMenuAndEnvironment(isMenuActive);

        // on fait pause au jeu
        ToggleTimeScale(isMenuActive);

        //// Positionne le menu devant la cam�ra VR lorsque le menu est activ�
        //if (isMenuActive && vrCamera != null)
        //{
        //    PositionMenuInFrontOfCamera();
        //}
    }

    public void ToggleMenuAndEnvironment(bool pauseMenuActive)
    {
        // Active/d�sactive le canvas du menu en fonction du param�tre
        menuPause.SetActive(pauseMenuActive);


        // Active/d�sactive les objets de l'environnement en fonction du param�tre
        foreach (GameObject obj in environmentObjectsActivate)
        {
            obj.SetActive(pauseMenuActive);
        }

        // D�sactive/active les objets de l'environnement en fonction du param�tre
        foreach (GameObject obj in environmentObjectsDesactivate)
        {
            obj.SetActive(!pauseMenuActive);
        }
    }

    public void ToggleTimeScale(bool pauseMenuActive)
    {
        if (pauseMenuActive)
        {
            // Si le menu est actif, on arrete le temps (timeScale � 0)
            Time.timeScale = 0f;
        }
        else
        {
            // Sinon on le mets � 1 pour que le temps s'�coule
            Time.timeScale = 1f;
        }
    }

    //private void PositionMenuInFrontOfCamera()
    //{
    //    // Place le menu devant la cam�ra et oriente-le vers la cam�ra
    //    Vector3 cameraPosition = vrCamera.transform.position;

    //    float distanceFromCamera = 2.0f;

    //    Vector3 menuPosition = cameraPosition + vrCamera.transform.forward * distanceFromCamera;
    //    menuPause.transform.position = menuPosition;


    //    // Assurez-vous que le panneau MenuPause est actif avant de le positionner
    //    if (menuPause.activeSelf)
    //    {
    //        // Oriente le menu vers la cam�ra
    //        //menuPause.transform.LookAt(cameraPosition + vrCamera.transform.forward);
    //        menuPause.transform.LookAt(2 * menuPause.transform.position - cameraPosition);
    //    }
    //}

}
