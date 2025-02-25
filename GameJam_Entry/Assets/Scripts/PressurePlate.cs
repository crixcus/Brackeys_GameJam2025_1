using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public door door;
    public Transform checkPosition;
    public float checkRadius = 0.5f;
    public LayerMask playerLayer;
    private bool isActivated = false;

    private void Update()
    {
        bool playerOnPlate = Physics2D.OverlapCircle(checkPosition.position, checkRadius, playerLayer);

        if (playerOnPlate && !isActivated)
        {
            isActivated = true;
            door.OpenDoor();
        }
        else if (!playerOnPlate && isActivated)
        {
            isActivated = true;
            door.OpenDoor();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(checkPosition.position, checkRadius);
    }
}