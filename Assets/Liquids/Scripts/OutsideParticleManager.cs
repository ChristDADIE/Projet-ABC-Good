using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideParticleManager : MonoBehaviour
{
    public static OutsideParticleManager main;

    List<ParticleLiquid> particles = new();

    public List<Liquide> liquids = new();

    [SerializeField]
    int max = 100;
    void Awake()
    {
        if (main != null)
            Destroy(this);
        else
            main = this;
    }

    public void AddParticle(ParticleLiquid pl)
    {
        if(particles.Count >= max)
        {
            Destroy(particles[0].gameObject);
            RemoveParticle(particles[0]);
        }
        particles.Add(pl);
    }

    public void RemoveParticle(ParticleLiquid pl)
    {
        particles.Remove(pl);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(particles.Count);
    }

    private void FixedUpdate()
    {
        foreach(Liquide recipient in liquids)
        {
            List<ParticleLiquid> toAdd = new();
            foreach(ParticleLiquid pl in particles)
            {
                if(recipient.IsFilling(pl.GetComponent<SphereCollider>()))
                {
                    toAdd.Add(pl);
                }
            }
            foreach(ParticleLiquid pl in toAdd)
            {
                if(recipient.Quantity / recipient.volume + pl.Property.quantity <= 1) // ne deborde pas
                {
                    Debug.Log(recipient.Property.color);
                    recipient.Property = AbstractLiquid.merge(recipient.Property, pl.Property);
                    Debug.Log(recipient.Property.color);
                    Destroy(pl.gameObject);
                    RemoveParticle(pl);
                }
            }
        }
    }
}
