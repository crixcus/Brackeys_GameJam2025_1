using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private FieldOfViewBehavior fov;

    private void Start()
    {
        if (fov == null)
        {
            fov = GetComponent<FieldOfViewBehavior>();
        }
    }
    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Ensure z-axis stays consistent

        // Calculate direction from player to mouse
        Vector3 direction = (mousePosition - transform.position).normalized;
        
        // Rotate player to face the mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if(fov != null)
        {
            fov.SetAimDirection(direction);
            fov.SetOrigin(transform.position);
        }
    }
}
