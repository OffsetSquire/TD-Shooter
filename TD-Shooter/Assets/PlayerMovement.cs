using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    // Collider dimensions for standing and crouching
    private Vector2 standingColliderSize;
    private Vector2 crouchingColliderSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        standingColliderSize = GetComponent<BoxCollider2D>().size;
        crouchingColliderSize = new Vector2(standingColliderSize.x, standingColliderSize.y / 2f);
    }

    void Update()


    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootBullet();
        }
        void ShootBullet()
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
        HandleMovement();
        HandleJump();

        // Check for crouch input
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
    }

    void HandleMovement()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Move = Input.GetAxis("Horizontal");

        float currentSpeed = isCrouching ? speed / 3f : speed;

        // Check if the shift key is held down to increase speed
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed *= 1.5f; // Adjust the multiplier as needed
        }

        rb.velocity = new Vector2(currentSpeed * Move, rb.velocity.y);
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
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
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}