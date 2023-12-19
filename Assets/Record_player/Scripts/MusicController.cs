using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public GameObject menu; // Référence au GameObject du menu
    public AudioClip forestSound;
    public AudioClip desertSound;
    public AudioClip spaceSound;
    public AudioSource source;
    public float volume = 0.5f;


    public GameObject LeftHand;
    public GameObject RightHand;

    public GameObject LeftHandWithRay;
    public GameObject RightHandWithRay;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(forestSound, volume);
        menu.SetActive(false);

    }

    public void PlayForestSound(){
        source.Stop();
        source.PlayOneShot(forestSound, volume);
        MenuHide();
 
    }

    public void PlayDesertSound(){
        source.Stop();
        source.PlayOneShot(desertSound, volume);
        MenuHide();

    }

    public void PlaySpaceSound(){
        source.Stop();
        source.PlayOneShot(spaceSound, volume);
        MenuHide();

    }

    public void MenuDisplay(){
        menu.SetActive(true);
        LeftHand.SetActive(false);
        LeftHand.SetActive(false);
        LeftHand.SetActive(true);
        RightHandWithRay.SetActive(true);
    }

    public void MenuHide(){
        menu.SetActive(false);
        LeftHand.SetActive(true);
        RightHand.SetActive(true);
        LeftHandWithRay.SetActive(false);
        RightHandWithRay.SetActive(false);
    }


}
