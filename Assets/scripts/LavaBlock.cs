using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBlock : MonoBehaviour
{
    public float initialDamage = 15f;
    public float continuousDamage = 15f;
    public float damageInterval = 1f;

    private Health playerHealth;
    private Coroutine damageCoroutine;

    void Start()
    {
        playerHealth = FindObjectOfType<Health>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            ApplyInitialDamage();
            damageCoroutine = StartCoroutine(ApplyContinuousDamage());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            StopCoroutine(damageCoroutine);
        }
    }

    private void ApplyInitialDamage()
    {
        if (playerHealth != null)
        {
            playerHealth.currentHealth -= initialDamage;
        }
    }

    private IEnumerator ApplyContinuousDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageInterval);

            if (playerHealth != null)
            {
                playerHealth.currentHealth -= continuousDamage;
            }

            if (playerHealth.currentHealth <= 0)
            {
                playerHealth.currentHealth = 0;
                break;
            }
        }
    }
}
