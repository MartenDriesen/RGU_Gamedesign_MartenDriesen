using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public int balance = 100;
    public TMP_Text balanceText;

    public GameObject player;
    private Health health;

    void Start()
    {
        health = FindObjectOfType<Health>();

        if (balanceText == null)
        {
            GameObject foundObject = GameObject.Find("balanceText");
            if (foundObject != null)
            {
                balanceText = foundObject.GetComponent<TMP_Text>();
                Debug.Log("balanceText ok.");
            } else
            { Debug.LogError("balanceText GameObject not find");
                return;
            }
        }
    }

    void Update() 
    {
        UpdateBalanceDisplay();

        if (health.currentHealth <= 0)
        { player.transform.position = new Vector3(8, 15, player.transform.position.z);
            health.currentHealth = health.maxHealth;
            balance -= 40;
        }
    }

    private void UpdateBalanceDisplay()
    {
        if (balanceText != null) {
            balanceText.text = "Balance: " + balance + " $";
        }
        else {
            Debug.LogWarning("balanceText is null."); }
    }

    public void AddToBalance(int amount)
    {
        if (amount < 0)
        {    Debug.LogWarning(" add a negative amount.");
            return;    }

        balance += amount;
    }
}
