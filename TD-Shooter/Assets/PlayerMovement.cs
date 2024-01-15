using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float Horizontal;
    public float speed;
    private float Move;
    private bool isFacingRight = true;
    private Rigidbody2D rb;

    public float jump;

    public int jumpsLeft;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // I denna void Update så gör koden så att när man gå i en x axel, så får man den velocity man behöver för att kunna gå.
    // If Koden gör så att, om man trycker på hopp knappen, samtidigt som antalet hopps man har kvar är över 0, så kan man hoppa.
    // Den gör också så att om man hoppar så går ner antalet hopps man har kvar med 1, till den går ned till 0 då efter det man kan inte hoppa längre på grund av att kraven för att kunna hoppa inte är då uppfyllda.
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Move = Input.GetAxis("Horizontal");
        Flip();

        rb.velocity = new Vector2(speed * Move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            jumpsLeft -= 1;
        }
    }

    // Denna kod gör bara så att man får tillbaka sina hopp efter att man landat på marken igen.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsLeft = 2;
        }
    }

    private void Flip()
    {
        if (isFacingRight && Horizontal < 0f || !isFacingRight && Horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}