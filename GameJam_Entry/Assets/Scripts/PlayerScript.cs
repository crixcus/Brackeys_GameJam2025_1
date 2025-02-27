using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 20f; // Speed of movement
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isMoving = false;

    private AudioManager audioM;
    private AudioSource audioSource; // AudioSource to handle looping

    private void Awake()
    {
        audioM = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Add an AudioSource component dynamically
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioM.running;
        audioSource.loop = true; // Enable looping
        audioSource.playOnAwake = false; // Don't play at start
    }

    void Update()
    {
        // Get input from WASD keys
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;

        // Check if the player is moving
        if (moveInput.magnitude > 0)
        {
            if (!isMoving)
            {
                isMoving = true;
                PlayRunningSound();
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                StopRunningSound();
            }
        }
    }

    void PlayRunningSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void StopRunningSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
