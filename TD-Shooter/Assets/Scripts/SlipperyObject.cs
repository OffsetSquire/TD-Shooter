using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyObject : MonoBehaviour
{
    public float slipperyFactor = 0.5f; // Adjust this value to control slipperiness

    private Collider2D objectCollider;

    void Start()
    {
        objectCollider = GetComponent<Collider2D>();

        // Check if the object has a Collider2D component
        if (objectCollider == null)
        {
            Debug.LogError("SlipperyObject script requires a Collider2D component!");
        }
    }

    void Update()
    {
        // Check if the object is in contact with the ground (adjust layers accordingly)
        // You might want to refine this check based on your game's setup
        if (IsGrounded())
        {
            ApplySlipperyEffect();
        }
    }

    bool IsGrounded()
    {
        // Adjust the raycast distance and layer mask based on your game's setup
        RaycastHit2D hit = Physics2D.Raycast(objectCollider.bounds.center, Vector2.down, objectCollider.bounds.extents.y + 0.1f);

        return hit.collider != null;
    }

    void ApplySlipperyEffect()
    {
        // Adjust the friction to make the object slippery
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.sharedMaterial.friction = slipperyFactor;
        }
    }
}