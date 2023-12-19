using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebutFin : MonoBehaviour
{
    // Start is called before the first frame update
    public void jouer()
    {
        SceneManager.LoadScene("PRINCIPAL");
    }

    // Update is called once per frame
    public void RetourAccueil()
    {
        SceneManager.LoadScene("Debut");
    }
}
