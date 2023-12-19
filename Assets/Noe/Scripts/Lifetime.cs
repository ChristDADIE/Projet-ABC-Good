using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime;

    float life;
    void Start()
    {
        life = 0;
    }

    // Update is called once per frame
    void Update()
    {
        life += Time.deltaTime;
        if (life > lifetime)
            Destroy(gameObject);
    }
}
