using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;

    private void Update()
    {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetDirection(transform.forward);
    }
}