using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadialFillChallenge : MonoBehaviour
{
    public Image radialImage;
    public float minAnimationSpeed = 0.5f;
    public float maxAnimationSpeed = 2f;
    public float initialTargetFillPercentage = 0.3f;
    public float minTargetFillPercentage = 0.05f;
    public float targetDecreaseAmount = 0.02f;
    public float targetTolerance = 0.05f;
    public Image targetMarker;
    public TMP_Text hitCounterText;
    public TMP_Text missCounterText;
    public int targetHitsToWin = 5;
    public int maxMisses = 4;
    public float fillSpeed = 0.5f;
    public int hitCount = 0;
    public int missCount = 0;



    private float fillAmount = 0f;
    private bool increasing = true;
    private float currentAnimationSpeed;
    private float targetStart;
    private float targetEnd;
    private bool targetActive = false;
    private float targetFillPercentage;

    private bool gameActive = false;
    private float currentFill = 0f;
    private bool challengeComplete = false;

    void Start()
    {
        targetMarker.gameObject.SetActive(false);
        radialImage.gameObject.SetActive(false);
        hitCounterText.gameObject.SetActive(false);
        missCounterText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameActive)
        {
            if (increasing)
            {
                fillAmount += currentAnimationSpeed * Time.unscaledDeltaTime;
                if (fillAmount >= 1f)
                {
                    fillAmount = 1f;
                    increasing = false;
                    SetRandomSpeed();
                }
            }
            else
            {
                fillAmount -= currentAnimationSpeed * Time.unscaledDeltaTime;
                if (fillAmount <= 0f)
                {
                    fillAmount = 0f;
                    increasing = true;
                    SetRandomSpeed();
                }
            }

            radialImage.fillAmount = fillAmount;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckTargetHit();
                DecreaseTargetSize();
                SetTargetArea();
            }

            if (hitCount >= targetHitsToWin || missCount >= maxMisses)
            {
                EndGame();
            }
        }
    }

    public bool IsComplete()
    {
        return challengeComplete;
    }

    public void ToggleGameActive()
    {
        if (!gameActive)
        {
            StartGame();
        }
        else
        {
            EndGame();
        }
    }

    void StartGame()
    {
        gameActive = true;
        challengeComplete = false; // Reset challengeComplete
        radialImage.gameObject.SetActive(true);
        targetMarker.gameObject.SetActive(true);
        hitCounterText.gameObject.SetActive(true);
        missCounterText.gameObject.SetActive(true);
        SetRandomSpeed();
        targetFillPercentage = initialTargetFillPercentage;
        SetTargetArea();
        UpdateCounterTexts();
        hitCount = 0;
        missCount = 0;
        UpdateCounterTexts();
        fillAmount = 0f; //reset the fill amount.
    }

    void EndGame()
    {
        gameActive = false;
        radialImage.gameObject.SetActive(false);
        targetMarker.gameObject.SetActive(false);
        hitCounterText.gameObject.SetActive(false);
        missCounterText.gameObject.SetActive(false);
        Debug.Log("Game Over! Hits: " + hitCount + ", Misses: " + missCount);
        radialImage.fillAmount = 0; //reset the radial fill.
    }

    void SetRandomSpeed()
    {
        currentAnimationSpeed = Random.Range(minAnimationSpeed, maxAnimationSpeed);
    }

    void DecreaseTargetSize()
    {
        targetFillPercentage -= targetDecreaseAmount;
        targetFillPercentage = Mathf.Max(targetFillPercentage, minTargetFillPercentage);
    }

    void SetTargetArea()
    {
        targetStart = Random.Range(0f, 1f - targetFillPercentage);
        targetEnd = targetStart + targetFillPercentage;
        targetActive = true;
        UpdateTargetMarker();
    }

    void CheckTargetHit()
    {
        if (targetActive && fillAmount >= targetStart - targetTolerance && fillAmount <= targetEnd + targetTolerance)
        {
            Debug.Log("Target Hit!");
            hitCount++;
        }
        else
        {
            Debug.Log("Missed!");
            missCount++;
        }
        UpdateCounterTexts();
    }

    void UpdateCounterTexts()
    {
        if (hitCounterText != null)
        {
            hitCounterText.text = "Hits: " + hitCount;
        }
        if (missCounterText != null)
        {
            missCounterText.text = "Misses: " + missCount;
        }
    }

    void UpdateTargetMarker()
    {
        if (targetMarker != null)
        {
            targetMarker.gameObject.SetActive(true);
            float startAngle = targetStart * 360f;
            float endAngle = targetEnd * 360f;
            float centerAngle = (startAngle + endAngle) / 2f;
            float markerSize = (endAngle - startAngle) / 360f;
            targetMarker.fillAmount = markerSize;
            targetMarker.transform.rotation = Quaternion.Euler(0, 0, -centerAngle);
        }
    }
}