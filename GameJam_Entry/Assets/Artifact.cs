using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Artifact : MonoBehaviour
{
    public string nextSceneName; 
    public float detectionRadius = 2f;
    private GameObject player;
    private bool isNearArtifact = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            isNearArtifact = distance < detectionRadius;

            if (isNearArtifact && Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(nextSceneName); 
            }
        }
    }
}

