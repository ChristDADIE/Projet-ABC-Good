using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMover : MonoBehaviour
{
    [SerializeField]
    float Distance;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePosition();

    }

    public void UpdatePosition()
    {
        Vector3 direction = Vector3.ProjectOnPlane(Camera.main.transform.forward, new Vector3(0, 1, 0));
        if (direction == new Vector3(0, 0, 0))
            direction = new Vector3(1, 0, 0);
        transform.position = Camera.main.transform.position + direction.normalized * Distance;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
