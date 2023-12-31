using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(OutsideParticleManager))]
[RequireComponent(typeof(HandleManager))]
public class MainManager : MonoBehaviour
{
    public enum context
    {
        mainMenu,
        selection,
        level
    };

    context globalContext;
    public context GlobalContext
    {
        set
        {
            globalContext = value;
        }
    }


    void Start()
    {
        globalContext = context.mainMenu;
        StartLevel(0);
    }

    public static MainManager main;

    private void Awake()
    {
        main = this;
    }

    void StartLevel(int id)
    {
        globalContext = context.level;
        GetComponent<LevelManager>().Id = id;
        GetComponent<LevelManager>().StartLevel();
    }

    public void LevelEnded()
    {

    }

    
    void Update()
    {
        
    }



}
