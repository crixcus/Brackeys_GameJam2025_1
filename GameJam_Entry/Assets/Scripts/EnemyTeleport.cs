using UnityEngine;
using UnityEngine.AI;

public class EnemyTeleport : MonoBehaviour
{
    public Transform[] roomTeleportPoints;
    public float detectionRange = 10f;

    private NavMeshAgent agent;
    private GameObject player;

    private AudioManager audioM;

    private void Awake()
    {
        audioM = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer > detectionRange)
        {
            TeleportToNextRoom();
        }
    }

    void TeleportToNextRoom()
    {
        if (roomTeleportPoints.Length == 0) return;


        Transform closestRoom = roomTeleportPoints[0];
        float shortestDistance = Vector3.Distance(player.transform.position, closestRoom.position);

        foreach (Transform room in roomTeleportPoints)
        {
            float distance = Vector3.Distance(player.transform.position, room.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestRoom = room;
            }
        }

        
        agent.Warp(closestRoom.position);
        if (!isPlaying(audioM.enemy_detect))
        {
            audioM.PlaySFX(audioM.enemy_detect);
        }
        
    }

    private bool isPlaying(AudioClip clip)
    {
        return audioM.audioSource.isPlaying && audioM.audioSource.clip == clip;
    }


}