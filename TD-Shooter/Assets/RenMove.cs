using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenMove : MonoBehaviour
{
    private Transform target;
    private float horizontal;
    private bool isFacingRight = false;

    public float speed;
    private float minDistance; // Now it's a random value between 18 and 23

    private float runTimer = 0f;
    private float runDuration = 3f;

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
            runTimer = runDuration; // Reset the run timer when in proximity to the player
            Flip(); // Call the Flip method
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
                Flip();
            }
        }
    }

    private void Flip()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // Get input inside the Flip method

        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= 1f;
            transform.localScale = newScale;

            UpdateMinDistance(); // Update the minimum distance when the direction changes
        }
    }

    private void UpdateMinDistance()
    {
        minDistance = Random.Range(18f, 23f);
    }
}