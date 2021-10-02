using UnityEngine;

public static class MeshGeneration
{
    public static Mesh Triangle(Vector3 a, Vector3 b, Vector3 c)
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[] { a, b, c };
        mesh.vertices = vertices;

        int[] tris = new int[] { 0, 2, 1, };
        mesh.triangles = tris;

        Vector2[] uv = new Vector2[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1)
        };

        mesh.uv = uv;

        return mesh;
    }
}