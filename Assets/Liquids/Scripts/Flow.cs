using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    [SerializeField]
    Liquide liquid;

    [SerializeField]
    Vector3 spawnPosition;

    [SerializeField]
    ParticleLiquid particleLiquid;

    [SerializeField]
    float particlePerUnit = 1.0f;

    [SerializeField]
    Vector3 randomMovements;
    
    void Start()
    {
        
    }

    void spawn()
    {
        ParticleLiquid pl = Instantiate<ParticleLiquid>(particleLiquid);
        pl.transform.position = liquid.transform.position + liquid.transform.rotation * spawnPosition * ((RectTransform)transform).localScale.x;
        pl.GetComponent<Rigidbody>().velocity = -liquid.oldAcceleration+ liquid.transform.rotation *new Vector3(randomMovements.x*Random.value, randomMovements.y * Random.value, randomMovements.z * Random.value);
        pl.Property = liquid.Property;
        pl.Property.quantity = 1 / particlePerUnit;
    }

    float cooldown = 0;
    void Update()
    {
        if(liquid.Quantity != 0)
        {
            if(liquid.Quantity < liquid.Flowing)
            {
                cooldown += liquid.Quantity;
                liquid.Quantity = 0;
            }
            else
            {
                cooldown += liquid.Flowing;
                liquid.Quantity -= liquid.Flowing;
            }
            float delay = 1 / particlePerUnit;
            while (cooldown >= delay)
            {
                spawn();
                cooldown -= delay;
            }
        }
    }
}
