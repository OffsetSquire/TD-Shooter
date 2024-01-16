using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    public Image staminaBar;
    public float staminaAmount = 100f;
    public float normalSpeed = 5f;
    private float runningSpeed = 10f; // Double the normal speed
    private bool isRunning = false;
    private float staminaDrainRate = 0.2f; // Adjust the stamina drain rate here
    private float regenRate = 0.1f; // Adjust the stamina regeneration rate here

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Adjust movement speed based on running state
        float currentSpeed = isRunning ? runningSpeed : normalSpeed;
        // Add your movement logic here using the 'currentSpeed' variable

        // Adjust stamina based on running state
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            LoseStamina(staminaDrainRate); // Adjust the faster stamina decrease rate while running
        }
        else
        {
            isRunning = false;
            RegenStamina(regenRate); // Adjust the slower stamina regeneration rate when not running
        }
    }

    public void LoseStamina(float damage)
    {
        staminaAmount -= damage;
        staminaBar.fillAmount = staminaAmount / 100f;

        // If stamina reaches 0, stop running
        if (staminaAmount <= 0)
        {
            isRunning = false;
        }
    }

    public void RegenStamina(float staminaRegenAmount)
    {
        staminaAmount += staminaRegenAmount;
        staminaAmount = Mathf.Clamp(staminaAmount, 0, 100);

        staminaBar.fillAmount = staminaAmount / 100f;
    }
}
