using UnityEngine;

public class ArtifactAudio : MonoBehaviour
{
    public AudioSource audioSource; 
    public float detectionRadius = 2f; 
    private GameObject player;
    private bool isPlaying = false; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < detectionRadius)
            {
                if (Input.GetKeyDown(KeyCode.E) && !audioSource.isPlaying)
                {
                    audioSource.Play(); 
                    isPlaying = true;
                }
            }
            else
            {
                if (isPlaying) 
                {
                    audioSource.Stop();
                    isPlaying = false;
                }
            }
        }
    }
}
