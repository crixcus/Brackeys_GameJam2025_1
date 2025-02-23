using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public GameObject player; // Assign the player object in the inspector.
    public GameObject door;   // Assign the door object in the inspector.
    public RadialFillChallenge radialFillChallenge; // Assign the SkillCheck script in the inspector.
    public string unlockMessage = "Door Unlocked!"; // Message to display upon successful unlock.

    private bool isNearDoor = false;
    private bool doorUnlocked = false;

    void Start()
    {
      

        radialFillChallenge.enabled = false; // Initially disable the skill check.
    }

    void Update()
    {
        if (player != null && door != null && !doorUnlocked)
        {
            float distance = Vector3.Distance(player.transform.position, door.transform.position);

            if (distance <= interactionDistance)
            {
                isNearDoor = true;
                if (!radialFillChallenge.enabled)
                {
                    radialFillChallenge.enabled = true;
                }

                if (radialFillChallenge.IsComplete())
                {
                    UnlockDoor();
                }
            }
            else
            {
                isNearDoor = false;
                radialFillChallenge.enabled = false;
            }
        }

    }

    void UnlockDoor()
    {
        doorUnlocked = true;
        radialFillChallenge.enabled = false;
        door.SetActive(false);
        Debug.Log(unlockMessage); 
      
    }

    public bool IsPlayerNearDoor()
    {
        return isNearDoor;
    }


    void OnDrawGizmosSelected()
    {
        if (door != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(door.transform.position, interactionDistance);
        }
    }
}