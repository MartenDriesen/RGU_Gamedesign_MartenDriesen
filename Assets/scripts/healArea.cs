using System.Collections;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    public int healAmount = 20;
    public float healInterval = 1f;
    public float damageCheckDuration = 4f;

    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;

    private Health health;
    private float lastHealthValue;
    private float damageCheckTimer = 0f;
    private bool canHeal = false;
    private bool playerInHealArea = false;
    private Coroutine healingCoroutine;

    void Start()
    {
        health = FindObjectOfType<Health>();
        lastHealthValue = health.currentHealth;
    }

    void Update()
    {
        if (playerInHealArea && health.currentHealth == lastHealthValue)
        {
            damageCheckTimer += Time.deltaTime;

            if (damageCheckTimer >= damageCheckDuration && !canHeal)
            {
                canHeal = true;
                healingCoroutine = StartCoroutine(HealOverTime());
            }
        }
        else if (health.currentHealth != lastHealthValue)
        {
            damageCheckTimer = 0f;
            canHeal = false;

            if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine);
                healingCoroutine = null;
            }
        }

        lastHealthValue = health.currentHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            playerInHealArea = true;
            damageCheckTimer = 0f;

            one.SetActive(true);
            two.SetActive(true);
            three.SetActive(true);
            four.SetActive(true);
            five.SetActive(true);
            six.SetActive(true);
            seven.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("player"))
        {
            playerInHealArea = false;
            canHeal = false;
            damageCheckTimer = 0f;

     if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine);
                healingCoroutine = null;
            }
        }
    }

    IEnumerator HealOverTime()
    {
 while (canHeal)
        {  health.currentHealth = Mathf.Min(health.currentHealth + healAmount, health.maxHealth);
 if (health.currentHealth >= health.maxHealth)
            {
                canHeal = false;
                break;
            }

            yield return new WaitForSeconds(healInterval);
        }
    }
}
