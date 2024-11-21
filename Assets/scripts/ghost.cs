using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Dragonfly dragonfly;
    private List<SpriteRenderer> childRenderers = new List<SpriteRenderer>();
    private bool canUseInvisibility = true;
    public float invisibilityDuration = 4f;public float cooldownDuration = 10f;

    void Start()
    {
  CollectRenderers(transform);
        dragonfly = FindObjectOfType<Dragonfly>();
        Transform body = transform.Find("Body");
        if (body != null)
        {
            CollectRenderers(body);
        } }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && canUseInvisibility)
        {
            dragonfly.playerInvi = true;
         StartCoroutine(ActivateInvisibility());
        }
    }

    private IEnumerator ActivateInvisibility()
    { canUseInvisibility = false;
     SetOpacity(0.5f);
        yield return new WaitForSeconds(invisibilityDuration);
 SetOpacity(1f);
        dragonfly.playerInvi = false;
     yield return new WaitForSeconds(cooldownDuration);
         canUseInvisibility = true;
    }

    private void SetOpacity(float opacity)
    { foreach (SpriteRenderer renderer in childRenderers)
        {    Color color = renderer.color;
            color.a = opacity;
           renderer.color = color;
        }
    }

    private void CollectRenderers(Transform parent)
    {
 foreach (SpriteRenderer renderer in parent.GetComponentsInChildren<SpriteRenderer>())
        {   childRenderers.Add(renderer); }
   }}
