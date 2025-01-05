using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Unity.FPS.AI;

public class RandomMovementModule : MonoBehaviour
{

    [SerializeField]
    private float movementRadius = 4f;

    [SerializeField]
    private float sensibilityOfDistance = 0.5f;

    private NavMeshAgent controller; 

    Vector3 finalPosition = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<NavMeshAgent>();
        finalPosition = RandomNavmeshLocation(movementRadius);
        controller.SetDestination(finalPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, finalPosition) <= sensibilityOfDistance)
        {
            finalPosition = RandomNavmeshLocation(movementRadius);
            controller.SetDestination(finalPosition);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;

        Debug.Log("chi entra");

        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
