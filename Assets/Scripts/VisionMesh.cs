using UnityEngine;

[System.Serializable]
public class VisionMesh
{
    public GameObject gameObject;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public VisionMesh(Transform parent, Material material)
    {
        gameObject = new GameObject("Vision Mesh");
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        meshRenderer.material = material;

        gameObject.transform.parent = parent;
        gameObject.transform.localPosition = Vector3.zero;
    }
}