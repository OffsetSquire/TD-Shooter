using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider; // Reference to your Slider component

    void Start()
    {
        // Ensure that the slider reference is not null
        if (slider == null)
        {
            Debug.LogError("Slider reference is not set!");
            return;
        }

        // Set the initial fill amount (0 to 1, where 0 is empty and 1 is full)
        float initialFillAmount = 0.5f; // Adjust this value based on your needs
        SetSliderFillAmount(initialFillAmount);
    }

    // Function to set the fill amount of the slider
    void SetSliderFillAmount(float fillAmount)
    {
        // Clamp the fill amount to be within the valid range (0 to 1)
        fillAmount = Mathf.Clamp01(fillAmount);

        // Set the slider's normalized value based on the fill amount
        slider.normalizedValue = fillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Shift key is held down
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            // Decrease the fill amount by a different value (e.g., 0.05) when Shift is held
            float currentFillAmount = slider.normalizedValue;
            float newFillAmount = Mathf.Clamp01(currentFillAmount - 0.05f);
            SetSliderFillAmount(newFillAmount);
        }
        else
        {
            // Increase the fill amount by a different value (e.g., 0.05) when Shift is not held
            float currentFillAmount = slider.normalizedValue;
            float newFillAmount = Mathf.Clamp01(currentFillAmount + 0.05f);
            SetSliderFillAmount(newFillAmount);
        }
    }
}
