using UnityEngine;
using System.Collections.Generic;

public class Vision : MonoBehaviour
{
    List<VisionMesh> meshes = new List<VisionMesh>();

    const float START_DEGRESS = 90f;
    float lastLength = 0;

    [SerializeField, Range(0f, 100f)] int length = 12;
    [SerializeField, Range(0f, 360f)] float range = 90f;
    [SerializeField, Range(.01f, 100f)] float maxDistance = 10f;
    [SerializeField] bool debug = false;
    [SerializeField] bool castShadow = false;
    [SerializeField] string targetLayer = "New layer";
    [SerializeField] LayerMask layerMask;
    [SerializeField] Material material;

    void Start()
    {
        lastLength = length;
        SpawnVisionMesh();
    }

    void Update()
    {
        if (lastLength != length)
        {
            lastLength = length;
            SpawnVisionMesh();
        }

        if (length > 0)
            Generate();
    }

    void Generate()
    {
        for (int i = 0; i < length; i++)
        {
            Vector3 startPos = Vector3.zero;
            Vector3 point1 = Raycast(i).point;
            Vector3 point2 = Raycast(i + 1).point;

            DrawTriangle(i, startPos, point1, point2);
        }
    }

    (RaycastHit hit, Vector3 point) Raycast(int i)
    {
        float degress = range / length * i;
        float offsetDegress = range / 2f;

        Vector3 ori = transform.position;
        Vector3 dir = MathfX.DegressToVector3D(degress - offsetDegress + START_DEGRESS);

        RaycastHit hit = RaycastHitX.Cast(ori, dir, layerMask, maxDistance, debug);

        if (debug &&
            hit.collider != null &&
            hit.collider.gameObject.layer == LayerMask.NameToLayer(targetLayer))
            Debug.Log("Can see target");

        return (hit, hit.collider ?
                    Vector3X.IgnoreY(hit.point) : dir * maxDistance);
    }

    void DrawTriangle(int i, Vector3 startPos, Vector3 endPos, Vector3 nextEndPos)
    {
        meshes[i].meshFilter.mesh = MeshGeneration.Triangle(startPos, endPos, nextEndPos);
    }

    void SpawnVisionMesh()
    {
        foreach (var el in meshes)
            Destroy(el.gameObject);

        meshes.Clear();

        for (int i = 0; i < length; i++)
        {
            VisionMesh item = new VisionMesh(transform, material);

            if (!castShadow)
                item.meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            meshes.Add(item);
        }

        if (debug)
            Debug.Log("Clear and re-spawn the vision mesh");
    }
}