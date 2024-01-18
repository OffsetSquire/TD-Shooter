using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private float Horizontal;
    public float speed;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    private float Move;
    private bool isFacingRight = true;
    private bool isCrouching = false;
    private Rigidbody2D rb;

    public float jump;
    public int jumpsLeft;

    private Vector2 standingColliderSize;
    private Vector2 crouchingColliderSize;
    public Image healthBar;
    public float healthAmount = 100f;

    private StaminaManager staminaManager; // Added variable

    public void TakeDamage(float damage)
    {
        Debug.Log("takedamage");
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        standingColliderSize = GetComponent<BoxCollider2D>().size;
        crouchingColliderSize = new Vector2(standingColliderSize.x, standingColliderSize.y / 2f);

        // Find the StaminaManager script on the player or set it through the Inspector
        staminaManager = GetComponent<StaminaManager>();
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            //ShootBullet();
        }

        HandleMovement();
        HandleJump();

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
            UpdateColliderSize();
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            UpdateColliderSize();
        }

        if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(25);
        }

    }
    
    void BaseSpeed()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Move = Input.GetAxis("Horizontal");
    }


    void HandleMovement()
    {
        BaseSpeed();

        float currentSpeed = isCrouching ? speed / 3f : speed;

        if (staminaManager.IsRunning)
        {
            // Check if the shift key is held down to increase speed
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                currentSpeed *= 1.5f; // Adjust the multiplier as needed
            }

            rb.velocity = new Vector2(currentSpeed * Move, rb.velocity.y);
        }
        else
        {
            // If not running, set velocity to zero
            rb.velocity = new Vector2(currentSpeed * Horizontal, rb.velocity.y);
        }

        Rotate();
    }


    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpsLeft -= 1;
        }
    }

    void UpdateColliderSize()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        if (isCrouching)
        {
            collider.size = crouchingColliderSize;
        }
        else
        {
            collider.size = standingColliderSize;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = 1;
        }
    }

    private void Rotate()
    {
        if ((isFacingRight && Horizontal < 0f) || (!isFacingRight && Horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 rotation = transform.rotation.eulerAngles;

            rotation.y += 180f;

            rotation.y += 180f;     

            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
