using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RenMove : MonoBehaviour
{
    private Transform target;
    private float horizontal;
    private Animator animator;
    private bool isFacingRight = false;

    public float speed;
    private float minDistance; // Now it's a random value between 18 and 23

    private float runTimer = 5f;
    private float runDuration = 3f;

    public int maxHealth = 100;
    private int currentHealth;

    private static int renKillCount = 0; // Static counter to track the number of Ren characters killed
    public string nextSceneName = "Level2"; // Change this to the name of the next scene

    // Start is called before the first frame update
    void Start()
    {
        // Assuming your Animator component is attached to the same GameObject
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        UpdateMinDistance();
        currentHealth = maxHealth; // Initialize current health
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= minDistance)
        {
            // Move towards the player only if within the minimum distance
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            runTimer = runDuration; // Reset the run timer when in proximity to the player
            RotateTowards(isFacingRight ? Vector2.left : Vector2.right); // Call the RotateTowards method
            FlipCharacterSprite(); // Call the Flip method
        }
        else
        {
            // If not in proximity to the player, run for a certain duration
            if (runTimer > 0f)
            {
                // Stand still if facing left
                if (isFacingRight)
                {
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                runTimer -= Time.deltaTime;
            }
            else
            {
                // If the run duration is over, change direction
                
            }
        }
    }

    void FlipCharacterSprite()
    {
        // Assuming your character's sprite renderer is attached to the same GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Flip the sprite based on the direction
        if (isFacingRight)
        {
            spriteRenderer.flipX = false; // No flip
        }
        else
        {
            spriteRenderer.flipX = true; // Flip horizontally
        }
    }

    private void UpdateMinDistance()
    {
        minDistance = Random.Range(18f, 23f);
    }

    // Rotate towards a specified direction
    private void RotateTowards(Vector2 direction)
    {
        // Calculate the rotation angle based on the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Interpolate the current rotation to the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Adjust the speed as needed
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the tag "Bullet"
        if (other.CompareTag("Bullet"))
        {
            // Get the Bullet script or component from the bullet GameObject if available
            Bullet bulletScript = other.GetComponent<Bullet>();

            // Check if the Bullet script is not null
            if (bulletScript != null)
            {
                // Apply damage to the character based on the bullet's damage value
                TakeDamage(34);

                // Destroy the bullet GameObject upon collision (you may want to handle this differently)
                Destroy(other.gameObject);
            }
        }
    }
    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if the character is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function to handle character death
    void Die()
    {
        // Add any death-related actions here
        Destroy(gameObject);
    }
}