using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public GameObject player;
    public GameObject door;
    public RadialFillChallenge radialFillChallenge;
    public string unlockMessage = "Door Unlocked!";
    public Vector3[] teleportPositions; // Set 2 positions in Inspector

    private bool isNearDoor = false;
    private int teleportCount = 0;
    private bool isUnlocking = false;

    AudioManager audioM;

    private void Awake()
    {
        audioM = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        if (radialFillChallenge == null)
        {
            Debug.LogError("RadialFillChallenge is not assigned!", this);
            return;
        }

        radialFillChallenge.enabled = false;
        radialFillChallenge.OnSkillCheckSuccess += UnlockDoor;
    }

    void Update()
    {
        if (player == null || door == null || teleportCount >= 2) return;

        float distance = Vector3.Distance(player.transform.position, door.transform.position);
        bool wasNearDoor = isNearDoor;
        isNearDoor = distance <= interactionDistance;

        if (isNearDoor && !wasNearDoor && !isUnlocking)
        {
            radialFillChallenge.enabled = true;
        }
    }

    void UnlockDoor()
    {
        audioM.PlaySFX(audioM.lockpick);
        if (teleportCount >= 2) return; // Limit teleportations to twice

        isUnlocking = true; // Prevents multiple activations
        teleportCount++;

        if (teleportCount <= teleportPositions.Length)
        {
            door.transform.position = teleportPositions[teleportCount - 1]; // Move door
            Debug.Log($"Door Teleported to {door.transform.position}");
        }

        if (teleportCount >= 2)
        {
            door.SetActive(false); // After second teleport, deactivate it
            Debug.Log("Door has vanished!");
        }

        radialFillChallenge.enabled = false;
        isUnlocking = false; // Reset for next use
    }

    void OnDisable()
    {
        if (radialFillChallenge != null)
        {
            radialFillChallenge.OnSkillCheckSuccess -= UnlockDoor;
        }
    }

    public bool IsPlayerNearDoor()
    {
        return isNearDoor;
    }
}
