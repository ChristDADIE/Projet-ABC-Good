using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLiquid : MonoBehaviour
{

    AbstractLiquid property;

    public AbstractLiquid Property
    {
        get
        {
            return property;
        }
        set
        {
            property = value;
            UpdateGraphics();
        }
    }

    [SerializeField]
    float friction;
    void Start()
    {
        OutsideParticleManager.main.AddParticle(this);
    }

    void UpdateGraphics()
    {
        GetComponent<Renderer>().material.color = property.color;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity *= (1 - friction * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
