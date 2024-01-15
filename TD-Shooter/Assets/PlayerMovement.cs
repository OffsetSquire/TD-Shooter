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

    // I denna void Update s� g�r koden s� att n�r man g� i en x axel, s� f�r man den velocity man beh�ver f�r att kunna g�.
    // If Koden g�r s� att, om man trycker p� hopp knappen, samtidigt som antalet hopps man har kvar �r �ver 0, s� kan man hoppa.
    // Den g�r ocks� s� att om man hoppar s� g�r ner antalet hopps man har kvar med 1, till den g�r ned till 0 d� efter det man kan inte hoppa l�ngre p� grund av att kraven f�r att kunna hoppa inte �r d� uppfyllda.
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

    // Denna kod g�r bara s� att man f�r tillbaka sina hopp efter att man landat p� marken igen.
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