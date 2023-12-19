using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    int penetration = 1;
    
    
    Damage damage;
    public void ScaleMode(float scaling)
    {
        Setup();
        for (int i = 0;i != damage.damageAmounts.Length;++i)
        {
            damage.damageAmounts[i] *= scaling;
        }
    }

    public Damage Touched()
    {
        penetration -= 1;
        if (penetration <= 0)
            Destroy(gameObject);
        return damage;

    }
    void Setup()
    {
        damage = new Damage();
        damage.damageAmounts = new float[] { 100 };
        damage.damageTypes = new string[] { "classic" };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
