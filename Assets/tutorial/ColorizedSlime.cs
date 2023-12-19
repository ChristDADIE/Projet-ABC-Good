using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorizedSlime : Slime
{
    public Color factorDamage = new Color(1,0,0);
    public override void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerAttack pa))
        {
            Damage damage = pa.Touched();
            if (damage.liquid == null)
                return;
            float factor = Mathf.Pow((factorDamage.r - damage.liquid.color.r), 2) + Mathf.Pow((factorDamage.g - damage.liquid.color.g), 2) + Mathf.Pow((factorDamage.b - damage.liquid.color.b), 2);
            factor /= 3;
            factor = 1 - factor;
            Debug.Log("facteur de d�g�t = " + factor);
            if (factor < 0.6667)
                factor = 0;


            for (int i = 0; i != damage.damageTypes.Length; ++i)
                Damage(damage.damageAmounts[i]* factor, damage.damageTypes[i]);
        }
    }

    protected override void Animation()
    {
        base.Animation();
        if (subdivision == -1)
            isDead = true;
    }
}
