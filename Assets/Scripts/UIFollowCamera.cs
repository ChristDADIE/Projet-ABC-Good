using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    [SerializeField]
    float Distance = 100;

    [SerializeField]
    public bool fixedMode;
    public bool FixedMode
    {
        get
        {
            return fixedMode;
        }

        set
        {
            if(value)
            {
                SetMenuPosition();
            }
            else
            {
                transform.position = GoodPosition();
            }
            fixedMode = value;
        }
    }

    Vector3 speed;

    void Start()
    {
        transform.position = GoodPosition();
        speed = new Vector3(0, 0, 0);
    }

    public void TowardsCamera()
    {
        transform.forward = transform.position- Camera.main.transform.position;
    }

    public Vector3 GoodPosition()
    {
        Vector3 direction = Camera.main.transform.forward;
        return Camera.main.transform.position + direction.normalized * Distance;
    }

    public void SetMenuPosition()
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
        if(!FixedMode)
        {
            TowardsCamera();
            speed += (GoodPosition() - transform.position) * Time.deltaTime * 3;
            speed *= 0.8f;
            transform.position += speed;
        }
    }
}
