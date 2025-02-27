using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public door door;
    public Transform checkPosition;
    public float checkRadius = 0.5f;
    public LayerMask playerLayer;
    private bool isActivated = false;

    private AudioManager audioM;

    private void Awake()
    {
        audioM = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        bool playerOnPlate = Physics2D.OverlapCircle(checkPosition.position, checkRadius, playerLayer);

        if (playerOnPlate && !isActivated)
        {
            isActivated = true;
            audioM.PlaySFX(audioM.door);
            door.OpenDoor();
        }
        else if (!playerOnPlate && isActivated)
        {
            isActivated = true;
            door.OpenDoor();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPosition.position, checkRadius);
    }
}