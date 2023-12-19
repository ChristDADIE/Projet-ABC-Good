using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{
    public Splash spawn;

    public Liquide liquid;

    public float DistanceSpawn = 3;

    public float minVelocityInSpawn = 10;



    void OnCollisionEnter()
    {
        if (transform.position.magnitude > DistanceSpawn)
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
            float scale = liquid.Property.quantity * 2;
            ((RectTransform)splash.transform).localScale = new Vector3(scale, scale, scale);
            splash.GetComponent<PlayerAttack>().ScaleMode(scale);
        }


        Destroy(this.gameObject);
        
    }


}
