using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    private bool isFaceingRight;
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootBullet();
        }
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame && isFaceingRight)
        {

        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }
}
