using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndRenMove : MonoBehaviour
{
    private Transform target;
    private Transform with;
    private float speed; // Now it's a random speed
    public float rotationSpeed;
    public float minDistance;
    private bool isFacingRight = false;
    private bool isFollowingRen = true;
    private float followTimer = 5f; // Time to follow "Ren" in seconds

    public int maxHealth = 100;
    private int currentHealth;

    private static int renKillCount = 0; // Static counter to track the number of Ren characters killed
    public string nextSceneName = "Level2"; // Change this to the name of the next scene

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        with = GameObject.FindGameObjectWithTag("Ren").GetComponent<Transform>();
        UpdateSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowingRen)
        {
            FollowRen();
        }
        else
        {
            FollowPlayer();
        }
    }

    void FollowRen()
    {
        float distanceToRen = Vector2.Distance(transform.position, with.position);

        if (distanceToRen <= minDistance)
        {
            // Rotate towards the "Ren" object
            Vector2 directionToRen = (with.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToRen.y, directionToRen.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);

            // Move towards the "Ren" object
            transform.position = Vector2.MoveTowards(transform.position, with.position, speed * Time.deltaTime);
        }

        // Update timer
        followTimer -= Time.deltaTime;
        if (followTimer <= 0f)
        {
            isFollowingRen = false;
            followTimer = 5f; // Reset the timer
            UpdateSpeed(); // Update the speed when changing direction
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
    void FollowPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= minDistance)
        {
            // Rotate towards the player only if within the minimum distance
            Vector2 directionToPlayer = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);

            // Move towards the player only if within the minimum distance
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void UpdateSpeed()
    {
        speed = Random.Range(8f, 10f); // Set the speed to a random value between 3 and 6
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
        renKillCount++;

        // Check if the required number of Rens are killed to change the scene
        if (renKillCount >= 4)
        {
            ChangeScene();
        }
        // Add any death-related actions here
        Destroy(gameObject);
    }

    void ChangeScene()
    {
        // Change the scene to the specified next scene
        SceneManager.LoadScene("Level2");
    }
}
