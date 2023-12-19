using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLiquid : MonoBehaviour
{

    AbstractLiquid property;

    public float lifetime = 20;
    float life;

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
        life = 0;
        OutsideParticleManager.main.AddParticle(this);
    }

    void UpdateGraphics()
    {
        GetComponent<Renderer>().material.color = property.color;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity *= (1 - friction * Time.fixedDeltaTime);
        life += Time.fixedDeltaTime;
        if(life > lifetime)
        {
            OutsideParticleManager.main.RemoveParticle(this);
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
