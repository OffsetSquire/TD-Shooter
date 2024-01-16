using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }
}
