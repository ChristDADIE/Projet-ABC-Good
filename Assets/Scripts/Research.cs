using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Research
{
    public string name;
    public Research[] prerequisites;
    public int unlocked, total_unlocked;

    public BaseResearch ui_research;

    public bool IsUnlocked()
    {
        return unlocked == total_unlocked;
    }

    public bool CanBeUnlocked()
    {
        foreach (Research r in prerequisites)
        {
            if (!r.IsUnlocked())
                return false;
        }
        return true;
    }


    public Research(string name, Research[] prerequisites, int steps = 1, int unlocked = 0)
    {
        this.name = name;
        this.prerequisites = prerequisites;
        this.unlocked = unlocked;
        this.total_unlocked = steps;
    }

}
