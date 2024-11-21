using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyShootDistance : MonoBehaviour
{
    public GameStateManager gameState;
    public Shoot shoot;

    void Start()
    {
        gameState = FindObjectOfType<GameStateManager>();
        shoot = FindObjectOfType<Shoot>();
    }

    public void OnButtonClick()
    {
        Debug.Log("clicked");
        if (shoot != null && gameState != null)
        {
            shoot.travelDistance += 1f;
            gameState.balance -= 50;
        }
        else
        {
            Debug.LogError("Scripts are not assigned.");
        }
    }
}
