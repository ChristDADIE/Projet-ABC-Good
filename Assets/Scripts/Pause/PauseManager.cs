using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public static class GameManager
{
    public static bool IsPaused { get; private set; }

    public static void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f; // Arrête le temps
    }

    public static void UnpauseGame()
    {
        IsPaused = false;
        Time.timeScale = 1f; // Reprend le temps
    }
}


public class PauseManager : MonoBehaviour
{

    public InputAction PauseButton;
    public GameObject menuPause;
    public GameObject[] environmentObjectsDesactivate;
    public GameObject[] environmentObjectsActivate;

    public MenuEnvironemnents menuEnvironemnents; // Référence à MenuEnvironemnents

    // Référence à la caméra VR pour adapter la vue quand on mettra le jeu sur pause
    public Camera vrCamera; 

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
        menuEnvironemnents = FindObjectOfType<MenuEnvironemnents>(); // Trouve l'instance de MenuEnvironemnents
    }

    private void OnButtonAPressed(InputAction.CallbackContext context)
    {
        // Inverse l'état du menu pause
        isMenuActive = !isMenuActive;


        if (isMenuActive)
        {
            GameManager.PauseGame();
            menuEnvironemnents.retour(); // Appelle la fonction de MenuEnvironemnents
        }
        else
        {
            GameManager.UnpauseGame();
            menuEnvironemnents.Unpause(); // Ajoute une fonction Unpause dans MenuEnvironemnents si nécessaire
        }


        // Acitve ou désactive le menu
        ToggleMenuAndEnvironment(isMenuActive);

        // on fait pause au jeu
        ToggleTimeScale(isMenuActive);

        // Positionne le menu devant la caméra VR lorsque le menu est activé
        if (isMenuActive && vrCamera != null)
        {
            PositionMenuInFrontOfCamera();
        }
    }

    private void ToggleMenuAndEnvironment(bool pauseMenuActive)
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

    private void ToggleTimeScale(bool pauseMenuActive)
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

    private void PositionMenuInFrontOfCamera()
    {
        // Place le menu devant la caméra et oriente-le vers la caméra
        Vector3 cameraPosition = vrCamera.transform.position;

        float distanceFromCamera = 2.0f;

        Vector3 menuPosition = cameraPosition + vrCamera.transform.forward * distanceFromCamera;
        menuPause.transform.position = menuPosition;


        // Assurez-vous que le panneau MenuPause est actif avant de le positionner
        if (menuPause.activeSelf)
        {
            // Oriente le menu vers la caméra
            //menuPause.transform.LookAt(cameraPosition + vrCamera.transform.forward);
            menuPause.transform.LookAt(2 * menuPause.transform.position - cameraPosition);
        }
    }

}
