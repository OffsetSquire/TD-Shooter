using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 3f;
    float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
