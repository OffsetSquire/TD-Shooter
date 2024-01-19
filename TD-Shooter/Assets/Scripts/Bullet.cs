using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float initialMovement = 5;
    public float speed;
    public Rigidbody2D rb;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
            Destroy(gameObject);
    }
    void Start()
    {
        rb.velocity = new Vector2(initialMovement * speed, 0f) * Time.deltaTime;
    }

}
