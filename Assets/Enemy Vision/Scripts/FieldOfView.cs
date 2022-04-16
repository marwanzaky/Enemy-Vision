using UnityEngine;
using MarwanZaky.MathfX;
using MarwanZaky.Methods;

public class FieldOfView : MonoBehaviour
{
    Mesh mesh;

    Vector3 origin = Vector3.zero;

    float startAngle;

    [SerializeField] MeshFilter meshFilter;
    [SerializeField] int rayCount = 2;
    [SerializeField] float fieldOfView = 90f;
    [SerializeField] float distance = 50f;
    [SerializeField] Vector3 offset;
    [SerializeField] LayerMask targetMask;
    [SerializeField] LayerMask layerMask;

    public bool IsTarget { get; private set; }

    private void Start()
    {
        mesh = new Mesh();
        meshFilter.mesh = mesh;
    }

    private void LateUpdate()
    {
        IsTarget = false;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        var angle = startAngle;
        var angleIncrease = fieldOfView / rayCount;

        var vertices = new Vector3[rayCount + 1 + 1];
        var uv = new Vector2[vertices.Length];
        var triangles = new int[rayCount * 3];

        var vertexIndex = 1;
        var trianglesIndex = 0;

        vertices[0] = origin;

        for (int i = 0; i <= rayCount; i++)
        {
            var hit = PhysicsX.Raycast(origin, MathfX.AngleToVector3D(angle), layerMask, distance, debug: true);
            var vertex = hit.collider ? hit.point : origin + MathfX.AngleToVector3D(angle + transform.eulerAngles.y) * distance;

            if (hit.collider != null && CompareLayer(hit.collider.gameObject.layer, targetMask))
                IsTarget = true;

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[trianglesIndex + 0] = 0;
                triangles[trianglesIndex + 1] = vertexIndex - 1;
                triangles[trianglesIndex + 2] = vertexIndex;

                trianglesIndex += 3;
            }

            vertexIndex++;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin + offset;
    }

    public void SetDirection(Vector3 direction)
    {
        startAngle = MathfX.VectorToAngle3D(direction) + fieldOfView / 2f;
    }

    bool CompareLayer(LayerMask layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}