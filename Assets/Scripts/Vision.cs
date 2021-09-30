using UnityEngine;
using System.Collections.Generic;

public class Vision : MonoBehaviour
{
    List<VisionMesh> meshes = new List<VisionMesh>();
    float startDegress = 90f;

    [SerializeField] int length = 12;
    [SerializeField] float range = 90f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] bool debug = false;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Material material;

    void Start()
    {
        for (int i = 0; i < length; i++)
        {
            VisionMesh item = new VisionMesh(transform, material);
            meshes.Add(item);
        }
    }

    void Update()
    {
        for (int i = 0; i < length; i++)
        {
            Vector3 endPos = Raycast(i);
            Vector3 nextEndPos = Raycast(i + 1);
            GenerateTriangle(i, Vector3.zero, endPos, nextEndPos);
        }
    }

    Vector3 Raycast(int i)
    {
        float unit = range / length;
        float degress = unit * i;
        float offsetDegress = range / 2f;

        Vector3 ori = transform.position;
        Vector3 dir = MathfX.DegressToVector3D(degress - offsetDegress + startDegress);

        RaycastHit hit = RaycastHitX.Cast(ori, dir, layerMask, maxDistance, debug);

        return (hit.collider ? Vector3X.IgnoreY(hit.point) : dir * maxDistance);
    }

    void GenerateTriangle(int i, Vector3 startPos, Vector3 endPos, Vector3 nextEndPos)
    {
        meshes[i].meshFilter.mesh = MeshGeneration.Triangle(startPos, endPos, nextEndPos);
    }
}