using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataResearch
{
    public static List<List<Research>> researchs;
    public static void Init()
    {
        researchs = new();
        researchs.Add(new List<Research>());
        researchs[0].Add(new Research("Tutoriel", new Research[] { }, 2, 2));

        researchs.Add(new List<Research>());
        researchs[1].Add(new Research("Liquides color�es", new Research[] { researchs[0][0] }));

        researchs[1].Add(new Research("Aimants", new Research[] { researchs[0][0] }));

        researchs.Add(new List<Research>());
        researchs[2].Add(new Research("Bases et acides", new Research[] { researchs[1][0] }));

        researchs[2].Add(new Research("Lumi�re", new Research[] { researchs[1][0], researchs[1][1] }));

        researchs[2].Add(new Research("�tats de la mati�re", new Research[] { researchs[1][1] }));

        researchs.Add(new List<Research>());
        researchs[3].Add(new Research("Combustion", new Research[] { researchs[2][1], researchs[2][2] }));

        researchs[3].Add(new Research("Gravit�", new Research[] { researchs[2][2] }));

        researchs.Add(new List<Research>());
        researchs[4].Add(new Research("Ma�trise", new Research[] { researchs[3][0], researchs[3][1] }));
    }

    public static List<Research> GetLevel(int level)
    {
        return researchs[level];
    }

    public static bool IsRequired(Research research)
    {
        foreach (List<Research> r1 in researchs)
        {
            foreach (Research r2 in r1)
            {
                foreach (Research r3 in r2.prerequisites)
                {
                    if (r3 == research)
                        return true;
                }
            }
        }
        return false;
    }

    public static int NbLevels()
    {
        return researchs.Count;
    }

    public static int NbResearch(int level)
    {
        return researchs[level].Count;
    }

    public static int GetLevel(Research target)
    {
        for (int level = 0; level != researchs.Count; ++level)
        {
            foreach (Research r in researchs[level])
            {
                if (r == target)
                    return level;
            }
        }
        return -1;
    }


}
