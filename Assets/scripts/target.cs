using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject objectToDestroy1;
    public GameObject objectToDestroy2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (objectToDestroy1 != null)
            {
                objectToDestroy1.SetActive(false);
            }
            if (objectToDestroy2 != null)
            {
                objectToDestroy2.SetActive(false);
            }
            
            gameObject.SetActive(false);

            Destroy(collision.gameObject);
        }
    }
}
