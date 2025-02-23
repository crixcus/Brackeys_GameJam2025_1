using UnityEngine;
using UnityEngine.AI;

public class agentMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;

    NavMeshAgent agent;
    private float originalSpeed;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        originalSpeed = agent.speed;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);


        if (distanceToPlayer <= chaseRange)
        {
            agent.SetDestination(target.position);
        }
        else
        {

            agent.SetDestination(transform.position);
        }
    }

    public void StopAgent()
    {
        agent.speed = 0;
    }

    public void ResumeAgent()
    {
        agent.speed = originalSpeed;
    }
}
