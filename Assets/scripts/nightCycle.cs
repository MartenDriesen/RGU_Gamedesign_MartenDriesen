using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
//used theory from my school in Belgium to adjust alpha on night sky.
public class NighttimeStarSpawner : MonoBehaviour
{
    public GameObject starPrefab;
    public Vector2 starSpawnRangeX = new Vector2(104f, 106f);
    public float starSpawnY = 10.55f;
    public TextMeshProUGUI starSignText;
    public float textDisplayDuration = 7f;
    public float alphaFadeDuration = 4f;
    public float interval = 60f;
    
    private bool isNight = false;
    private bool starSpawned = false;
    private SpriteRenderer spriteRenderer;
    private Color color;
    private List<GameObject> spawnedStars = new List<GameObject>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        color.a = 0;
        spriteRenderer.color = color;
        StartCoroutine(FadeCycle());
    }

    void Update()
    {  if (!isNight && starSpawned)
        {   starSpawned = false;
            DestroyAllStars();
        }
    }

    IEnumerator SpawnStarAndShowText()
    {
        float randomX = Random.Range(starSpawnRangeX.x, starSpawnRangeX.y);
        Vector2 spawnPosition = new Vector2(randomX, starSpawnY);
        GameObject star = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
        spawnedStars.Add(star);

        if (starSignText != null)
        {
            starSignText.gameObject.SetActive(true);
        }
//
        yield return new WaitForSeconds(textDisplayDuration);

        if (starSignText != null)
        {
            starSignText.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeCycle()
    {
        //used theory from my school in Belgium to adjust alpha on night sky.
        //https://stackoverflow.com/questions/53139259/making-a-timer-in-unity
        yield return new WaitForSeconds(interval);

        while (true)
        {
            isNight = true;
            yield return StartCoroutine(FadeAlpha(0, 200 / 255f, alphaFadeDuration));

            yield return new WaitForSeconds(7f);

            if (isNight)
            {  StartCoroutine(SpawnStarAndShowText());
                starSpawned = true;
            }

            yield return new WaitForSeconds(interval);

            isNight = false;
            //https://gamedevbeginner.com/coroutines-in-unity-when-and-how-to-use-them/

            yield return StartCoroutine(FadeAlpha(200 / 255f, 0, alphaFadeDuration));

            yield return new WaitForSeconds(interval);
        }
    }

    IEnumerator FadeAlpha(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        color = spriteRenderer.color;

        while (elapsed < duration)
        {   elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            spriteRenderer.color = color;
            yield return null;
        }

        color.a = endAlpha;
        spriteRenderer.color = color;
    }

    void DestroyAllStars()
    { foreach (GameObject star in spawnedStars)
        {   if (star != null)
            {
                Destroy(star);    }
        }
        spawnedStars.Clear();
    }
}
