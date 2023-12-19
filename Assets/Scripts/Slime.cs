using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    float baseSize;
    public float bouncyFactor, bouncyFrequency;
    float factor;
    public float subdivision;
    override public void Setup(LevelManager level, float factor, Vector3 position)
    {
        base.Setup(level, factor, position);
        baseSize = ((RectTransform)transform).localScale.x;
        this.factor = factor;
    }
    override protected void Animation()
    {
        ((RectTransform)transform).localScale = new Vector3(baseSize, baseSize * (1 + bouncyFactor * Mathf.Cos(Time.time * bouncyFrequency)), baseSize);
    }

    override protected void Death()
    {
        if (subdivision > 0)
        {
            Slime slime1 = Instantiate<Slime>(this);
            Slime slime2 = Instantiate<Slime>(this);
            Vector2 deltaPosition1 = Random.insideUnitCircle * Mathf.Sqrt(factor);
            Vector2 deltaPosition2 = Random.insideUnitCircle * Mathf.Sqrt(factor);
            slime1.Setup(level, factor * 0.5f, transform.position + new Vector3(deltaPosition1.x, 0, deltaPosition1.y));
            slime1.subdivision = subdivision - 1;

            slime2.Setup(level, factor * 0.5f, transform.position + new Vector3(deltaPosition1.x, 0, deltaPosition1.y));
            slime2.subdivision = subdivision - 1;

            level.AddEnemies(slime1);
            level.AddEnemies(slime2);
        }
        base.Death();
    }
}
