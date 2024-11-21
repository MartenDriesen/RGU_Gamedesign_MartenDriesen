using System.Collections.Generic;
using UnityEngine;

public class PickupLogic : MonoBehaviour
{
    public Transform handPosition;
    private GameObject currentObject;
    private bool isHoldingObject = false;
    public ParticleSystem particleSystemPrefab;
    private Dictionary<GameObject, ParticleSystem> activeParticleSystems = new Dictionary<GameObject, ParticleSystem>();

    void Update()
    {
        pickupObjectLogic();
    }

    void pickupObjectLogic()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {    if (isHoldingObject)
            {      DropObject();
            }
            else if (currentObject != null)
            {
                PickUpObject();
            }
        }
    }

    private void PickUpObject()
    {
   if (currentObject == null) return;
   isHoldingObject = true;
     currentObject.transform.SetParent(handPosition);
        currentObject.transform.localPosition = Vector3.zero;
        currentObject.GetComponent<Collider2D>().enabled = false;
        StopAndDestroyParticles(currentObject);
    }

    private void DropObject()
    {
        if (currentObject == null) return;

     isHoldingObject = false;
        currentObject.transform.SetParent(null);
     currentObject.GetComponent<Collider2D>().enabled = true;
        EmitParticles(currentObject);
        currentObject = null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    { if ((collision.CompareTag("Flower") || collision.CompareTag("HoneyPot")) && !isHoldingObject)
        {
            currentObject = collision.gameObject;
            EmitParticles(currentObject);
        }
    } private void OnTriggerExit2D(Collider2D collision)
    {
     if ((collision.CompareTag("Flower") || collision.CompareTag("HoneyPot")) && !isHoldingObject)
        {
            StopAndDestroyParticles(collision.gameObject);
            currentObject = null;
        }
    }

 private void EmitParticles(GameObject obj)
    {  if (!activeParticleSystems.ContainsKey(obj))
        {
            ParticleSystem newParticleSystem = Instantiate(particleSystemPrefab, obj.transform.position, Quaternion.identity);
            newParticleSystem.transform.SetParent(obj.transform);
            newParticleSystem.Play();
            activeParticleSystems[obj] = newParticleSystem;
        } else if (activeParticleSystems[obj] != null && !activeParticleSystems[obj].isPlaying)
        {
            activeParticleSystems[obj].Play();
        }
    }

    private void StopAndDestroyParticles(GameObject obj)
    {
        if (activeParticleSystems.TryGetValue(obj, out ParticleSystem particleSystem))
        {
            particleSystem.Stop();
            Destroy(particleSystem.gameObject);
            activeParticleSystems.Remove(obj);
        }
    }
}
