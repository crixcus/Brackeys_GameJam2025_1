using UnityEngine;
using UnityEngine.AI;

public class agentMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 10f;

    private NavMeshAgent agent;
    private float originalSpeed;
    private SpriteRenderer spriteRenderer;
   

    private AudioManager audioM;

    private void Awake()
    {
        audioM = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;  
        agent.updateUpAxis = false;    
        originalSpeed = agent.speed;

        spriteRenderer = GetComponent<SpriteRenderer>(); // Get sprite renderer for flipping
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position); // Use Vector2 for 2D

        if (distanceToPlayer <= chaseRange)
        {
            audioM.PlaySFX(audioM.enemy_detect);
            agent.SetDestination(target.position); 
            FaceTarget();
        }
        else
        {
            agent.SetDestination(transform.position); // Stop movement
            audioM.StopSFX(audioM.enemy_detect);
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void StopAgent()
    {
        agent.speed = 0;
    }

    public void ResumeAgent()
    {
        agent.speed = originalSpeed;
    }

    //public void isTriggerEnter2D(Collider2D colli)
    //{
    //    if (colli.CompareTag("Player"))
    //    {
            
            
    //    }
    //}
}