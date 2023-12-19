using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseTutorielFonction : MonoBehaviour
{
    public void modeExperience() {

        SceneManager.LoadScene("PRINCIPAL");
    }

    public void quitter() {

        SceneManager.LoadScene("Fin");

    }
}
