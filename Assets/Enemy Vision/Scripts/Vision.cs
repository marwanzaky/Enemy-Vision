using UnityEngine;
using System.Collections.Generic;

public class Vision : MonoBehaviour
{
    List<VisionMesh> meshes = new List<VisionMesh>();

    const float START_DEGRESS = 90f;

    float lastLength = 0;

    public bool seeingTarget = false;

    [SerializeField, Range(1, 120)] int length = 12;
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

        if (seeingTarget)
            OnSeeingTarget();

        foreach (var el in meshes)
        {
            el.gameObject.transform.rotation = Quaternion.identity;
            el.gameObject.transform.position = Vector3.zero;
        }
    }

    void Generate()
    {
        seeingTarget = false;

        for (int i = 0; i < length; i++)
        {
            var hit1 = Raycast(i);
            var hit2 = Raycast(i + 1);

            Vector3[] triangleVers = new Vector3[3] {
                transform.position,
                hit1.worldSpacePoint,
                hit2.worldSpacePoint
            };

            if (!seeingTarget)
                seeingTarget = hit1.seeingTarget;

            Mesh trianglMesh = MeshGeneration.Triangle(triangleVers[0], triangleVers[1], triangleVers[2]);
            meshes[i].meshFilter.mesh = trianglMesh;
        }
    }

    (bool seeingTarget, Vector3 worldSpacePoint) Raycast(int i)
    {
        float degress = range / length * i;
        float offsetDegress = range / 2f + transform.eulerAngles.y;

        Vector3 ori = transform.position;
        Vector3 dir = MathfX.DegressToVector3D(degress - offsetDegress + START_DEGRESS);

        RaycastHit hit = RaycastHitX.Cast(ori, dir, layerMask, maxDistance, debug);

        return (hit.collider &&
                hit.collider.gameObject.layer == LayerMask.NameToLayer(targetLayer),

                hit.collider ?
                Vector3X.IgnoreY(hit.point, transform.position.y) :
                transform.position + dir * maxDistance);
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

    protected virtual void OnSeeingTarget()
    {

    }
}