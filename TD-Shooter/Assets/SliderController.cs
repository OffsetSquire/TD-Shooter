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

        // Set the slider's value based on the fill amount
        slider.value = fillAmount;
    }

    // Example function to shorten the fill amount (called when a button is pressed, for instance)
    public void ShortenFillAmount()
    {
        // Adjust the fill amount by a certain value (e.g., decrease by 0.1)
        float currentFillAmount = slider.value;
        float newFillAmount = Mathf.Clamp01(currentFillAmount - 0.1f);
        SetSliderFillAmount(newFillAmount);
    }
}