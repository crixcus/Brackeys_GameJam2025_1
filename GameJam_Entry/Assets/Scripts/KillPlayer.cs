using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes if needed

public class KillPlayer : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 2f;
    private GameObject player;
    public GameObject GameOverPanel;
    private bool detected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < detectionRadius && !detected)
            {
                detected = true;
                EnemyKillPlayer();
            }
        }
    }

    void EnemyKillPlayer()
    {
        Debug.Log("Player has been caught!");
        Time.timeScale = 0f; 
        //SceneManager.LoadScene("GameOverScene");
        GameOverPanel.SetActive(!GameOverPanel.activeSelf);
    }
}