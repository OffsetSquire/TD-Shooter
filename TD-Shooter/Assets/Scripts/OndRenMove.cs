using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndRenMove : MonoBehaviour
{
    private Transform target;
    private Transform with;
    public float speed;
    public float rotationSpeed;
    public float minDistance;
    private bool isFollowingRen = true;
    private float followTimer = 15f; // Time to follow "Ren" in seconds

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        with = GameObject.FindGameObjectWithTag("Ren").GetComponent<Transform>();
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
}
