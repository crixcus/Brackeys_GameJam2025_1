using UnityEngine;

public class Qmark : MonoBehaviour
{
    public GameObject questionMark; 
    public GameObject dialogueBox; 
    public float detectionRadius = 2f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        questionMark.SetActive(false); 
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < detectionRadius && !dialogueBox.activeSelf)
            {
                questionMark.SetActive(true); 
                questionMark.transform.position = transform.position + new Vector3(0, 1.5f, 0);
            }
            else
            {
                questionMark.SetActive(false); 
            }
        }
    }
}
