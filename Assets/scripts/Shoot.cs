using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float travelDistance = 3f;
    public float travelSpeed = 10f;
    public Transform frontArm;
    public float returnSpeed = 5f;
    public float shootCooldown = 0.5f;

    private MoveSprite moveSprite;
    private Quaternion originalArmRotation;
    private float lastShootTime;

    void Start()
    {
        moveSprite = GetComponent<MoveSprite>();
        if (frontArm != null)
        {
            originalArmRotation = frontArm.localRotation;
        }
        lastShootTime = -shootCooldown;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShootTime + shootCooldown)
        {
            ShootProjectile();
            RotateFrontArm();
            lastShootTime = Time.time;
        }

        if (frontArm != null)
        {  frontArm.localRotation = Quaternion.Slerp(
                frontArm.localRotation,
                originalArmRotation,
                Time.deltaTime * returnSpeed
            );
        }
    }

    void ShootProjectile()
    { if (projectilePrefab == null || frontArm == null) return;

        Vector2 shootDirection = moveSprite.faceRight ? Vector2.right : Vector2.left;

        Vector3 spawnPosition = frontArm.position;
        spawnPosition.x += moveSprite.faceRight ? 2f : -2f;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = shootDirection * travelSpeed;

        if (!moveSprite.faceRight)
        {
            Vector3 projectileScale = projectile.transform.localScale;
            projectileScale.x = -Mathf.Abs(projectileScale.x);
            projectile.transform.localScale = projectileScale;
        }
     Destroy(projectile, travelDistance / travelSpeed);
    }

    void RotateFrontArm()
    {   frontArm.localRotation = Quaternion.Euler(0, 0, -90);
    }
}
