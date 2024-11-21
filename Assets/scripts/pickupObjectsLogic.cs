using System.Collections.Generic;
using UnityEngine;
//https://chatgpt.com/share/67333bce-fa48-8001-ad18-344d7e7bdbba
// pickup logic is my own. however it needed an upgrade which was too difficult i asked chat gpt and used it to start over.
public class PickupObjectsLogic : MonoBehaviour
{
    public Transform item1;
    public Transform item2;
    public Transform item3;
    
    public int maxPickupCount = 3; // Max number of objects player can pick up

    private List<GameObject> heldObjects = new List<GameObject>();
    private HashSet<GameObject> nearbyObjects = new HashSet<GameObject>(); // Track multiple nearby objects

    void Update()
    {
        HandlePickupAndDrop();
        UpdateHeldObjectPositions();
    }

    void HandlePickupAndDrop()
    {
        if (Input.GetKeyDown(KeyCode.W) && nearbyObjects.Count > 0 && heldObjects.Count < maxPickupCount)
        {
            // Attempt to pick up the first object in nearbyObjects
            GameObject objectToPickup = GetFirstAvailablePickup();
            if (objectToPickup != null)
            {
                PickUpObject(objectToPickup);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
    }

    private GameObject GetFirstAvailablePickup()
    {
        // Return the first object that is a "HoneyPot" or "Flower" and within max pickup limits
        foreach (var obj in nearbyObjects)
        {
            if (obj.CompareTag("HoneyPot") || obj.CompareTag("Flower"))
            {
                return obj;
            }
        }
        return null;
    }

    private void PickUpObject(GameObject objectToPickup)
    {
        heldObjects.Add(objectToPickup); // Add object to held list
        objectToPickup.GetComponent<Collider2D>().enabled = false; // Disable collider

        // Attach to the correct item position
        Transform targetTransform = heldObjects.Count switch
        {
            1 => item1,
            2 => item2,
            3 => item3,
            _ => null
        };

        if (targetTransform != null)
        {
            objectToPickup.transform.SetParent(targetTransform);
            objectToPickup.transform.localPosition = Vector3.zero;
        }

        // Set scale to 35% smaller
        objectToPickup.transform.localScale *= 0.65f;

        // Stop particle effect when picked up
        ParticleSystem particles = objectToPickup.GetComponentInChildren<ParticleSystem>();
        if (particles != null)
        {
            particles.Stop();
        }

        Debug.Log("Picked up " + objectToPickup.name);
        nearbyObjects.Remove(objectToPickup); // Remove from nearby objects since it’s picked up
    }

    private void DropObject()
    {
        if (heldObjects.Count > 0)
        {
            // Get the last held object
            GameObject objectToDrop = heldObjects[heldObjects.Count - 1];
            heldObjects.RemoveAt(heldObjects.Count - 1); // Remove from the held list

            // Detach from parent and reset position
            objectToDrop.transform.SetParent(null);
            objectToDrop.GetComponent<Collider2D>().enabled = true;

            // Drop the object a bit down on the Y-axis
            objectToDrop.transform.position = new Vector3(objectToDrop.transform.position.x, objectToDrop.transform.position.y -4f, objectToDrop.transform.position.z);

            // Reset scale to original (1,1,1)
            objectToDrop.transform.localScale = Vector3.one;

            // Restart particle effect
            ParticleSystem particles = objectToDrop.GetComponentInChildren<ParticleSystem>();
            if (particles != null)
            {
                particles.Play();
            }

            Debug.Log("Dropped " + objectToDrop.name);
        }
    }

    private void UpdateHeldObjectPositions()
    {
        // Only update position, not scale, to ensure they follow parent without inheriting parent's scale
        foreach (GameObject heldObject in heldObjects)
        {
            if (heldObject.transform.parent != null)
            {
                heldObject.transform.position = heldObject.transform.parent.position;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // If a nearby object with the correct tag is in range and hasn't reached the max pickup count
        if ((other.CompareTag("HoneyPot") || other.CompareTag("Flower")) && heldObjects.Count < maxPickupCount)
        {
            nearbyObjects.Add(other.gameObject); // Add the object to the nearby set

            // Play the particle effect for the nearby object if it has a ParticleSystem
            ParticleSystem particles = other.GetComponentInChildren<ParticleSystem>();
            if (particles != null && !particles.isPlaying)
            {
                particles.Play();
            }

            Debug.Log("You can pick up " + other.name + " tagged as " + other.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Remove object from nearbyObjects and stop particle effect when exiting trigger
        if (nearbyObjects.Contains(other.gameObject))
        {
            ParticleSystem particles = other.GetComponentInChildren<ParticleSystem>();
            if (particles != null)
            {
                particles.Stop();
            }

            nearbyObjects.Remove(other.gameObject); // Remove from the nearby set
            Debug.Log("Exited trigger for " + other.name);
        }
    }
}
