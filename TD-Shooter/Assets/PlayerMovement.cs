using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;




public class PlayerMovement : MonoBehaviour
{
    public float ClipLength = 1f;
    public GameObject AudioClip;



    public AudioSource audioSource;
    public AudioClip shootingAudioClip;
    bool canShoot = true;
    public int ammountOfBullets;
    public int maxAmmountOfBullets;
    public bool canMove = true;


    private float horizontal;
    public float speed;
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    private float move;
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
    private void Awake()
    {
        ammountOfBullets = maxAmmountOfBullets;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("takedamage");
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;

        if (healthAmount <= 0)
        {
            // You may want to call a separate function for handling player death here.
            // For example: PlayerDied();
        }
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;
    }

    void Start()
    {
        AudioClip.SetActive(false);



        rb = GetComponent<Rigidbody2D>();
        standingColliderSize = GetComponent<BoxCollider2D>().size;
        crouchingColliderSize = new Vector2(standingColliderSize.x, standingColliderSize.y / 2f);

        // Find the StaminaManager script on the player or set it through the Inspector
        staminaManager = GetComponent<StaminaManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown("r"))
        {
            StartCoroutine(Reload());
        }
            
        if(canMove)
        {
            HandleMovement();
            HandleJump();
        }
        

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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(25);
        }

        void ShootBullet()
        {
            if (bulletPrefab != null && shootingPoint != null && ammountOfBullets > 0 && canShoot)
            {

                audioSource.PlayOneShot(shootingAudioClip);

                Quaternion q= shootingPoint.rotation;
                GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
                ammountOfBullets -= 1;
                canShoot = false;
                StartCoroutine(ShootCoolDown());
            }


            
            



        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootBullet();

        }

    }
    public IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(2);
        canShoot = true;
    }
    public IEnumerator Reload()
    {
        canMove = false;
        yield return new WaitForSeconds(2.5f);
        for (int i = 0; i < maxAmmountOfBullets; i++)
        {
            ammountOfBullets++;
            if (ammountOfBullets > maxAmmountOfBullets)
            {
                ammountOfBullets--;
                
            }
        }
        canMove = true;
    }

    void BaseSpeed()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        move = Input.GetAxis("Horizontal");
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

            rb.velocity = new Vector2(currentSpeed * move, rb.velocity.y);
        }
        else
        {
            // If not running, set velocity to zero
            rb.velocity = new Vector2(currentSpeed * horizontal, rb.velocity.y);
        }

        Rotate(horizontal);
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
        else if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = 1;
        }
    }

    private void Rotate(float horizontalInput)
    {
        if (horizontalInput > 0 && !isFacingRight){
                    
            // Flip the player
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
        else if(horizontalInput < 0 && isFacingRight)
        {
            // Flip the player
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180, 0);
        }
    }
}
