using UnityEngine;
using UnityEngine.UI;

public class RadialFillAnimator : MonoBehaviour
{
    public Image radialImage;
    public float minAnimationSpeed = 0.5f;
    public float maxAnimationSpeed = 2f;

    private float fillAmount = 0f;
    private bool increasing = true;
    private float currentAnimationSpeed;

    void Start()
    {
        if (radialImage == null)
        {
            Debug.LogError("Radial Image component not assigned!");
            enabled = false;
            return;
        }
        SetRandomSpeed();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetRandomSpeed();
        }

        if (increasing)
        {
            fillAmount += currentAnimationSpeed * Time.deltaTime;
            if (fillAmount >= 1f)
            {
                fillAmount = 1f;
                increasing = false;
            }
        }
        else
        {
            fillAmount -= currentAnimationSpeed * Time.deltaTime;
            if (fillAmount <= 0f)
            {
                fillAmount = 0f;
                increasing = true;
            }
        }

        radialImage.fillAmount = fillAmount;
    }

    void SetRandomSpeed()
    {
        currentAnimationSpeed = Random.Range(minAnimationSpeed, maxAnimationSpeed);
    }
}