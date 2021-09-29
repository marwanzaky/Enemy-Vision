using UnityEngine;

[System.Serializable]
public class VisionMesh
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public VisionMesh(Transform parent, Material material)
    {
        GameObject go = new GameObject("Vision Mesh");
        meshFilter = go.AddComponent<MeshFilter>();
        meshRenderer = go.AddComponent<MeshRenderer>();

        meshRenderer.material = material;

        go.transform.parent = parent;
        go.transform.localPosition = Vector3.zero;
    }
}