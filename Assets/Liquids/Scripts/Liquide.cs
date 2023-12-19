using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class Liquide : MonoBehaviour
{
    [SerializeField]
    public float volume;

    [SerializeField]
    public Color color;

    [SerializeField]
    Mesh[] shapes;

    float previousQuantity;
    [SerializeField]
    float quantity;

    AbstractLiquid.type[] t = new AbstractLiquid.type[] { };


    public AbstractLiquid Property
    {
        get
        {
            return new AbstractLiquid(GetComponent<Renderer>().material.color, quantity * volume, t);
        }
        set
        {
            quantity = value.quantity / volume;
            GetComponent<Renderer>().material.color = value.color;
            this.t = value.t;
        }
    }

    public float Quantity
    {
        get
        {
            return quantity;
        }

        set
        {
            quantity = value;
        }
    }

    [SerializeField]
    float mass = 1.0f;

    [SerializeField]
    float viscosity = 0.05f;

    [SerializeField]
    Rigidbody movements;

    [SerializeField]
    float delta = 0.01f;

    [SerializeField]
    float gravity = 1f;

    [SerializeField]
    Vector3 center;

    [SerializeField]
    Mesh triggerMesh;

    Vector3[] triggerVertices;

    [SerializeField]
    bool calm;
    //comment
    [SerializeField]
    public Vector3 output;

    public bool Calm
    {
        get
        {
            return calm;
        }

        set
        {
            calm = value;
            previousQuantity = -1;
            if (calm)
            {
                speed = new Vector3(0, 0, 0);
                oldSpeed = new Vector3(0, 0, 0);
            }
        }
    }

    [SerializeField]
    Collider input;

    Vector3[][] references;
    Mesh liquid;
    Vector3[][] liquids;

    Vector3 direction;
    Vector3 speed;
    Vector3 oldSpeed;

    private float shaking;

    public float Shaking
    {
        get
        {
            return shaking;
        }
    }

    [NonSerialized]
    public Vector3 oldAcceleration = new Vector3(0, 0, 0);

    void UpdateReference()
    {
        for (int i = 0; i != shapes.Length; ++i)
        {
            if (!shapes[i].isReadable)
                Debug.Log("Erreur, le mesh " + i + " n'est pas en mode readable");
        }


        references = new Vector3[shapes.Length][];
        for (int i = 0; i != shapes.Length; ++i)
        {
            Vector3[] vertices_base = shapes[i].vertices;
            int[] triangles_base = shapes[i].triangles;
            List<Vector3> vertices = new();
            for (int j = 0; j != vertices_base.Length; ++j)
            {
                vertices_base[j] = vertices_base[j] + delta * (center - vertices_base[j]).normalized;
            }
            for (int j = 0; j != triangles_base.Length; ++j)
            {
                vertices.Add(vertices_base[triangles_base[j]]);
            }
            references[i] = vertices.ToArray();
        }
    }

    void Fuse(Vector3[][] meshs, Mesh target)
    {
        int total = 0;
        for (int i = 0; i != meshs.Length; ++i)
        {
            total += meshs[i].Length;
        }

        Vector3[] vertices = new Vector3[total];
        int[] triangles = new int[total];

        int currentVerticesIndex = 0;
        for (int i = 0; i != meshs.Length; ++i)
        {
            for (int j = 0; j != meshs[i].Length; ++j)
            {
                vertices[currentVerticesIndex++] = meshs[i][j];
            }

        }
        for (int i = 0; i != total; ++i)
        {
            triangles[i] = i;
        }
        target.vertices = vertices;
        target.triangles = triangles;
    }

    bool[] CalculateVerticesInPlan(Vector3 position, Vector3 direction, Vector3[] target)
    {
        bool[] verticeInPlan = new bool[target.Length];

        for (int i = 0; i < target.Length; ++i)
        {
            verticeInPlan[i] = Vector3.Dot(target[i] - position, direction) > 0;
        }
        return verticeInPlan;
    }

    void CalculateCut1(Plane plan, List<(Vector3, Vector3, Vector3)> listMetaTriangles, Vector3 a, Vector3 b, Vector3 c)
    {
        plan.Raycast(new Ray(a, c - a), out float distanceD);
        plan.Raycast(new Ray(a, b - a), out float distanceE);
        Vector3 d = a + (c - a).normalized * distanceD;
        Vector3 e = a + (b - a).normalized * distanceE;
        if (pointCut == new Vector3(0, 0, 0))
        {
            pointCut = e;
            listMetaTriangles.Add((a, e, d));
        }
        else
        {
            listMetaTriangles.Add((a, e, d)); listMetaTriangles.Add((e, pointCut, d));
        }
    }

    void CalculateCut2(Plane plan, List<(Vector3, Vector3, Vector3)> listMetaTriangles, Vector3 a, Vector3 b, Vector3 c)
    {
        plan.Raycast(new Ray(a, c - a), out float distanceD);
        plan.Raycast(new Ray(b, c - b), out float distanceE);
        Vector3 d = a + (c - a).normalized * distanceD;
        Vector3 e = b + (c - b).normalized * distanceE;
        if (pointCut == new Vector3(0, 0, 0))
        {
            pointCut = e;
            listMetaTriangles.Add((a, e, d)); listMetaTriangles.Add((a, b, e));
        }
        else
        {
            listMetaTriangles.Add((a, e, d)); listMetaTriangles.Add((a, b, e)); listMetaTriangles.Add((e, pointCut, d));
        }
    }

    Vector3 pointCut;
    void UpdateLiquid(Vector3 position, Vector3 direction)
    {
        liquid.Clear();
        for (int index_ref = 0; index_ref != references.Length; ++index_ref)
        {

            Vector3[] target = references[index_ref];

            bool[] verticeInPlan = CalculateVerticesInPlan(position, direction, target);

            pointCut = new Vector3(0, 0, 0);

            List<(Vector3, Vector3, Vector3)> listMetaTriangles = new();


            for (int i = 0; i < target.Length; i += 3)
            {
                int permutation = 0;
                if (verticeInPlan[i]) permutation += 1 << 0;
                if (verticeInPlan[i + 1]) permutation += 1 << 1;
                if (verticeInPlan[i + 2]) permutation += 1 << 2;

                Vector3 a, b, c;
                int cut = 0;
                switch (permutation)
                {
                    case 0:
                        listMetaTriangles.Add((target[i], target[i + 1], target[i + 2]));
                        continue;
                    case 1:
                        (a, b, c) = (target[i + 1], target[i + 2], target[i]); cut = 2; break;
                    case 2:
                        (a, b, c) = (target[i + 2], target[i], target[i + 1]); cut = 2; break;
                    case 3:
                        (a, b, c) = (target[i + 2], target[i], target[i + 1]); cut = 1; break;
                    case 4:
                        (a, b, c) = (target[i], target[i + 1], target[i + 2]); cut = 2; break;
                    case 5:
                        (a, b, c) = (target[i + 1], target[i + 2], target[i]); cut = 1; break;
                    case 6:
                        (a, b, c) = (target[i], target[i + 1], target[i + 2]); cut = 1; break;
                    default:
                        continue;
                }
                Plane plan = new(direction, position);

                if (cut == 1)
                {

                    CalculateCut1(plan, listMetaTriangles, a, b, c);
                }
                else
                {
                    CalculateCut2(plan, listMetaTriangles, a, b, c);
                }

            }
            Vector3[] listVertices = new Vector3[listMetaTriangles.Count * 3];

            for (int i = 0; i != listMetaTriangles.Count; ++i)
            {
                int j = i * 3;
                listVertices[j] = listMetaTriangles[i].Item1;
                listVertices[j + 1] = listMetaTriangles[i].Item2;
                listVertices[j + 2] = listMetaTriangles[i].Item3;
            }


            liquids[index_ref] = listVertices;
        }
        Fuse(liquids, liquid);
        liquid.RecalculateNormals();
        //liquid.Optimize();

    }
    void Start()
    {
        UpdateReference();
        liquid = new Mesh();
        liquids = new Vector3[references.Length][];

        GetComponent<MeshFilter>().mesh = liquid;
        direction = (Quaternion.Inverse(transform.rotation)) * (new Vector3(0, 1, 0));
        oldSpeed = new Vector3(0, 0, 0);
        speed = new Vector3(0, 0, 0);
        shaking = 0;
        previousQuantity = -1;
        triggerVertices = triggerMesh.vertices;
        GetComponent<Renderer>().material.color = color;

    }

    private Vector3 GetDown()
    {
        return Quaternion.Inverse(transform.rotation) * new Vector3(0, 1, 0);
    }


    private void FixedUpdate()
    {
        if (movements != null && !calm)
        {
            Vector3 acceleration = (movements.velocity - oldSpeed) / mass + GetDown() * Time.fixedDeltaTime * gravity;
            oldAcceleration = acceleration;
            oldSpeed = movements.velocity;
            shaking = acceleration.magnitude;
            speed += acceleration;
            speed = Vector3.ProjectOnPlane(speed, direction) * Mathf.Pow(0.999f, viscosity / (Time.fixedDeltaTime * (quantity + 0.2f)));
            direction += speed;
            direction = direction.normalized;
            Vector3 position = direction * scale + center;
            bool isFlowing = false;
            if (triggerMesh != null)
            {
                for (int i = 0; i != triggerMesh.vertexCount; ++i)
                {

                    if (Vector3.Dot(triggerVertices[i] - position, direction) <= 0)
                    {
                        isFlowing = true;
                        break;
                    }
                }
            }
            if (isFlowing)
            {
                flowing = Vector3.Dot(-acceleration, output);
                if (flowing < 0)
                    flowing = 0;
            }
            else
            {
                flowing = 0;
            }

        }
        else
        {
            direction = (Quaternion.Inverse(transform.rotation)) * (new Vector3(0, 1, 0));
            shaking = 0;
        }
    }


    float flowing;

    public float Flowing

    {
        get
        {
            return flowing;
        }
        set
        {
            flowing = value;
        }
    }

    private float scale = 0;


    public bool IsFilling(Collider liquidObject)
    {
        if (input == null)
            return false;
        return input.bounds.Intersects(liquidObject.bounds);

    }

    bool needUpdate;
    void Update()
    {
        needUpdate = !needUpdate;
        if (!needUpdate)
            return;
        if (!calm || previousQuantity != quantity)
        {
            previousQuantity = quantity;
            float borneMin = float.MaxValue;
            float borneMax = float.MinValue;
            for (int i = 0; i != references.Length; ++i)
            {
                for (int j = 0; j != references[i].Length; ++j)
                {
                    float value = Vector3.Dot(direction, references[i][j] - center);
                    if (value < borneMin) borneMin = value;
                    if (value > borneMax) borneMax = value;
                }
            }
            scale = (borneMax - borneMin) * quantity + borneMin;
            Vector3 position = direction * scale + center;
            UpdateLiquid(position, direction);
        }
        needUpdate = false;


    }
}
