using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LaserBeam
{
    // Declaring class variables
    UnityEngine.Vector3 pos, dir;
    public GameObject laserObj;
    LineRenderer laser;
    List<UnityEngine.Vector3> laserIndices = new List<UnityEngine.Vector3>();

    // Dictionary holding refractive indices of different materials
    Dictionary<string, float> refractiveMaterials = new Dictionary<string, float>()
    {
        {"Air", 1.0f},
        {"Water", 1.33f},
        {"Glass", 1.5f},
        {"Diamond", 2.42f}
    };

    // Constructor for LaserBeam class
    public LaserBeam(UnityEngine.Vector3 pos, UnityEngine.Vector3 dir, Material material)
    {
        // Initializing variables and setting up the laser object
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.red;
        this.laser.endColor = Color.red;

        // Casting a ray to determine laser path
        CastRay(pos, dir, laser);
    }

    // Casts a ray and determines the path of the laser beam
    void CastRay(UnityEngine.Vector3 pos, UnityEngine.Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
        {
            // Handles the scenario when the ray hits an object
            CheckHit(hit, dir, laser);
        }
        else
        {
            // Handles the scenario when the ray doesn't hit any object
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    // Updates the positions of the laser beam
    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;
        foreach (UnityEngine.Vector3 index in laserIndices)
        {
            laser.SetPosition(count, index);
            count++;
        }
    }

    // Handles the scenario when the ray hits an object
    void CheckHit(RaycastHit hitInfo, UnityEngine.Vector3 direction, LineRenderer laser)
    {
        if (hitInfo.collider.gameObject.tag == "Mirror")
        {
            // Handles reflection from a mirror
            UnityEngine.Vector3 pos = hitInfo.point;
            UnityEngine.Vector3 dir = UnityEngine.Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, laser);
        }
        else if (hitInfo.collider.gameObject.tag == "Refract")
        {
            // Handles refraction through a refractive surface
            UnityEngine.Vector3 pos = hitInfo.point;
            laserIndices.Add(pos);
            // Ensure direction components are not close to zero
            float epsilon = 0.001f; // Adjust the epsilon value as needed
            float xDir = Mathf.Abs(direction.x) < epsilon ? epsilon : direction.x;
            float yDir = Mathf.Abs(direction.y) < epsilon ? epsilon : direction.y;
            float zDir = Mathf.Abs(direction.z) < epsilon ? epsilon : direction.z;

            // Calculate new position with adjusted direction components
            UnityEngine.Vector3 newPos1 = new UnityEngine.Vector3(
                Mathf.Abs(direction.x) / xDir * epsilon + hitInfo.point.x,
                Mathf.Abs(direction.y) / yDir * epsilon + hitInfo.point.y,
                Mathf.Abs(direction.z) / zDir * epsilon + hitInfo.point.z
            );
            float n1 = refractiveMaterials["Air"];
            float n2 = refractiveMaterials["Glass"];

            UnityEngine.Vector3 norm = hitInfo.normal;
            UnityEngine.Vector3 incident = direction;

            UnityEngine.Vector3 refractedVector = Refract(n1, n2, norm, incident);

             Debug.Log("refvector :" + refractedVector);

            CastRay(newPos1, refractedVector, laser);
        }
        else
        {
            // Handles the scenario when the ray hits any other object
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    // Function to calculate the refracted vector
    public static UnityEngine.Vector3 Refract(float RI1, float RI2, UnityEngine.Vector3 surfNorm, UnityEngine.Vector3 incident)
    {
        surfNorm.Normalize(); // should already be normalized, but normalize just to be sure
        incident.Normalize();

        return (RI1 / RI2 * UnityEngine.Vector3.Cross(surfNorm, UnityEngine.Vector3.Cross(-surfNorm, incident)) - surfNorm * Mathf.Sqrt(1 - UnityEngine.Vector3.Dot(UnityEngine.Vector3.Cross(surfNorm, incident) * (RI1 / RI2 * RI1 / RI2), UnityEngine.Vector3.Cross(surfNorm, incident)))).normalized;
    }
}
