using UnityEngine;

public class SkillCheckManager : MonoBehaviour
{
    public RadialFillChallenge challengeScript;
    public DoorInteraction doorInteraction;
    public GameObject skillCheckContainer;

    private float previousTimeScale;

    void Start()
    {
        skillCheckContainer.SetActive(false);
    }

    void Update()
    {
        if (doorInteraction != null && doorInteraction.IsPlayerNearDoor() && Input.GetKeyDown(KeyCode.E))
        {
            StartSkillCheck();
        }

        if (challengeScript != null && IsSkillCheckFinished() && skillCheckContainer.activeSelf)
        {
            EndSkillCheck();
        }
    }

    void StartSkillCheck()
    {
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        skillCheckContainer.SetActive(true);
        challengeScript.ToggleGameActive();
        Debug.Log("Skill check started. Time scale: " + Time.timeScale);
    }

    void EndSkillCheck()
    {
        Time.timeScale = 1f; // Force time scale to 1
        skillCheckContainer.SetActive(false);
        challengeScript.ToggleGameActive();
        Debug.Log("Skill check ended. Time scale: " + Time.timeScale);
    }

    bool IsSkillCheckFinished()
    {
        if (challengeScript != null)
        {
            return challengeScript.hitCount >= challengeScript.targetHitsToWin || challengeScript.missCount >= challengeScript.maxMisses;
        }
        return false;
    }
}