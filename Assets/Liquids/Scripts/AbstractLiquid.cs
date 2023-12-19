using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbstractLiquid
{
    public Color color;
    public float quantity;
    public type[] t;
    public enum type
    {
        water,
        pigments
    }

    public AbstractLiquid(Color color, float quantity, type[] t)
    {
        this.color = color;
        this.quantity = quantity;
        this.t = t;
    }

    static type[] MergeTypes(type[] t1,type[] t2)
    {
        return t1.Union(t2).ToArray();
    }

    public void Set(AbstractLiquid al)
    {
        this.color = al.color;
        this.quantity = al.quantity;
        this.t = (type[])al.t.Clone();
    }


    public static AbstractLiquid merge(AbstractLiquid al1, AbstractLiquid al2)
    {
        Color newcolor;
        if (al1.color.a == 0 && al2.color.a == 0)
            newcolor = new Color(0, 0, 0, 0);
        else
            newcolor = Color.Lerp(al1.color, al2.color, al2.color.a* al2.quantity / (al1.color.a* al1.quantity + al2.color.a* al2.quantity));

        return new AbstractLiquid(newcolor, al1.quantity + al2.quantity, MergeTypes(al1.t, al2.t));
    }

}
