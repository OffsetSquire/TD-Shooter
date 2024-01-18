using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenMove : MonoBehaviour
{
    private Transform target;
    private float horizontal;

    public float speed;
    private float minDistance; // Now it's a random value between 5 and 23
    private bool isFacingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        UpdateMinDistance();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= minDistance)
        {
            // Move towards the player only if within the minimum distance
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
            Flip(); // Call the Flip method
        }
    }

    private void Flip()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get input inside the Flip method

        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1f;
            transform.localScale = newScale;

            UpdateMinDistance(); // Update the minimum distance when the direction changes
        }
    }

    private void UpdateMinDistance()
    {
        minDistance = Random.Range(5f, 23f);
    }
}