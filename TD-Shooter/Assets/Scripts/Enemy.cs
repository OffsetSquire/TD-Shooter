using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy inst;
    public float maxHealth = 3f;
    float health;

    private void Start()
    {
        inst = this;
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
