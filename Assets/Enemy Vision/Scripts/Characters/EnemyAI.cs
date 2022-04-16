using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRotation;

    float smooothRotationTime = 3f;

    [SerializeField] FieldOfView fieldOfView;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] float stoppingDistance = 1f;

    private void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        fieldOfView.SetOrigin(transform.position);
        fieldOfView.SetDirection(transform.forward);

        Destination();

        if (agent.remainingDistance <= .1f)
            transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * smooothRotationTime);
    }

    private void Destination()
    {
        var destination = Vector3.zero;

        if (fieldOfView.IsTarget)
        {
            destination = target.position;
            agent.stoppingDistance = stoppingDistance;
        }
        else
        {
            destination = startPos;
            agent.stoppingDistance = 0;
        }

        agent.SetDestination(destination);
    }
}