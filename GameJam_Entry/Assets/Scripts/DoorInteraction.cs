using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public GameObject player;
    public GameObject door;
    public RadialFillChallenge radialFillChallenge;
    public string unlockMessage = "Door Unlocked!";

    private bool isNearDoor = false;
    private bool doorUnlocked = false;

    void Start()
    {
        radialFillChallenge.enabled = false;
        radialFillChallenge.OnSkillCheckSuccess += UnlockDoor; // Subscribe to event
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

    void OnDestroy()
    {
        radialFillChallenge.OnSkillCheckSuccess -= UnlockDoor; // Unsubscribe to avoid memory leaks
    }

    public bool IsPlayerNearDoor()
    {
        return isNearDoor;
    }

}
