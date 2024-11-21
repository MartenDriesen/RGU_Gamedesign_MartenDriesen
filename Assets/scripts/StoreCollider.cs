using UnityEngine;

public class StoreTrigger : MonoBehaviour
{
    public GameObject beekeeper;

    private void OnTriggerEnter(Collider other)
    {if (other.tag == "player")
        {   Debug.Log("at the store.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            Debug.Log("eixt stor.");
        }
    }
}
