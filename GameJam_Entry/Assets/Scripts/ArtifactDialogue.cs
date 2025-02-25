using UnityEngine;

public class ArtifactDialogue : MonoBehaviour
{
    public GameObject dialogueBox; 
    public float detectionRadius = 2f; 
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueBox.SetActive(false); 
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < detectionRadius && Input.GetKeyDown(KeyCode.E))
            {
                dialogueBox.SetActive(true); 
            }
        }
    }

    public void CloseDialogue()
    {
        dialogueBox.SetActive(false);
    }
}
