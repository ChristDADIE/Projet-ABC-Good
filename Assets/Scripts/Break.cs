using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Break : MonoBehaviour
{
    public Splash spawn;

    public Liquide liquid;

    public float DistanceSpawn = 3;

    public float minVelocityInSpawn = 10;



    void OnCollisionEnter()
    {
        XRGrabInteractable _;
        if (transform.position.magnitude > DistanceSpawn || !TryGetComponent< XRGrabInteractable>(out _))
            breaking();
        else if(GetComponent<Rigidbody>().velocity.magnitude > minVelocityInSpawn)
        {
            breaking();
        }
    }

    void breaking()
    {
        if(liquid.Property.quantity > 0.2)
        {
            Splash splash = Instantiate(spawn);
            splash.transform.position = transform.position;
            splash.GetComponent<ParticleSystemRenderer>().material.color = liquid.Property.color;
            float scale = liquid.Property.quantity;
            ((RectTransform)splash.transform).localScale = new Vector3(scale, scale, scale);
            splash.GetComponent<PlayerAttack>().ScaleMode(scale,liquid.Property);
        }


        Destroy(this.gameObject);
        
    }


}
