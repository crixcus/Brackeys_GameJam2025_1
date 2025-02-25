using UnityEngine;

public class door : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D doorCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<Collider2D>();
    }

    public void OpenDoor()
    {
        spriteRenderer.enabled = false; // Hide door
        doorCollider.enabled = false;   // Disable collision
    }

    public void CloseDoor()
    {
        spriteRenderer.enabled = true;  // Show door
        doorCollider.enabled = true;    // Enable collision
    }
}