using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private Health health;

    void Start()
    {
        health = FindObjectOfType<Health>();
    }

    void OnCollisionEnter2D(Collision2D other)
    { if (other.gameObject.CompareTag("player"))
     {
            health.currentHealth = 0;
     }
    }
}
