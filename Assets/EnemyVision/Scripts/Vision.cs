using UnityEngine;

public class Vision : MonoBehaviour
{
    float startDegress = 90f;

    [SerializeField] int length = 12;
    [SerializeField] float range = 90f;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] bool debug = false;
    [SerializeField] LayerMask layerMask;

    void Update()
    {
        for (int i = 0; i < length; i++)
        {
            float unit = range / length;
            float degress = unit * i;
            float offsetDegress = range / 2f;

            Vector3 ori = transform.position;
            Vector3 dir = MathfX.DegressToVector3D(degress - offsetDegress + startDegress);

            RaycastHitX.Cast(ori, dir, layerMask, maxDistance, debug);
        }
    }
}