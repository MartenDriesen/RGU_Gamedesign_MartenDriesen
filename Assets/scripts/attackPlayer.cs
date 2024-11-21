using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPlayer : MonoBehaviour
{
    private Dragonfly dragonfly;

    void Start()
    {
        dragonfly = FindObjectOfType<Dragonfly>();

        if (dragonfly == null)
        {
            Debug.LogError("No Dragonfly findable");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("player"))
        {   Debug.Log("Player detected by attackPlayer.");
            if (dragonfly != null)
            {
                Debug.Log("anhry");
                dragonfly.isHit = true; }
        }
    }
}
