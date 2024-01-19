using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float initialMovement = 5;
    public float speed;
    public Rigidbody2D rb;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bulcol");
        //

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy.inst.TakeDamage(1);
        }
    }
    void Start()
    {
        rb.velocity = new Vector2(initialMovement * speed, 0f) * Time.deltaTime;
    }

}
