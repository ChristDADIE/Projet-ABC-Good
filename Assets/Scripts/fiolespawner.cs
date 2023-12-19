using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fiolespawner : MonoBehaviour
{

    public GameObject fiole;
    public Vector3 position;

    public float deltaTime;
    public float deltaPosition;

    float time = 0;

    Transform current;
    // Start is called before the first frame update
    void Start()
    {
        current = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(current == null || (current.position-(transform.position+ position)).magnitude > deltaPosition)
        {
            time += Time.deltaTime;
            if(time > deltaTime)
            {
                GameObject c = Instantiate(fiole);
                current = c.transform;
                current.position = transform.position + position;
                time = 0;
            }
        }
    }
}
